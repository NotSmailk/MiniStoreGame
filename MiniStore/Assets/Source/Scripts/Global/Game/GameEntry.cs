using Assets.Source.Scripts.BaseComponents;
using Assets.Source.Scripts.Garbage.Systems;
using Assets.Source.Scripts.Global.Game;
using Assets.Source.Scripts.Global.Game.Systems;
using Assets.Source.Scripts.Player.Systems;
using Assets.Source.Scripts.ScriptableObjects.Data;
using Assets.Source.Scripts.Stands.Components;
using Assets.Source.Scripts.Stands.GameEntities;
using Assets.Source.Scripts.Stands.Systems;
using Assets.Source.Scripts.Table.Components;
using Assets.Source.Scripts.Table.GameEntities;
using Assets.Source.Scripts.Table.Systems;
using Assets.Source.Scripts.Utils;
using Assets.Source.UI;
using Assets.Source.UI.Systems;
using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    sealed class GameEntry : MonoBehaviour 
    {
        [SerializeField] private GUIPanel _panel;
        [SerializeField] private TableEntity[] _tables;
        [SerializeField] private StandEntity[] _stands;
        [SerializeField] private Camera _mainCamera;

        private EcsWorld _world;
        private EcsSystems _pausableSystems;
        private EcsSystems _startSystems;
        private EcsSystems _uiSystems;
        private List<EcsSystems> _systems;
        private FlasksData _flasks;
        private StandData _standsData;
        private IAssetLoader _loader;
        private GameProgress _progress;
        private bool _isInitialized = false;

        private void Awake() 
        {
            _progress = new GameProgress();
            _progress.AddToStart(StartGame);
            _progress.AddScoreChange(_panel.ShowScore);
            _loader = new ResourcesAssetLoader();
            _systems = new();
            _world = new EcsWorld();
            _pausableSystems = new EcsSystems(_world);
            _startSystems = new EcsSystems(_world);
            _uiSystems = new EcsSystems(_world);
            _flasks = _loader.Load<FlasksData>("Data/New Flask Data");
            _standsData = _loader.Load<StandData>("Data/New Stands Data");

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world); 
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_pausableSystems);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_startSystems);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_uiSystems);
#endif
            InitStands();
            InitTables();

            InitStartSystems();
            InitPausableSystems();
            InitUISystems();
        }

        public void StartGame()
        {
            _isInitialized = true;
        }

        private void InitStands()
        {
            foreach (var stand in _stands)
            {
                stand.Entity = _world.NewEntity();
                stand.Entity.Get<StandComponent>();
                stand.Entity.Get<TransformComponent>() = stand.uiPoint;
            }
        }

        private void InitTables()
        {
            foreach (var table in _tables)
            {
                table.Entity = _world.NewEntity();
                table.Entity.Get<TableComponent>().Mesh = table.hintMesh;
                table.Entity.Get<TableComponent>().Light = table.hintLight;
                table.Entity.Get<ChangeTableItemEvent>();
            }
        }

        private void InitUISystems()
        {
            _uiSystems
                .Add(new StartButtonClickSystem())
                .Add(new ChangeScoreSystem());

            _uiSystems
                .Inject(_flasks)
                .Inject(_standsData)
                .Inject(_panel)
                .Inject(_progress)
                .InjectUi(_panel.UIEmitter)
                .Init();

            _systems.Add(_uiSystems);
        }

        private void InitStartSystems()
        {
            _startSystems
                .Add(new PlayerSpawnSystem())
                .Add(new StandSliderInitSystem());

            _startSystems
                .Inject(_flasks)
                .Inject(_standsData)
                .Inject(_panel)
                .Inject(_mainCamera)
                .Init();

            _systems.Add(_startSystems);
        }

        private void InitPausableSystems()
        {
            _pausableSystems
                .Add(new PhoneInputSystem())
                .Add(new MoveSystem())
                .Add(new StackControlSystem())
                .Add(new RemoveItemFromStackCheckerSystem())
                .Add(new StandItemProducingSystem())
                .Add(new RestartProducingSystem())
                .Add(new TableItemChangeSystem())
                .Add(new StandCollisionSystem());

            _pausableSystems
                .Inject(_flasks)
                .Inject(_standsData)
                .Init();

            _systems.Add(_pausableSystems);
        }

        private void Update()
        {
            _uiSystems?.Run();

            if (!_isInitialized)
                return;

            _pausableSystems?.Run();
            _startSystems?.Run();
        }

        private void OnDestroy() 
        {
            foreach (var system in _systems)
                DestroySystem(system);

            DestroyWorld(_world);
        }

        private void DestroySystem(EcsSystems system)
        {
            system?.Destroy();
            system = null;
        }

        private void DestroyWorld(EcsWorld world)
        {
            world?.Destroy();
            world = null;
        }
    }
}
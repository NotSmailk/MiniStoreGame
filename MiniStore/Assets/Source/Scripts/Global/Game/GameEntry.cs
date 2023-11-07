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
        private StandData _standsData;
        private FlasksData _flasks;
        private IAssetLoader _loader;
        private GameProgress _progress;
        private bool _isInitialized = false;

        private void Awake() 
        {
            _progress = new GameProgress();
            _progress.AddToStart(StartGame);
            _progress.AddScoreChange(_panel.ShowScore);
            _loader = new AddressablesAssetLoader();
            _systems = new();
            _world = new EcsWorld();
            _pausableSystems = new EcsSystems(_world);
            _startSystems = new EcsSystems(_world);
            _uiSystems = new EcsSystems(_world);
            _standsData = _loader.LoadScriptableObject<StandData>("Data/New Stands Data");
            _flasks = _loader.LoadScriptableObject<FlasksData>("Data/New Flask Data");
            _flasks.Loader = _loader;
            _flasks.InitLib();
            FixCamera();

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

        public void FixCamera()
        {
            float targetaspect = 9.0f / 16.0f;
            float windowaspect = (float)Screen.width / (float)Screen.height;
            float scaleheight = windowaspect / targetaspect;

            if (scaleheight < 1.0f)
            {
                Rect rect = _mainCamera.rect;

                rect.width = 1.0f;
                rect.height = scaleheight;
                rect.x = 0;
                rect.y = (1.0f - scaleheight) / 2.0f;

                _mainCamera.rect = rect;
            }
            else
            {
                float scalewidth = 1.0f / scaleheight;

                Rect rect = _mainCamera.rect;

                rect.width = scalewidth;
                rect.height = 1.0f;
                rect.x = (1.0f - scalewidth) / 2.0f;
                rect.y = 0;

                _mainCamera.rect = rect;
            }
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
                .Inject(_loader)
                .Init();

            _systems.Add(_startSystems);
        }

        private void InitPausableSystems()
        {
            _pausableSystems
                .Add(new PhoneInputSystem())
                .Add(new MoveSystem())
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
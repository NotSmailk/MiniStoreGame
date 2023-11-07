using Assets.Source.Scripts.BaseComponents;
using Assets.Source.Scripts.Player.Components;
using Leopotam.Ecs;
using UnityEngine;
using System.Collections.Generic;
using Assets.Source.Scripts.Utils;

namespace Assets.Source.Scripts.Player.Systems
{
    public class PlayerSpawnSystem : IEcsInitSystem
    {
        private EcsWorld _world = null;
        private const string PLAYER_PATH = "Prefabs/Player/Player";

        public void Init()
        {
            var loader = new ResourcesAssetLoader();
            var playerPrefab = loader.Load<PlayerEntity>(PLAYER_PATH);
            var player = Object.Instantiate(playerPrefab);

            player.Entity = _world.NewEntity();
            player.Entity.Get<PlayerComponent>();

            ref var move = ref player.Entity.Get<MoveComponent>();
            move.speed = 2f;
            move.rigidBody.Rigidbody = player.GetComponent<Rigidbody>();
            move.transform.Transform = player.transform;

            ref var stack = ref player.Entity.Get<StackComponent>();
            stack.FlasksStack = new Stack<Items.Components.FlaskComponent>();
            var point = new TransformComponent();
            point.Transform = player.stackPoint;
            stack.Stackpoint = point;
        }
    }
}

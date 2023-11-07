using Assets.Source.Scripts.Player.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Source.Scripts.Global.Game.Systems
{
    public class PhoneInputSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, MoveComponent> _filter = null;

        private float _sensivity = 0.15f;
        private Vector2 InputDelta => Input.GetTouch(0).deltaPosition;

        public void Run()
        {
            foreach (var ent in _filter)
            {
                ref var move = ref _filter.Get2(ent);
                        
                move.velocity = GetVelocity();
            }
        }

        private Vector3 GetVelocity()
        {
            var velocity = new Vector3();

            if (Input.touchCount <= 0)
                return velocity;

            velocity.x = InputDelta.x;
            velocity.z = InputDelta.y;
            return velocity;
        }
    }
}

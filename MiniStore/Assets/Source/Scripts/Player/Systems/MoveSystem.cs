using Assets.Source.Scripts.Player.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Source.Scripts.Player.Systems
{
    public class MoveSystem : IEcsRunSystem
    {
        private EcsFilter<MoveComponent> _filter = null;

        public void Run()
        {
            foreach (var ent in _filter)
            {
                ref var move = ref _filter.Get1(ent);
                move.velocity.Normalize();
                move.rigidBody.Rigidbody.velocity = move.velocity * move.speed;

                if (move.velocity != Vector3.zero)
                    move.transform.Transform.rotation = Quaternion.LookRotation(-move.velocity);
            }
        }
    }
}

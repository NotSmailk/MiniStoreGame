using Assets.Source.Scripts.Garbage.Components;
using Assets.Source.Scripts.Player.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Source.Scripts.Garbage.GameEntities
{
    public class GarbageEntity : GameEntity
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GameEntity entity))
            {
                if (entity.Entity.Has<StackComponent>())
                {
                    entity.Entity.Get<RemoveItemFromStackEvent>();
                }
            }
        }
    }
}
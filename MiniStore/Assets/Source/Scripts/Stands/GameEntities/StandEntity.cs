using Assets.Source.Scripts.BaseComponents;
using Assets.Source.Scripts.Player.Components;
using Assets.Source.Scripts.Stands.Components;
using Assets.Source.Scripts.Utils;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Source.Scripts.Stands.GameEntities
{
    public class StandEntity : GameEntity
    {
        public StandComponent standInfo;
        public TransformComponent uiPoint;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GameEntity entity))
            {
                StandComponent stand = default;
                StackComponent stack = default;
                if (entity.Entity.TryGet(ref stack) && Entity.TryGet(ref stand))
                {
                    entity.Entity.Get<AddToStackEvent>().FlaskType = standInfo.type;
                    entity.Entity.Get<AddToStackEvent>().IsProduced = stand.isProduced;

                    Entity.Get<ProducedItemTakenEvent>().IsProduced = stand.isProduced;
                    Entity.Get<ProducedItemTakenEvent>().StackCount = stack.FlasksStack.Count;
                }
            }
        }
    }
}

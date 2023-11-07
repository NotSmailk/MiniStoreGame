using Assets.Source.Scripts.Garbage.Components;
using Assets.Source.Scripts.Global.Game.Components;
using Assets.Source.Scripts.Items.Components;
using Assets.Source.Scripts.Player.Components;
using Assets.Source.Scripts.Table.Components;
using Assets.Source.Scripts.Utils;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Source.Scripts.Table.GameEntities
{
    public class TableEntity : GameEntity
    {
        public MeshRenderer hintMesh;
        public Light hintLight;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GameEntity entity))
            {
                StackComponent stack = new();
                TableComponent table = new();

                if (entity.Entity.TryGet(ref stack) && Entity.TryGet(ref table))
                {
                    if (stack.FlasksStack.TryPeek(out FlaskComponent flask))
                    {
                        if (flask.type.Equals(table.FlaskType))
                        {
                            entity.Entity.Get<RemoveItemFromStackEvent>();
                            entity.Entity.Get<ScoreChangedEvent>();
                            Entity.Get<ChangeTableItemEvent>();
                        }
                    }
                }
            }
        }
    }
}
using Assets.Source.Scripts.Items.GameEntities;
using Assets.Source.Scripts.Table.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Source.Scripts.Table.Systems
{
    public class TableItemChangeSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<TableComponent, ChangeTableItemEvent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var enitity = ref _filter.GetEntity(i);
                ref var table = ref _filter.Get1(i);
                ChangeColor(ref table);
                enitity.Del<ChangeTableItemEvent>();
            }
        }

        public void ChangeColor(ref TableComponent table)
        {
            table.FlaskType.RandomType();
            var propertyBlock = new MaterialPropertyBlock();
            table.Mesh.GetPropertyBlock(propertyBlock);
            var color = table.FlaskType.GetColor();
            propertyBlock.SetColor("_Color", color);
            table.Mesh.SetPropertyBlock(propertyBlock);
            table.Light.color = color;
        }
    }
}

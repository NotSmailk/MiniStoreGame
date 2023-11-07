using Assets.Source.Scripts.Player.Components;
using Leopotam.Ecs;
using System;

namespace Assets.Source.Scripts.Player.Systems
{
    public class StackControlSystem : IEcsRunSystem
    {
        private EcsWorld _world = null;
        private EcsFilter<StackComponent> _filter;

        public void Run()
        {
            
        }
    }
}

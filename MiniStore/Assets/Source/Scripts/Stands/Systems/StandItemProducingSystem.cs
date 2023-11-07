using Assets.Source.Scripts.ScriptableObjects.Data;
using Assets.Source.Scripts.Stands.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Source.Scripts.Stands.Systems
{
    public class StandItemProducingSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<StandComponent, StandSliderComponent> _filter;
        private StandData _stands;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var stand = ref _filter.Get1(i);
                ref var slider = ref _filter.Get2(i);

                if (stand.producingTime <= _stands.PRODUCING_TIME)
                {
                    stand.producingTime += Time.deltaTime;
                    slider.Slider.value = stand.producingTime / _stands.PRODUCING_TIME;
                }
                else
                {
                    stand.isProduced = true;
                    slider.ReadyImage.gameObject.SetActive(true);
                    slider.Slider.gameObject.SetActive(false);
                }
            }
        }
    }
}

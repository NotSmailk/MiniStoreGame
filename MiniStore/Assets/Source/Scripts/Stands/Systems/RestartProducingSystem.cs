using Assets.Source.Scripts.Stands.Components;
using Leopotam.Ecs;

namespace Assets.Source.Scripts.Stands.Systems
{
    public class RestartProducingSystem : IEcsRunSystem
    {
        private EcsFilter<StandComponent, StandSliderComponent, ProducedItemTakenEvent> _stands;

        public void Run()
        {
            foreach (var i in _stands)
            {
                ref var ent = ref _stands.GetEntity(i);
                ref var evnt = ref _stands.Get3(i);
                ref var slider = ref _stands.Get2(i);
                ref var stand = ref _stands.Get1(i);

                if (evnt.IsProduced && evnt.StackCount < 4)
                {
                    stand.isProduced = false;
                    stand.producingTime = 0f;
                    slider.Slider.gameObject.SetActive(true);
                    slider.Slider.value = 0f;
                    slider.ReadyImage.gameObject.SetActive(false);
                }

                ent.Del<ProducedItemTakenEvent>();
            }
        }
    }
}

using Assets.Source.Scripts.BaseComponents;
using Assets.Source.Scripts.ScriptableObjects.Data;
using Assets.Source.Scripts.Stands.Components;
using Assets.Source.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Source.Scripts.Stands.Systems
{
    public class StandSliderInitSystem : IEcsInitSystem
    {
        private EcsFilter<StandComponent> _stands;
        private StandData _data;
        private GUIPanel _panel;
        private Camera _main;

        public void Init()
        {
            foreach (var i in _stands)
            {
                ref var stand = ref _stands.GetEntity(i);
                var slider = Object.Instantiate(_data.Panel, _panel.transform);
                ref var sliderComp = ref stand.Get<StandSliderComponent>();
                sliderComp = slider.component;
                sliderComp.Slider.value = 0;
                sliderComp.ReadyImage.gameObject.SetActive(false);
                var transform = stand.Get<TransformComponent>();
                slider.GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint(_main, transform.Transform.position);
            }
        }
    }
}

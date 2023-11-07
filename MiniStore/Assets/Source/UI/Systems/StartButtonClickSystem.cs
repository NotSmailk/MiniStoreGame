using Assets.Source.Scripts.Global.Game;
using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;

namespace Assets.Source.UI.Systems
{
    public class StartButtonClickSystem : IEcsRunSystem
    {
        private EcsFilter<EcsUiClickEvent> _clickables;
        private readonly string _widgetName = "StartButton";
        private GameProgress _progress;

        public void Run()
        {
            foreach (var i in _clickables)
            {
                ref var ent = ref _clickables.GetEntity(i);
                ref var btn = ref _clickables.Get1(i);
                btn.Sender.SetActive(false);
                _progress.StartGame();
                ent.Del<EcsUiClickEvent>();
            }
        }
    }
}
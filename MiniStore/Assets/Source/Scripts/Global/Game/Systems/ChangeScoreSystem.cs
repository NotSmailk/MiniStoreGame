using Assets.Source.Scripts.Global.Game.Components;
using Leopotam.Ecs;

namespace Assets.Source.Scripts.Global.Game.Systems
{
    public class ChangeScoreSystem : IEcsRunSystem
    {
        private EcsFilter<ScoreChangedEvent> _events;
        private GameProgress _progress;
        private float _score = 10;

        public void Run()
        {
            foreach (var i in _events)
            {
                ref var ent = ref _events.GetEntity(i);
                ent.Del<ScoreChangedEvent>();
                _progress.AddScore(_score);
            }
        }
    }
}

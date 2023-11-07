using Assets.Source.Scripts.Garbage.Components;
using Assets.Source.Scripts.Items.Components;
using Assets.Source.Scripts.Player.Components;
using Leopotam.Ecs;

namespace Assets.Source.Scripts.Garbage.Systems
{
    public class RemoveItemFromStackCheckerSystem : IEcsRunSystem
    {
        private EcsFilter<StackComponent, RemoveItemFromStackEvent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var stack = ref _filter.Get1(i);

                if (stack.FlasksStack.TryPop(out FlaskComponent flask))
                {
                    UnityEngine.Object.Destroy(flask.flask.gameObject);
                }

                entity.Del<RemoveItemFromStackEvent>();
            }
        }
    }
}

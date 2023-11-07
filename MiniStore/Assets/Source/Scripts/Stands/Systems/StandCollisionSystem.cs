using Assets.Source.Scripts.Items.Components;
using Assets.Source.Scripts.Player.Components;
using Assets.Source.Scripts.ScriptableObjects.Data;
using Assets.Source.Scripts.Stands.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Source.Scripts.Stands.Systems
{
    public class StandCollisionSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private FlasksData _flaskData;
        private EcsFilter<StackComponent, AddToStackEvent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var ent = ref _filter.GetEntity(i);
                ref var stack = ref _filter.Get1(i);
                ref var evnt = ref _filter.Get2(i);

                if (stack.FlasksStack.Count < 4 && evnt.IsProduced)
                    stack.FlasksStack.Push(CreateFlask(evnt.FlaskType, stack));

                ent.Del<AddToStackEvent>();
            }
        }

        public FlaskComponent CreateFlask(FlaskType type, StackComponent stack)
        {
            var flask = Object.Instantiate(_flaskData.Get(type), stack.Stackpoint.Transform);
            flask.transform.position += CalcHeight(stack.FlasksStack.Count);
            flask.Entity = _world.NewEntity();
            ref var comp = ref flask.Entity.Get<FlaskComponent>();
            comp.flask = flask;
            comp.type = type;
            return comp;
        }

        public Vector3 CalcHeight(int count)
        {
            var offset = Vector3.up * _flaskData.FlaskOffset;
            return offset * count;
        }
    }
}

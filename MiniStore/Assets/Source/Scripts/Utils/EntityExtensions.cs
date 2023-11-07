using Leopotam.Ecs;

namespace Assets.Source.Scripts.Utils
{
    public static class EntityExtensions
    {
        public static bool TryGet<T>(this EcsEntity entity, ref T comp) where T : struct
        {
            if (entity.Has<T>())
            {
                comp = entity.Get<T>();
                return true;
            }

            return false;
        }
    }
}

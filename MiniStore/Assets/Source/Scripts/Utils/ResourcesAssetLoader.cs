using UnityEngine;

namespace Assets.Source.Scripts.Utils
{
    public class ResourcesAssetLoader : IAssetLoader
    {
        public T Load<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}

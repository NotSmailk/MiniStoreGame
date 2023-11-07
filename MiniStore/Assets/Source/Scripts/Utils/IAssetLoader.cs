using UnityEngine;

namespace Assets.Source.Scripts.Utils
{
    public interface IAssetLoader
    {
        public T Load<T>(string path) where T : Object;
    }
}

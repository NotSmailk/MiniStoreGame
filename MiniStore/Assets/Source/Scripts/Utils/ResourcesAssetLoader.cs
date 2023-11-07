using UnityEngine;

namespace Assets.Source.Scripts.Utils
{
    public class ResourcesAssetLoader : IAssetLoader
    {
        public T LoadMonoBehaviour<T>(string path) where T : MonoBehaviour
        {
            return Resources.Load<T>(path);
        }

        public T LoadScriptableObject<T>(string path) where T : ScriptableObject
        {
            return Resources.Load<T>(path);
        }
    }
}

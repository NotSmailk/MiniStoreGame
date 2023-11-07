using UnityEngine;

namespace Assets.Source.Scripts.Utils
{
    public interface IAssetLoader
    {
        public T LoadMonoBehaviour<T>(string path) where T : MonoBehaviour;
        public T LoadScriptableObject<T>(string path) where T : ScriptableObject;
    }
}

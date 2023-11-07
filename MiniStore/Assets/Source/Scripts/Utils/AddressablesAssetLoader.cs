using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Source.Scripts.Utils
{
    public class AddressablesAssetLoader : IAssetLoader
    {
        public T LoadMonoBehaviour<T>(string path) where T : MonoBehaviour
        {
            return Addressables.LoadAssetAsync<GameObject>(path).WaitForCompletion().GetComponent<T>();
        }

        public T LoadScriptableObject<T>(string path) where T : ScriptableObject
        {
            return Addressables.LoadAssetAsync<T>(path).WaitForCompletion();
        }
    }
}

using UnityEngine;

namespace Assets.Source.Scripts.ScriptableObjects.Data
{
    [CreateAssetMenu(menuName = "Data/Stand", fileName = "New Stands Data")]
    public class StandData : ScriptableObject
    {
        [SerializeField] private StandSlider _panel;

        public StandSlider Panel => _panel;

        public readonly float PRODUCING_TIME = 3f;
    }
}

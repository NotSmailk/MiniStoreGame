namespace Assets.Source.Scripts.Stands.Components
{
    [System.Serializable]
    public struct StandComponent
    {
        public FlaskType type;
        [UnityEngine.HideInInspector] public bool isProduced;
        [UnityEngine.HideInInspector] public float producingTime;
    }
}
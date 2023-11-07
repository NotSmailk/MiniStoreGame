using Assets.Source.Scripts.Items.GameEntities;
using Assets.Source.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Scripts.ScriptableObjects.Data
{
    [CreateAssetMenu(menuName = "Data/Flasks", fileName = "New Flask Data")]
    public class FlasksData : ScriptableObject
    {
        [SerializeField] private YellowFlask _yellow;
        [SerializeField] private BlueFlask _blue;
        [SerializeField] private RedFlask _red;
        [SerializeField] private GreenFlask _green;
        [SerializeField] private float _flaskOffset = 0.1f;

        private Dictionary<FlaskType, FlaskEntity> _flask;

        public float FlaskOffset => _flaskOffset;
        public IAssetLoader Loader;

        private void OnValidate()
        {
            InitLib();
        }

        public void InitLib()
        {
            if (Loader == null)
            {
                _flask = new();
                _flask.Add(FlaskType.Yellow, _yellow);
                _flask.Add(FlaskType.Red, _red);
                _flask.Add(FlaskType.Blue, _blue);
                _flask.Add(FlaskType.Green, _green);
            }
            else
            {
                _flask = new();
                _flask.Add(FlaskType.Yellow, Loader.LoadMonoBehaviour<YellowFlask>(_yellow.name));
                _flask.Add(FlaskType.Red, Loader.LoadMonoBehaviour<RedFlask>(_red.name));
                _flask.Add(FlaskType.Blue, Loader.LoadMonoBehaviour<BlueFlask>(_blue.name));
                _flask.Add(FlaskType.Green, Loader.LoadMonoBehaviour<GreenFlask>(_green.name));
            }
        }

        public FlaskEntity Get(FlaskType type)
        {
            if (!_flask.ContainsKey(type) || _flask[type] == null)
                InitLib();

            return _flask[type];
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Scripts.Items.GameEntities
{
    public static class FlaskTypeExtension
    {
        public static Dictionary<FlaskType, Color> Colors = new()
        {
            { FlaskType.Blue, new Color(0f, 0f, 1f) },
            { FlaskType.Green, new Color(0f, 1f, 0f) },
            { FlaskType.Red, new Color(1f, 0f, 0f) },
            { FlaskType.Yellow, new Color(1f, 1f, 0f) }
        };

        public static void RandomType(this ref FlaskType type)
        {
            type = (FlaskType)Random.Range(0, 4);
        }

        public static Color GetColor(this FlaskType type)
        {
            return Colors[type];
        }
    }
}

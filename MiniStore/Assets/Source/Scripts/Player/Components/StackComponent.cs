using Assets.Source.Scripts.BaseComponents;
using Assets.Source.Scripts.Items.Components;
using System.Collections.Generic;

namespace Assets.Source.Scripts.Player.Components
{
    public struct StackComponent
    {
        public Stack<FlaskComponent> FlasksStack { get; set; }
        public TransformComponent Stackpoint { get; set; }
    }
}
using Assets.Source.Scripts.BaseComponents;
using UnityEngine;

namespace Assets.Source.Scripts.Player.Components
{
    public struct MoveComponent
    {
        public Vector3 velocity;
        public float speed;
        public RigidbodyComponent rigidBody;
        public TransformComponent transform;
    }
}

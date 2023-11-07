using Assets.Source.Scripts.Player.Components;
using Leopotam.Ecs;
using UnityEngine;

public class KeyboardInputSystem : IEcsRunSystem
{
    private EcsFilter<PlayerComponent, MoveComponent> _filter = null;

    private const KeyCode fwd = KeyCode.W;
    private const KeyCode bwd = KeyCode.S;
    private const KeyCode left = KeyCode.A;
    private const KeyCode right = KeyCode.D;

    public void Run()
    {
        foreach (var ent in _filter)
        {
            ref var move = ref _filter.Get2(ent);

            move.velocity = GetVelocity();
        }
    }

    public Vector3 GetVelocity()
    {
        var velocity = new Vector3();
        velocity.x = Horizontal();
        velocity.z = Vertical();
        return velocity;
    }

    public float Vertical()
    {
        float axis = 0f;

        if (Input.GetKey(fwd))
            axis += 1;

        if(Input.GetKey(bwd))
            axis -= 1;

        return axis;
    }

    public float Horizontal()
    {
        float axis = 0f;

        if (Input.GetKey(right))
            axis += 1;

        if (Input.GetKey(left))
            axis -= 1;

        return axis;
    }
}

using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class CubeController : NetworkBehaviour
{
    public struct CubeInput : INetworkInput
    {
        public Vector3 Direction;
    }

    [SerializeField] private float moveSpeed = 10f;

    public override void Spawned()
    {
        if (Object.InputAuthority == Runner.LocalPlayer)
        {
            var events = Runner.GetComponent<NetworkEvents>();
            events.OnInput.AddListener(OnInput);
        }
    }

    public override void FixedUpdateNetwork()
    {
        Vector3 direction = Vector3.zero;

        if (GetInput(out CubeInput input))
        {
            direction = input.Direction;
        }

        transform.position += direction.normalized * moveSpeed * Runner.DeltaTime;
    }

    private void OnInput(NetworkRunner runner, NetworkInput inputContainer)
    {
        var input = new CubeInput
        {
            Direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"))
        };

        print("hello");

        inputContainer.Set(input);
    }

}


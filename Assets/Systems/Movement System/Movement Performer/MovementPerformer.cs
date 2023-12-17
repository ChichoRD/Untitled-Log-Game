using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPerformer : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidbody;

   

    public bool TryMove(Vector2 movementVelocity)
    {
        Vector2 velocity = movementVelocity;

        Vector2 velocityDifference = velocity - _rigidbody.velocity;

        const float ASYMPTOTIC_CONVERGENCE = 0.25f;
        //m * dv = f * dt
        // f = m * dv/dt
        Vector2 movementForce = _rigidbody.mass *
                                (velocityDifference * ASYMPTOTIC_CONVERGENCE) /
                                Time.fixedDeltaTime;

        _rigidbody.AddForce(movementForce, ForceMode2D.Force);

        return true;
    }

   

}
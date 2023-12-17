using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementApplier : MonoBehaviour
{
    [SerializeField]
    private MovementPerformer _performer;

    [SerializeField]
    private PlayerMovementInputProvider _movProvider;

    [field: SerializeField]
    public UnityEvent<Vector2> MovementPerformed { get; private set; } 

    [SerializeField]
    private float _baseSpeed = 1.0f;

    [SerializeField]
    private float _speedMultiplier = 1.0f;

    private void FixedUpdate()
    {
        MovementInput completeMovement = _movProvider.MovementInput;

        Vector2 movementVelocity = completeMovement.movement * _baseSpeed;

        if (completeMovement.isSprinting)
        {
            movementVelocity *= _speedMultiplier;
        }

        if(_performer.TryMove(movementVelocity))
        {
            MovementPerformed.Invoke(movementVelocity);
        }


        

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TreeMovementApplier : MonoBehaviour
{
    [SerializeField]
    private MovementPerformer _performer;

    [SerializeField]
    private TreeInputProvider _treeInputProvider;

    [field: SerializeField]
    public UnityEvent<Vector2> MovementPerformed { get; private set; }

    [SerializeField]
    private float _baseSpeed = 1.0f;

    private void FixedUpdate()
    {

        Vector2 movementVelocity = _treeInputProvider.MovementInput * _baseSpeed;


        if (_performer.TryMove(movementVelocity))
        {
            MovementPerformed.Invoke(movementVelocity);
        }
    }
}

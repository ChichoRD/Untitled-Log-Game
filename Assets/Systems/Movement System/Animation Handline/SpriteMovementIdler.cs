using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMovementIdler : MonoBehaviour
{
    [SerializeField]
    private MovementApplier _movApplier;

    [SerializeField]
    private Animator _animator;

    private void Awake()
    {
        _movApplier.MovementPerformed.AddListener(OnMovementPerformed);
    }

    private void OnDestroy()
    {
        _movApplier.MovementPerformed.RemoveListener(OnMovementPerformed);
    }

    private void OnMovementPerformed(Vector2 movementVelocity)
    {
        if (movementVelocity != Vector2.zero)
        {
            _animator.SetBool("IsMoving", true);
        }
        else _animator.SetBool("IsMoving", false);
    }

}

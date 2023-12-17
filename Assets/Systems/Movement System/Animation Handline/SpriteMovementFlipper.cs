using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMovementFlipper : MonoBehaviour
{
    [SerializeField]
    private MovementApplier _movApplier;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private Vector2 _lastLookedDirection = Vector2.right;

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
        if(movementVelocity!= Vector2.zero)
        {
            _lastLookedDirection = movementVelocity;
        }
        
        _spriteRenderer.flipX = _lastLookedDirection.x < 0.0f;
        
    }

}

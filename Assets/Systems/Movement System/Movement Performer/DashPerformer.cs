using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPerformer : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D _rigidbody;
   
    public bool TryDash(Vector2 dashAcceleration)
    {
        _rigidbody.AddForce(_rigidbody.mass * dashAcceleration, ForceMode2D.Force);

        return true;
    }
}


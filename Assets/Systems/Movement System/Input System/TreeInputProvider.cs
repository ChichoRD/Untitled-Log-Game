using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInputProvider : MonoBehaviour
{
    [SerializeField]
    private Transform _loganTransform;
    [SerializeField]
    private Transform _myTransform;

    public Vector2 MovementInput
    {
        get
        {
            return  (_loganTransform.position - _myTransform.position).normalized;
        } 
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInputProvider : MonoBehaviour
{
    [field: SerializeField]
    public Transform LoganTransform {  private get; set; }
    [SerializeField]
    private Transform _myTransform;

    public Vector2 MovementInput
    {
        get
        {
            return  (LoganTransform.position - _myTransform.position).normalized;
        } 
    }

}

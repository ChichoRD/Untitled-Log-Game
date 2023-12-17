using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerDashInputProvider : MonoBehaviour
{
    [SerializeField]
    private InputActionReference _dashAction;

    [field: SerializeField]
    public UnityEvent DashInputPerformed { get; private set; }


    private void Awake()
    {
        _dashAction.action.performed += DashActionPerformed;
    }

    private void OnDestroy()
    {
        _dashAction.action.performed -= DashActionPerformed;
    }

    private void OnEnable()
    {
        _dashAction.action.Enable();
    }

    private void OnDisable()
    {
        _dashAction.action.Disable();
    }


    private void DashActionPerformed(InputAction.CallbackContext obj)
    {
        DashInputPerformed.Invoke();
    }


}


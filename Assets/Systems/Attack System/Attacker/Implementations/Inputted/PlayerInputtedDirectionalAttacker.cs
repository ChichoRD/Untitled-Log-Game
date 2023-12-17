using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AttackSystem.Attacker.Implementations.Inputted
{
    internal class PlayerInputtedDirectionalAttacker : MonoBehaviour
    {
        [SerializeField]
        private InputActionReference _attackAction;

        [SerializeField]
        private Camera _camera;

        private IAttacker _attacker;
        private IInputtableAttacker<Vector2> _inputtableAttacker;

        private void Awake()
        {
            _attackAction.action.performed += OnAttackActionPerformed;
        }

        private void OnEnable()
        {
            _attackAction.action.Enable();
        }

        private void Start()
        {
            _attacker = GetComponentInChildren<IAttacker>();
            _inputtableAttacker = GetComponentInChildren<IInputtableAttacker<Vector2>>();
        }

        private void OnDisable()
        {
            _attackAction.action.Disable();
        }

        private void OnDestroy()
        {
            _attackAction.action.performed -= OnAttackActionPerformed;
        }

        private void OnAttackActionPerformed(InputAction.CallbackContext context)
        {
            Vector2 mousePosition = _attackAction.action.ReadValue<Vector2>();
            Vector3 worldMousePosition = _camera.ScreenToWorldPoint(mousePosition);
            const float Z_DEPTH = 0.0f;
            worldMousePosition.z = Z_DEPTH;

            Vector3 direction = (worldMousePosition - transform.position).normalized;

            _inputtableAttacker.SetInput(direction);
            _attacker.TryAttack();
        }
    }
}

using AttackSystem.Attack.Implementations.Throwable;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AttackSystem.Attack.Implementations.Inputted
{
    internal class PlayerInputtedAttacker : MonoBehaviour
    {
        [SerializeField]
        private InputActionReference _attackActionReference;

        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private float _attackTravelDistance;
        [SerializeField]
        private float _attackTravelTime;
        [SerializeField]
        private Rigidbody2D _rigidbody;

        private IAttack<VelocityAttackData> _attack;

        private void Awake()
        {
            _attackActionReference.action.performed += OnAttackPerformed;
        }

        private void OnEnable()
        {
            _attackActionReference.action.Enable();
        }

        private void Start()
        {
            _attack = GetComponentInChildren<IAttack<VelocityAttackData>>();
        }

        private void OnDisable()
        {
            _attackActionReference.action.Disable();
        }

        private void OnDestroy()
        {
            _attackActionReference.action.performed -= OnAttackPerformed;
        }

        private void OnAttackPerformed(InputAction.CallbackContext context)
        {
            Vector2 mousePosition = context.ReadValue<Vector2>();
            Vector3 worldMousePosition = _camera.ScreenToWorldPoint(mousePosition);
            const float Z_DEPTH = 0.0f;
            worldMousePosition.z = Z_DEPTH;

            Vector3 direction = (worldMousePosition - transform.position).normalized;

            Vector3 planarAttackVelocity = new Vector3(direction.x, 0.0f, direction.y).normalized * _attackTravelDistance / _attackTravelTime;
            Vector3 verticalAttackVelocity = _attackTravelTime * 0.5f * -Physics2D.gravity;

            Vector3 attackVelocity = planarAttackVelocity + verticalAttackVelocity;
            VelocityAttackData attackData = new VelocityAttackData(attackVelocity);
            _attack.TryAttack(attackData);
        }
    }
}

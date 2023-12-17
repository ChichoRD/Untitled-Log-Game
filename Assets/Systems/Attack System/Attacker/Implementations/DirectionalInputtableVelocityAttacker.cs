using AttackSystem.Attack;
using AttackSystem.Attack.Implementations.Throwable;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AttackSystem.Attacker.Implementations.Inputted
{
    internal class DirectionalInputtableVelocityAttacker : MonoBehaviour, IAttacker, IInputtableAttacker<Vector2>
    {
        [SerializeField]
        private float _attackTravelDistance;
        [SerializeField]
        private float _attackTravelTime;
        [SerializeField]
        private Rigidbody2D _rigidbody;

        private IAttack<VelocityAttackData> _attack;
        private Task _lastAttackTask = Task.CompletedTask;
        private Vector2 _lastAttackDirection;


        private void Start()
        {
            _attack = GetComponentInChildren<IAttack<VelocityAttackData>>();
        }

        public bool TryAttack()
        {
            Vector3 planarAttackVelocity = new Vector3(_lastAttackDirection.x, 0.0f, _lastAttackDirection.y).normalized * _attackTravelDistance / _attackTravelTime;
            Vector3 verticalAttackVelocity = _attackTravelTime * 0.5f * -Physics2D.gravity;

            Vector3 attackVelocity = planarAttackVelocity + verticalAttackVelocity;
            VelocityAttackData attackData = new VelocityAttackData(attackVelocity);
            if (_lastAttackTask.IsCompleted)
            {
                _lastAttackTask = _attack.TryAttack(attackData);
                return true;
            }

            return false;
        }

        public bool SetInput(Vector2 input)
        {
            _lastAttackDirection = input;
            return true;
        }
    }
}

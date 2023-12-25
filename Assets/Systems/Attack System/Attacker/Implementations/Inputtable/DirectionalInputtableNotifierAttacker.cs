using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace AttackSystem.Attacker.Implementations
{
    internal class DirectionalInputtableNotifierAttacker : MonoBehaviour, IAttacker, IInputtableAttacker<Vector2>
    {
        private IAttacker _attacker;
        private IInputtableAttacker<Vector2> _inputtableAttacker;
        private Vector2 _lastAttackInput;

        [field: SerializeField]
        public UnityEvent<Vector2> OnAttackedSuccessfully { get; private set; }

        [field: SerializeField]
        public UnityEvent<Vector2> OnInputSetSuccessfully { get; private set; }

        private void Start()
        {
            _attacker = GetComponentsInChildren<IAttacker>().FirstOrDefault(a => a != (IAttacker)this);
            _inputtableAttacker = GetComponentsInChildren<IInputtableAttacker<Vector2>>().FirstOrDefault(a => a != (IInputtableAttacker<Vector2>)this);
        }

        public bool TryAttack()
        {
            if (_attacker.TryAttack())
            {
                OnAttackedSuccessfully?.Invoke(_lastAttackInput);
                return true;
            }

            return false;
        }

        public bool SetInput(Vector2 input)
        {
            if (_inputtableAttacker.SetInput(input))
            {
                _lastAttackInput = input;
                OnInputSetSuccessfully?.Invoke(input);
                return true;
            }

            return false;
        }
    }
}
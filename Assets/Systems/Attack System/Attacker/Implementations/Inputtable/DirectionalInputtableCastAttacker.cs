using AttackSystem.Attack;
using AttackSystem.Attack.Data;
using AttackSystem.Attack.Implementations.Cast;
using System.Threading.Tasks;
using UnityEngine;

namespace AttackSystem.Attacker.Implementations
{
    internal class DirectionalInputtableCastAttacker : MonoBehaviour, IAttacker, IInputtableAttacker<Vector2>
    {
        [SerializeField]
        private Bounds _attackBounds;

        [SerializeField]
        private float _attackDamage;

        [SerializeField]
        private Transform _attackOrigin;

        private IAttack<ICastAttackData<AttackDamage>> _attack;

        private ICastAttackData<AttackDamage> _lastAttackData;
        private Task _lastAttackTask = Task.CompletedTask;

        private void Start()
        {
            _attack = GetComponentInChildren<IAttack<ICastAttackData<AttackDamage>>>();
        }

        public bool SetInput(Vector2 input)
        {
            _lastAttackData = new CastAttackData<AttackDamage>(
                new Bounds(_attackOrigin.position + _attackBounds.center, _attackBounds.size),
                input,
                new AttackDamage(_attackDamage));
            return true;
        }

        public bool TryAttack()
        {
            if (_lastAttackTask.IsCompleted)
            {
                _lastAttackTask = _attack.TryAttack(_lastAttackData);
                return true;
            }

            return false;
        }

        private void OnDrawGizmosSelected()
        {
            if (_attackOrigin == null)
            {
                Debug.LogWarning("No attack origin set for " + name);
                return;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_attackOrigin.position + _attackBounds.center, _attackBounds.size);
        }
    }
}
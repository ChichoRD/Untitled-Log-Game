using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace AttackSystem.Attack.Implementations.Throwable
{
    internal class StoppableThrowableAttack : MonoBehaviour, IAttack<IVelocityAttackData>
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private float _velocityReductionFactor = 0.05f;

        private IAttack<IVelocityAttackData> _throwableAttack;

        private void Start()
        {
            _throwableAttack = GetComponentsInChildren<IAttack<IVelocityAttackData>>().FirstOrDefault(a => a != (IAttack<IVelocityAttackData>)this);
        }

        public async Task TryAttack<UAttackData>(UAttackData attackData) where UAttackData : IVelocityAttackData
        {
            await _throwableAttack.TryAttack(attackData);
            _rigidbody.velocity *= _velocityReductionFactor;
        }
    }
}

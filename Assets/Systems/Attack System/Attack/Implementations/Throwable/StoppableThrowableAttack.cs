using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace AttackSystem.Attack.Implementations.Throwable
{
    internal class StoppableThrowableAttack : MonoBehaviour, IAttack<VelocityAttackData>
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private float _velocityReductionFactor = 0.05f;

        private IAttack<VelocityAttackData> _throwableAttack;

        private void Start()
        {
            _throwableAttack = GetComponentsInChildren<IAttack<VelocityAttackData>>().FirstOrDefault(a => a != (IAttack<VelocityAttackData>)this);
        }

        public async Task TryAttack(VelocityAttackData attackData)
        {
            await _throwableAttack.TryAttack(attackData);
            _rigidbody.velocity *= _velocityReductionFactor;
        }
    }
}

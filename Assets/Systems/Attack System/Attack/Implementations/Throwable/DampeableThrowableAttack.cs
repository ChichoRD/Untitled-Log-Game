using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace AttackSystem.Attack.Implementations.Throwable
{
    internal class DampeableThrowableAttack : MonoBehaviour, IAttack<IVelocityAttackData>
    {
        [SerializeField]
        [Min(0)]
        private int _dampeningBounces = 1;

        [SerializeField]
        [Min(0)]
        private float _dampeningFactor = 0.5f;
        private IAttack<IVelocityAttackData> _throwableAttack;

        private void Start()
        {
            _throwableAttack = GetComponentsInChildren<IAttack<IVelocityAttackData>>().FirstOrDefault(a => a != (IAttack<IVelocityAttackData>)this);
        }

        public async Task TryAttack<UAttackData>(UAttackData attackData) where UAttackData : IVelocityAttackData
        {
            VelocityAttackData velocityAttackData = new VelocityAttackData(attackData.VelocityIncrement);
            for (int i = 0; i < _dampeningBounces; i++)
            {
                await _throwableAttack.TryAttack(velocityAttackData);
                velocityAttackData = new VelocityAttackData(attackData.VelocityIncrement * _dampeningFactor);
            }
        }
    }
}

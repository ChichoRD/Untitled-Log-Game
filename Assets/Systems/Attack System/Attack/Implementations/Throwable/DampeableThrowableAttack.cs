using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace AttackSystem.Attack.Implementations.Throwable
{
    internal class DampeableThrowableAttack : MonoBehaviour, IAttack<VelocityAttackData>
    {
        [SerializeField]
        [Min(0)]
        private int _dampeningBounces = 1;

        [SerializeField]
        [Min(0)]
        private float _dampeningFactor = 0.5f;
        private IAttack<VelocityAttackData> _throwableAttack;

        private void Start()
        {
            _throwableAttack = GetComponentsInChildren<IAttack<VelocityAttackData>>().FirstOrDefault(a => a != (IAttack<VelocityAttackData>)this);
        }

        public async Task TryAttack(VelocityAttackData attackData)
        {
            for (int i = 0; i < _dampeningBounces; i++)
            {
                await _throwableAttack.TryAttack(attackData);
                attackData = new VelocityAttackData(attackData.VelocityIncrement * _dampeningFactor);
            }
        }
    }
}

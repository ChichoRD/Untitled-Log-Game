using AttackSystem.Attack;
using AttackSystem.Attack.Data;
using AttackSystem.Attack.Implementations.Cast;
using AttackSystem.Damageable;
using System.Threading.Tasks;
using UnityEngine;

namespace AttackSystem.Attacker.Implementations
{
    internal class CastDamagerAttack2D : MonoBehaviour, IAttack<ICastAttackData<AttackDamage>>
    {
        [SerializeField]
        private LayerMask _castMask;

        public Task TryAttack<UAttackData>(UAttackData attackData) where UAttackData : ICastAttackData<AttackDamage>
        {
            Vector3 castDirection = attackData.Displacement.normalized;
            float castDistance = attackData.Displacement.magnitude;

            Vector3 castOrigin = attackData.AttackBounds.center;

            foreach (RaycastHit2D hit in Physics2D.BoxCastAll(castOrigin, attackData.AttackBounds.size, 0f, castDirection, castDistance, _castMask))
                _ = hit.collider.TryGetComponent(out IDamageable<AttackDamage> damageable)
                    && damageable.TryTakeDamage(attackData.Data);

            return Task.CompletedTask;
        }
    }
}
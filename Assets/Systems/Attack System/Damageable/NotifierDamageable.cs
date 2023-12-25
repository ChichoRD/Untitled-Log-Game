using AttackSystem.Attack.Data;
using UnityEngine;

namespace AttackSystem.Damageable
{
    internal class NotifierDamageable : MonoBehaviour, IDamageable<AttackDamage>
    {
        public bool TryTakeDamage(AttackDamage damage)
        {
            Debug.Log("Took " + damage.Value + " damage");
            return true;
        }
    }
}
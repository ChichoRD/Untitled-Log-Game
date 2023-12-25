namespace AttackSystem.Damageable
{
    internal interface IDamageable<in TDamage>
    {
        bool TryTakeDamage(TDamage damage);
    }
}
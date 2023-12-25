namespace AttackSystem.Attack.Data
{
    internal readonly struct AttackDamage
    {
        public float Value { get; }

        public AttackDamage(float damage)
        {
            Value = damage;
        }
    }
}
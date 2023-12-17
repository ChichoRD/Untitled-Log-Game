namespace AttackSystem.Attacker
{
    internal interface IInputtableAttacker<in TInput>
    {
        bool SetInput(TInput input);
    }
}

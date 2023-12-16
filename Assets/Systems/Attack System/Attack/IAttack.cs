
namespace AttackSystem.Attack
{
    internal interface IAttack<in TAttackData>
    {
        bool TryAttack(TAttackData attackData);
    }
}

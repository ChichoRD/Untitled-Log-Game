using System.Threading.Tasks;

namespace AttackSystem.Attack
{
    internal interface IAttack<in TAttackData>
    {
        Task TryAttack<UAttackData>(UAttackData attackData)
            where UAttackData : TAttackData;
    }
}

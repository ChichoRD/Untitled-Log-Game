using AttackSystem.Attacker;
using InteractionSystem.Data.Response;

namespace AttackSystem.Interaction
{
    internal interface IAttackerProvisionResponse : IInteractionResponse
    {
        IAttacker GetAttacker();
    }
}

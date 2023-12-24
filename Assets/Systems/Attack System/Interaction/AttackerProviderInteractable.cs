using AttackSystem.Attacker;
using InteractionSystem.Interactable;
using UnityEngine;

namespace AttackSystem.Interaction
{
    internal class AttackerProviderInteractable : MonoBehaviour, IResponseOnlyInteractable<IAttackerProvisionResponse>
    {
        [SerializeField]
        private Transform _searchRoot;

        public IAttackerProvisionResponse TryInteract()
        {
            IAttacker attacker = _searchRoot.GetComponentInChildren<IAttacker>();
            return new AttackerProvisionResponse(attacker, attacker != null);
        }
    }
}
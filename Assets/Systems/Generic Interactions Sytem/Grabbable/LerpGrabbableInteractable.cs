using InteractionSystem.Interactable;
using UnityEngine;

namespace GenericInteractions.Grabbable
{
    internal class LerpGrabbableInteractable : MonoBehaviour, IRequestOnlyInteractable<IGrabRequestInfo>
    {
        public bool TryInteract(InteractionSystem.Data.IInteractionRequest<IGrabRequestInfo> interactionRequestInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}

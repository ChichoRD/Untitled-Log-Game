using InteractionSystem.Data.Request;
using InteractionSystem.Interactable;
using System.Linq;
using UnityEngine;

namespace GenericInteractions.Grabbable
{
    internal class LayeredGrabbableInteractable : MonoBehaviour, IInteractable<IGrabRequestInfo, IGrabResponse>, IRequestOnlyInteractable<IGrabRequestInfo>, ILayeredInteractable
    {
        private IInteractable<IGrabRequestInfo, IGrabResponse> _grabbableInteractable;
        [SerializeField]
        private InteractableLayer _interactableLayer;
        public InteractableLayer InteractableLayer => _interactableLayer;

        private void Start() => _grabbableInteractable = GetComponentsInChildren<IInteractable<IGrabRequestInfo, IGrabResponse>>()
                                                         .FirstOrDefault(g => g != (IInteractable<IGrabRequestInfo, IGrabResponse>)this);

        public IGrabResponse TryInteract(IInteractionRequest<IGrabRequestInfo> interactionRequest) => _grabbableInteractable.TryInteract(interactionRequest);

        bool IRequestOnlyInteractable<IGrabRequestInfo>.TryInteract(IInteractionRequest<IGrabRequestInfo> interactionRequestInfo) => TryInteract(interactionRequestInfo).Success;
    }
}

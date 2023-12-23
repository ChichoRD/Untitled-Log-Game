using InteractionSystem.Data.Request;
using InteractionSystem.Interactable;
using System.Linq;
using UnityEngine;

namespace GenericInteractions.Grabbable
{
    internal class Rigidbody2DConfigurerGrabbableInteractable : MonoBehaviour, IInteractable<IGrabRequestInfo, IGrabResponse>, IRequestOnlyInteractable<IGrabRequestInfo>
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private RigidbodyType2D _rigidbodyType;

        private IInteractable<IGrabRequestInfo, IGrabResponse> _interactable;

        private void Start()
        {
            _interactable = GetComponentsInChildren<IInteractable<IGrabRequestInfo, IGrabResponse>>().FirstOrDefault(i => i != (IInteractable<IGrabRequestInfo, IGrabResponse>)this);
        }

        public IGrabResponse TryInteract(IInteractionRequest<IGrabRequestInfo> interactionRequest)
        {
            IGrabResponse grabResponse = _interactable.TryInteract(interactionRequest);
            if (grabResponse.Success)
                _rigidbody.bodyType = _rigidbodyType;

            return grabResponse;
        }

        bool IRequestOnlyInteractable<IGrabRequestInfo>.TryInteract(IInteractionRequest<IGrabRequestInfo> interactionRequestInfo) =>
            TryInteract(interactionRequestInfo).Success;
    }
}

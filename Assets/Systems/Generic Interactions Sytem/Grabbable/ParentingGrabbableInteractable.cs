using InteractionSystem.Data.Request;
using InteractionSystem.Interactable;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace GenericInteractions.Grabbable
{
    internal class ParentingGrabbableInteractable : MonoBehaviour, IInteractable<IGrabRequestInfo, IGrabResponse>, IRequestOnlyInteractable<IGrabRequestInfo>
    {
        [SerializeField]
        private bool _parentBeforeGrab;

        [SerializeField]
        private Transform _transform;

        private IInteractable<IGrabRequestInfo, IGrabResponse> _grabbableInteractable;

        private void Start()
        {
            _grabbableInteractable = GetComponentsInChildren<IInteractable<IGrabRequestInfo, IGrabResponse>>().FirstOrDefault(g => g != (IInteractable<IGrabRequestInfo, IGrabResponse>)this);
        }

        public IGrabResponse TryInteract(IInteractionRequest<IGrabRequestInfo> interactionRequestInfo)
        {
            IGrabResponse grabResponse = _grabbableInteractable.TryInteract(interactionRequestInfo);
            if (grabResponse.Success)
                _ = StartCoroutine(_parentBeforeGrab
                    ? ParentBeforeGrabCoroutine(interactionRequestInfo.RequestInfo, grabResponse)
                    : ParentAfterGrabCoroutine(interactionRequestInfo.RequestInfo, grabResponse));

            return grabResponse;
        }

        bool IRequestOnlyInteractable<IGrabRequestInfo>.TryInteract(IInteractionRequest<IGrabRequestInfo> interactionRequestInfo) =>
            TryInteract(interactionRequestInfo).Success;

        private IEnumerator ParentBeforeGrabCoroutine(IGrabRequestInfo grabRequestInfo, IGrabResponse grabResponse)
        {
            _transform.SetParent(grabRequestInfo.GrabParent);
            yield return grabResponse.GrabCoroutine;
        }

        private IEnumerator ParentAfterGrabCoroutine(IGrabRequestInfo grabRequestInfo, IGrabResponse grabResponse)
        {
            yield return grabResponse.GrabCoroutine;
            _transform.SetParent(grabRequestInfo.GrabParent);
        }
    }
}

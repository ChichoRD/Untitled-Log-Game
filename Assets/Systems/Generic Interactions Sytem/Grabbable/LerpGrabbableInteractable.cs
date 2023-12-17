using InteractionSystem.Data;
using InteractionSystem.Interactable;
using System.Collections;
using UnityEngine;

namespace GenericInteractions.Grabbable
{
    internal class LerpGrabbableInteractable : MonoBehaviour, IInteractable<IGrabRequestInfo, IGrabResponse>, IRequestOnlyInteractable<IGrabRequestInfo>
    {
        [SerializeField]
        [Min(float.Epsilon)]
        private float _lerpTime;
        [SerializeField]
        private Transform _transform;

        private Coroutine _grabCoroutine;

        public IGrabResponse TryInteract(IInteractionRequest<IGrabRequestInfo> interactionRequestInfo) =>
            new GrabResponse(
                _grabCoroutine,
                TryStartGrabCoroutine(interactionRequestInfo.RequestInfo, ref _grabCoroutine)
                || (TryStopGrabCoroutine(ref _grabCoroutine)
                    && TryStartGrabCoroutine(interactionRequestInfo.RequestInfo, ref _grabCoroutine)));

        bool IRequestOnlyInteractable<IGrabRequestInfo>.TryInteract(IInteractionRequest<IGrabRequestInfo> interactionRequestInfo) =>
            TryInteract(interactionRequestInfo).Success;

        private bool TryStartGrabCoroutine(IGrabRequestInfo grabRequest, ref Coroutine grabCoroutine)
        {
            if (_grabCoroutine != null)
                return false;

            grabCoroutine = StartCoroutine(GrabCoroutine(grabRequest));
            return true;
        }

        private bool TryStopGrabCoroutine(ref Coroutine grabCoroutine)
        {
            if (_grabCoroutine == null)
                return false;

            StopCoroutine(_grabCoroutine);
            grabCoroutine = null;
            return true;
        }

        private IEnumerator GrabCoroutine(IGrabRequestInfo grabRequest)
        {
            Vector3 startPosition = transform.position;
            Quaternion startRotation = transform.rotation;

            float delta = Time.deltaTime / _lerpTime;
            for (float t = 0; t < 1.0f; t += delta)
            {
                float progress = grabRequest.GetGrabProgress(t);
                Vector3 targetPosition = Vector3.Lerp(startPosition, grabRequest.GrabParent.position, progress);
                Quaternion targetRotation = Quaternion.Slerp(startRotation, grabRequest.GrabParent.rotation, progress);

                _transform.SetPositionAndRotation(targetPosition, targetRotation);
                yield return null;
            }

            _transform.SetPositionAndRotation(grabRequest.GrabParent.position, grabRequest.GrabParent.rotation);
        }
    }
}

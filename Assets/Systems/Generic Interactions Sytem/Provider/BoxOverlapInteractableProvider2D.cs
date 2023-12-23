using InteractionSystem.Data.Response;
using InteractionSystem.Interactable;
using InteractionSystem.Provider;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GenericInteractions.Provider
{
    internal class BoxOverlapInteractableProvider2D : MonoBehaviour, IInteractableProvider
    {
        [SerializeField]
        private Bounds _boxCastBounds = new Bounds(Vector3.zero, Vector3.one);

        [SerializeField]
        private Transform _boxCastTransformation;

        [SerializeField]
        private LayerMask _layerMask = Physics.DefaultRaycastLayers;

        public IEnumerable<IInteractable<TInteractionRequestInfo, TInteractionResponse>> GetInteractables<TInteractionRequestInfo, TInteractionResponse>() where TInteractionResponse : IInteractionResponse
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(
                _boxCastTransformation.position + _boxCastBounds.center,
                _boxCastBounds.size,
                _boxCastTransformation.eulerAngles.z,
                _layerMask);

            return colliders
                .Select(collider => collider.GetComponentInChildren<IInteractable<TInteractionRequestInfo, TInteractionResponse>>())
                .Where(interactable => interactable != null);
        }

        private void OnDrawGizmosSelected()
        {
            if (_boxCastTransformation == null)
                Debug.LogWarning("BoxCastTransformation is null, please assign a transform to it.", this);

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(_boxCastTransformation.position + _boxCastBounds.center, _boxCastBounds.size);
        }
    }
}

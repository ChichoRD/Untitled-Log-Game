using GenericInteractions.Grabbable;
using InteractionSystem.Data;
using InteractionSystem.Data.Response;
using InteractionSystem.Handler;
using InteractionSystem.Handler.Observable;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AttackSystem.Attacker.Implementations.Inputted
{
    internal class PlayerInputtedDirectionalAttacker : MonoBehaviour
    {
        [SerializeField]
        private InputActionReference _attackAction;

        [SerializeField]
        private Camera _camera;

        private IList<IInteractableResponse<IGrabRequestInfo, IGrabResponse>> _grabResponses;
        private IObservableInteractionResponseHandler<IGrabRequestInfo, IGrabResponse> _observableInteractionResponseHandler;

        private void Awake()
        {
            _grabResponses = new List<IInteractableResponse<IGrabRequestInfo, IGrabResponse>>();
            _attackAction.action.performed += OnAttackActionPerformed;
        }

        private void OnEnable()
        {
            _attackAction.action.Enable();
        }

        private void Start()
        {
            _observableInteractionResponseHandler = GetComponentInChildren<IObservableInteractionResponseHandler<IGrabRequestInfo, IGrabResponse>>();

            _observableInteractionResponseHandler.InteractionResponseProcessed += OnInteractionResponseProcessed;
        }

        private void OnDisable()
        {
            _attackAction.action.Disable();
        }

        private void OnDestroy()
        {
            _observableInteractionResponseHandler.InteractionResponseProcessed -= OnInteractionResponseProcessed;
            _attackAction.action.performed -= OnAttackActionPerformed;
        }

        private void OnInteractionResponseProcessed(IInteractableResponse<IGrabRequestInfo, IGrabResponse> interactionResponse) =>
            _grabResponses.Add(interactionResponse);

        private void OnAttackActionPerformed(InputAction.CallbackContext context)
        {
            IList<IInteractableResponse<IGrabRequestInfo, IGrabResponse>> ungrabbedInteractables = new List<IInteractableResponse<IGrabRequestInfo, IGrabResponse>>();
            foreach (IInteractableResponse<IGrabRequestInfo, IGrabResponse> grabResponse in _grabResponses)
            {
                if (!grabResponse.Response.Success
                    || !grabResponse.Response.IsGrabbed
                    || grabResponse.Interactable is not MonoBehaviour behaviour)
                {
                    ungrabbedInteractables.Add(grabResponse);
                    continue;
                }

                IInputtableAttacker<Vector2> inputtableAttacker = behaviour.GetComponentInChildren<IInputtableAttacker<Vector2>>();
                if (inputtableAttacker != null)
                {
                    Vector2 mousePosition = _attackAction.action.ReadValue<Vector2>();
                    Vector3 worldMousePosition = _camera.ScreenToWorldPoint(mousePosition);
                    const float Z_DEPTH = 0.0f;
                    worldMousePosition.z = Z_DEPTH;

                    Vector3 direction = (worldMousePosition - transform.position).normalized;
                    inputtableAttacker.SetInput(direction);
                }

                IAttacker attacker = behaviour.GetComponentInChildren<IAttacker>();
                if (attacker != null)
                    attacker.TryAttack();
            }

            foreach (IInteractableResponse<IGrabRequestInfo, IGrabResponse> ungrabbedInteractable in ungrabbedInteractables)
                _grabResponses.Remove(ungrabbedInteractable);
        }
    }
}
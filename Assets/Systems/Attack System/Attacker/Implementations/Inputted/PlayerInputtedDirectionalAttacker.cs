using AttackSystem.Interaction;
using GenericInteractions.Grabbable;
using InteractionSystem.Data.Response;
using InteractionSystem.Handler.Observable;
using InteractionSystem.Interactable;
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
                if (!IsSuitableForAttacker(grabResponse, out IResponseOnlyInteractable<IAttackerProvisionResponse> attackerProvider))
                {
                    ungrabbedInteractables.Add(grabResponse);
                    continue;
                }

                IAttackerProvisionResponse attackerProvisionResponse = attackerProvider.TryInteract();
                _ = attackerProvisionResponse.Success
                    && TryAttackWith(attackerProvisionResponse.GetAttacker(), context);
            }

            foreach (IInteractableResponse<IGrabRequestInfo, IGrabResponse> ungrabbedInteractable in ungrabbedInteractables)
                _grabResponses.Remove(ungrabbedInteractable);
        }

        private bool TryAttackWith(IAttacker attacker, InputAction.CallbackContext context)
        {
            if (attacker is MonoBehaviour attackerBehaviour
                && TryGetComponentInChildren(attackerBehaviour, out IInputtableAttacker<Vector2> inputtableAttacker))
            {
                Vector2 attackDirection = context.control.device is Pointer
                    ? AttackDirectionFromCameraPointer(_camera, context.ReadValue<Vector2>())
                    : context.ReadValue<Vector2>();

                inputtableAttacker.SetInput(attackDirection);
            }

            return attacker.TryAttack();
        }

        private static Vector2 AttackDirectionFromCameraPointer(Camera camera, Vector2 pointerPosition, float zDepth = 0.0f)
        {
            Vector3 worldPointerPosition = camera.ScreenToWorldPoint(pointerPosition);
            worldPointerPosition.z = zDepth;

            Vector3 direction = (worldPointerPosition - camera.transform.position).normalized;
            return direction;
        }

        private static bool TryGetComponentInChildren<T>(MonoBehaviour behaviour, out T component)
        {
            component = behaviour.GetComponentInChildren<T>();
            return component != null;
        }

        private static bool IsSuitableForAttacker(IInteractableResponse<IGrabRequestInfo, IGrabResponse> grabResponse, out IResponseOnlyInteractable<IAttackerProvisionResponse> attackerProvider)
        {
            attackerProvider = default;

            return grabResponse.Response.Success
                    && grabResponse.Response.IsGrabbed
                    && grabResponse.Interactable is MonoBehaviour behaviour
                    && behaviour.TryGetComponent(out attackerProvider);
        }
    }
}
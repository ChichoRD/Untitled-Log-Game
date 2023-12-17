using InteractionSystem.Data;
using InteractionSystem.Handler;
using InteractionSystem.Interactable;
using InteractionSystem.Provider;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenericInteractions.Grabbable
{
    public class PlayerInputtedInteractionHandler : MonoBehaviour
    {
        [SerializeField]
        private InputActionReference _interactAction;

        [SerializeField]
        private AnimationCurveGrabRequestInfo _grabRequestInfo;

        private IInteractableProvider _interactableProvider;
        private IInteractionHandler _interactionHandler;

        private void Awake()
        {
            _interactAction.action.performed += OnInteractionActionPerformed;
        }

        private void OnEnable()
        {
            _interactAction.action.Enable();
        }

        private void Start()
        {
            _interactionHandler = GetComponentsInChildren<IInteractionHandler>().FirstOrDefault();
            _interactableProvider = GetComponentsInChildren<IInteractableProvider>().FirstOrDefault();
        }

        private void OnDisable()
        {
            _interactAction.action.Disable();
        }

        private void OnDestroy()
        {
            _interactAction.action.performed -= OnInteractionActionPerformed;
        }

        private void OnInteractionActionPerformed(InputAction.CallbackContext context)
        {
            foreach (IRequestOnlyInteractable<IGrabRequestInfo> interactable in _interactableProvider.GetRequestOnlyInteractables<IGrabRequestInfo>())
                _interactionHandler.HandleInteraction(interactable, new InteractionRequest<IGrabRequestInfo>(_grabRequestInfo));
        }
    }
}

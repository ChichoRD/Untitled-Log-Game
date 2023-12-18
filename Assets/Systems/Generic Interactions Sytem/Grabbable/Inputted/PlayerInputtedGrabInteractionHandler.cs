using InteractionSystem.Data;
using InteractionSystem.Handler;
using InteractionSystem.Interactable;
using InteractionSystem.Provider;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenericInteractions.Grabbable
{
    public class PlayerInputtedGrabInteractionHandler : MonoBehaviour
    {
        [SerializeField]
        private InputActionReference _grabInteractAction;

        [SerializeField]
        private AnimationCurveGrabRequestInfo _grabRequestInfo;

        private IInteractableProvider _interactableProvider;
        private IInteractionHandler _interactionHandler;

        private void Awake()
        {
            _grabInteractAction.action.performed += OnInteractionActionPerformed;
        }

        private void OnEnable()
        {
            _grabInteractAction.action.Enable();
        }

        private void Start()
        {
            _interactionHandler = GetComponentsInChildren<IInteractionHandler>().FirstOrDefault();
            _interactableProvider = GetComponentsInChildren<IInteractableProvider>().FirstOrDefault();
        }

        private void OnDisable()
        {
            _grabInteractAction.action.Disable();
        }

        private void OnDestroy()
        {
            _grabInteractAction.action.performed -= OnInteractionActionPerformed;
        }

        private void OnInteractionActionPerformed(InputAction.CallbackContext context)
        {
            foreach (IRequestOnlyInteractable<IGrabRequestInfo> interactable in _interactableProvider.GetRequestOnlyInteractables<IGrabRequestInfo>())
                _interactionHandler.HandleInteraction(interactable, new InteractionRequest<IGrabRequestInfo>(_grabRequestInfo));
        }
    }
}

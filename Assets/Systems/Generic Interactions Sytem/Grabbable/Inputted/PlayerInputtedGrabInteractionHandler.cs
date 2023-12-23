using InteractionSystem.Data.Request;
using InteractionSystem.Data.Response;
using InteractionSystem.Handler;
using InteractionSystem.Handler.History;
using InteractionSystem.Handler.Observable;
using InteractionSystem.Interactable;
using InteractionSystem.Interactor;
using InteractionSystem.Provider;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenericInteractions.Grabbable
{
    public class PlayerInputtedGrabInteractionHandler : MonoBehaviour,
        IInteractionHistoryService<IGrabRequestInfo, IGrabResponse>,
        IObservableInteractionRequestResponseHandler<IGrabRequestInfo, IGrabResponse>
    {
        [SerializeField]
        private InputActionReference _grabInteractAction;

        [SerializeField]
        private AnimationCurveGrabRequestInfo _grabRequestInfo;

        private IInteractableProvider _interactableProvider;
        private HistoricalInteractionHandler<InteractionNotifierHandler<IGrabRequestInfo, IGrabResponse>, IGrabRequestInfo, IGrabResponse> _interactionHandler;
        private IInteractionHandler<IGrabRequestInfo, IGrabResponse> InteractionHandler => _interactionHandler;

        private IInteractionHistoryService<IGrabRequestInfo, IGrabResponse> InteractionHistoryService => _interactionHandler;
        private IObservableInteractionRequestResponseHandler<IGrabRequestInfo, IGrabResponse> ObservableInteractionRequestResponseHandler => _interactionHandler;

        public IReadOnlyCollection<KeyValuePair<IInteractorRequest<IGrabRequestInfo, IInteractor<IGrabResponse>, IGrabResponse>, IList<IInteractableResponse<IGrabRequestInfo, IGrabResponse>>>> InteractionHistory => InteractionHistoryService.InteractionHistory;

        public event InteractionRequestResponseHandler<IGrabRequestInfo, IGrabResponse> InteractionRequestResponseProcessed
        {
            add => ObservableInteractionRequestResponseHandler.InteractionRequestResponseProcessed += value;

            remove => ObservableInteractionRequestResponseHandler.InteractionRequestResponseProcessed -= value;
        }

        private void Awake()
        {
            IInteractionHandler interactionHandler = GetComponentsInChildren<IInteractionHandler>().FirstOrDefault();

            InteractionNotifierHandler<IGrabRequestInfo, IGrabResponse> notifierHandler = new InteractionNotifierHandler<IGrabRequestInfo, IGrabResponse>(interactionHandler);
            _interactionHandler = new HistoricalInteractionHandler<InteractionNotifierHandler<IGrabRequestInfo, IGrabResponse>, IGrabRequestInfo, IGrabResponse>(notifierHandler);

            _grabInteractAction.action.performed += OnInteractionActionPerformed;
        }

        private void OnEnable()
        {
            _grabInteractAction.action.Enable();
        }

        private void Start()
        {
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
                InteractionHandler.HandleInteraction(interactable, new InteractionRequest<IGrabRequestInfo>(_grabRequestInfo));
        }
    }
}
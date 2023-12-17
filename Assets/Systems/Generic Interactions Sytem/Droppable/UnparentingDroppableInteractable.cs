using InteractionSystem.Interactable;
using System.Linq;
using UnityEngine;

namespace GenericInteractions.Droppable
{
    internal class UnparentingDroppableInteractable : MonoBehaviour, IAgnosticInteractable
    {
        [SerializeField]
        private Transform _transform;

        private IAgnosticInteractable _droppableInteractable;

        private void Start()
        {
            _droppableInteractable = GetComponentsInChildren<IAgnosticInteractable>().FirstOrDefault(d => d != (IAgnosticInteractable)this);
        }

        public bool TryInteract()
        {
            bool dropped = _droppableInteractable.TryInteract();
            //TODO - fix recursion
            if (dropped)
                _transform.SetParent(null);
            return dropped;
        }
    }
}

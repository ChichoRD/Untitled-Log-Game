using InteractionSystem.Interactable;
using System.Linq;
using UnityEngine;

namespace GenericInteractions.Droppable
{
    internal class Rigidbody2DConfigurerDroppableInteractable : MonoBehaviour, IAgnosticInteractable
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private RigidbodyType2D _rigidbodyType;

        private IAgnosticInteractable _droppableInteractable;

        private void Start()
        {
            _droppableInteractable = GetComponentsInChildren<IAgnosticInteractable>().FirstOrDefault(d => d != (IAgnosticInteractable)this);
        }

        public bool TryInteract()
        {
            bool dropped = _droppableInteractable.TryInteract();
            if (dropped)
                _rigidbody.bodyType = _rigidbodyType;

            return dropped;
        }
    }
}

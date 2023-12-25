using InteractionSystem.Provider;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace AttackSystem.Attack.Implementations.Throwable
{
    internal class DroppableThrowableAttack : MonoBehaviour, IAttack<IVelocityAttackData>
    {
        private IAttack<IVelocityAttackData> _throwableAttack;
        private IInteractableProvider _interactableProvider;

        [SerializeField]
        private bool _cancelAttackOnDropFailure;

        private void Start()
        {
            _throwableAttack = GetComponentsInChildren<IAttack<IVelocityAttackData>>().FirstOrDefault(a => a != (IAttack<IVelocityAttackData>)this);
            _interactableProvider = GetComponentInChildren<IInteractableProvider>();
        }

        public Task TryAttack<UAttackData>(UAttackData attackData) where UAttackData : IVelocityAttackData =>
            (!_interactableProvider.GetAgnosticInteractables().FirstOrDefault().TryInteract() && _cancelAttackOnDropFailure)
            ? Task.CompletedTask
            : _throwableAttack.TryAttack(attackData);
    }
}

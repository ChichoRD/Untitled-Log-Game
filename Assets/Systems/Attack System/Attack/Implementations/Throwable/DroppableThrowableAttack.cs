using InteractionSystem.Provider;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace AttackSystem.Attack.Implementations.Throwable
{
    internal class DroppableThrowableAttack : MonoBehaviour, IAttack<VelocityAttackData>
    {
        private IAttack<VelocityAttackData> _throwableAttack;
        private IInteractableProvider _interactableProvider;

        [SerializeField]
        private bool _cancelAttackOnDropFailure;

        private void Start()
        {
            _throwableAttack = GetComponentsInChildren<IAttack<VelocityAttackData>>().FirstOrDefault(a => a != (IAttack<VelocityAttackData>)this);
            _interactableProvider = GetComponentInChildren<IInteractableProvider>();
        }

        public Task TryAttack(VelocityAttackData attackData) =>
            (!_interactableProvider.GetAgnosticInteractables().FirstOrDefault().TryInteract() && _cancelAttackOnDropFailure)
            ? Task.CompletedTask
            : _throwableAttack.TryAttack(attackData);
    }
}

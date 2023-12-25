using UnityEngine;

namespace AttackSystem.Attack.Implementations.Throwable
{
    internal readonly struct VelocityAttackData : IVelocityAttackData
    {
        public Vector3 VelocityIncrement { get; }
        public VelocityAttackData(Vector3 velocityIncrement)
        {
            VelocityIncrement = velocityIncrement;
        }
    }
}

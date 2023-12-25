using UnityEngine;

namespace AttackSystem.Attack.Implementations.Throwable
{
    internal interface IVelocityAttackData
    {
        Vector3 VelocityIncrement { get; }
    }

    internal interface IVelocityAttackData<out T> : IVelocityAttackData
    {
        T Data { get; }
    }
}

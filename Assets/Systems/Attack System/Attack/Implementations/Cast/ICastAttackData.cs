using UnityEngine;

namespace AttackSystem.Attack.Implementations.Cast
{
    internal interface ICastAttackData<out T>
    {
        Bounds AttackBounds { get; }
        Vector3 Displacement { get; }
        T Data { get; }
    }
}

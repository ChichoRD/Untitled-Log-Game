using UnityEngine;

namespace AttackSystem.Attack.Implementations.Cast
{
    internal readonly struct CastAttackData<T> : ICastAttackData<T>
    {
        public Bounds AttackBounds { get; }
        public Vector3 Displacement { get; }
        public T Data { get; }

        public CastAttackData(Bounds attackBounds, Vector3 displacement, T data)
        {
            AttackBounds = attackBounds;
            Displacement = displacement;
            Data = data;
        }
    }
}

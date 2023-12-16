using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AttackSystem.Attack.Implementations.Throwable
{
    internal readonly struct VelocityAttackData
    {
        public Vector3 VelocityIncrement { get; }
        public VelocityAttackData(Vector3 velocityIncrement)
        {
            VelocityIncrement = velocityIncrement;
        }
    }
}

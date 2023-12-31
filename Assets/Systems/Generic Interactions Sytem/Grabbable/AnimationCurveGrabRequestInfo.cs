﻿using System;
using UnityEngine;

namespace GenericInteractions.Grabbable
{
    [Serializable]
    internal struct AnimationCurveGrabRequestInfo : IGrabRequestInfo
    {
        [field: SerializeField]
        public Transform GrabParent { get; private set; }
        [field: SerializeField]
        public AnimationCurve GrabProgressCurve { get; private set; }
        [field: SerializeField]
        [field: Min(float.Epsilon)]
        public float GrabTime { get; private set; }
        public readonly float GetGrabProgress(float grabTime) => GrabProgressCurve.Evaluate(grabTime / GrabTime);
    }
}

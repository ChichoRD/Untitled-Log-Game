using UnityEngine;

namespace GenericInteractions.Grabbable
{
    internal interface IGrabRequestInfo
    {
        Transform GrabParent { get; }
        float GetLerpProgress(float grabTime);
    }
}

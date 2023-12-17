using UnityEngine;

namespace GenericInteractions.Grabbable
{
    internal interface IGrabRequestInfo
    {
        Transform GrabParent { get; }
        float GetGrabProgress(float grabTime);
    }
}

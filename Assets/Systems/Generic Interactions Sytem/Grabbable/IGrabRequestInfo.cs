using UnityEngine;

namespace GenericInteractions.Grabbable
{
    public interface IGrabRequestInfo
    {
        Transform GrabParent { get; }
        float GetGrabProgress(float grabTime);
    }
}

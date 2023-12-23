using InteractionSystem.Data.Response;
using UnityEngine;

namespace GenericInteractions.Grabbable
{
    public interface IGrabResponse : IInteractionResponse
    {
        Coroutine GrabCoroutine { get; }
        bool IsGrabbed { get; }
    }
}

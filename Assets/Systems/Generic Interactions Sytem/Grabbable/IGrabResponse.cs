using InteractionSystem.Data;
using UnityEngine;

namespace GenericInteractions.Grabbable
{
    internal interface IGrabResponse : IInteractionResponse
    {
        Coroutine GrabCoroutine { get; }
    }
}

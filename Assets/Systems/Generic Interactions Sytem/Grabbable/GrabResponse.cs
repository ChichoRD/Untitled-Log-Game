using UnityEngine;

namespace GenericInteractions.Grabbable
{
    internal readonly struct GrabResponse : IGrabResponse
    {
        public Coroutine GrabCoroutine { get; }

        public bool Success { get; }

        public GrabResponse(Coroutine grabCoroutine, bool success)
        {
            GrabCoroutine = grabCoroutine;
            Success = success;
        }
    }
}

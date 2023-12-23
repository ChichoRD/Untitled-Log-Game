using System;
using UnityEngine;

namespace GenericInteractions.Grabbable
{
    internal readonly struct GrabResponse : IGrabResponse
    {
        private readonly Func<bool> _isGrabbed;

        public Coroutine GrabCoroutine { get; }
        public bool IsGrabbed { get => _isGrabbed(); }
        public bool Success { get; }

        public GrabResponse(Coroutine grabCoroutine, Func<bool> isGrabbed, bool success)
        {
            GrabCoroutine = grabCoroutine;
            _isGrabbed = isGrabbed;
            Success = success;
        }
    }
}

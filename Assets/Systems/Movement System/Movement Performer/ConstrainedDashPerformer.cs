using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(DashPerformer))]
public class ConstrainedDashPerformer : MonoBehaviour 
{
    [SerializeField]
    private DashPerformer _dashPerformer;

    [SerializeField]
    private float _dashCoolDown = 2.5f;

    private readonly Stopwatch _stopwatch = new Stopwatch();
    public bool TryDash(Vector2 dashAcceleration)
    {
        if(_stopwatch.Elapsed.TotalSeconds > _dashCoolDown)
        {
            if (_dashPerformer.TryDash(dashAcceleration))
            {
                _stopwatch.Restart();
            }

            return true;
        }
        return false; 
    }

    private void Awake()
    {
        _stopwatch.Start();
    }
}


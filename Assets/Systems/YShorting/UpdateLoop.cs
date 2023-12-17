using System;
using System.Collections;
using UnityEngine;

public class UpdateLoop
{
    public const float FPS60 = 0.016666666f;
    public const float FPS50 = 0.02f;
    public const float FPS30 = 0.033333333f;
    public const float FPS15 = 0.066666666f;
    public const float FPS10 = 0.1f;

    private readonly MonoBehaviour @object;
    private readonly WaitForSeconds refreshWait;
    private readonly Action onUpdate;
    private Coroutine update = null;

    public UpdateLoop(float timeStep, MonoBehaviour @object, Action onUpdate)
    {
        this.@object = @object;
        this.onUpdate = onUpdate ?? new Action(() => { });
        refreshWait = new WaitForSeconds(timeStep);
    }

    public void StartUpdate() => update ??= @object.StartCoroutine(Update());

    private IEnumerator Update()
    {
        while (@object != null && @object.gameObject.activeSelf)
        {
            onUpdate.Invoke();
            yield return refreshWait;
        }

        update = null;
    }
}

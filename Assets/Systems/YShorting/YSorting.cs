#define EDITOR_SORTING

using System;
using UnityEngine;

#if EDITOR_SORTING
[ExecuteInEditMode]
#endif
public class YSorting : MonoBehaviour
{
    [field: SerializeField] public int SortOrder { get; private set; } = 5000;
    public float yBias = 0f;

    private Renderer rend;
    private Canvas canvas;

    private UpdateLoop loop;
    private Action sortAction = null;
    private const int RESOLUTION = 10;
    private const float GIZMOS_SCALE = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        canvas = GetComponentInChildren<Canvas>();

        sortAction += rend == null ? null : () => rend.sortingOrder = SortingOrder();
        sortAction += canvas == null ? null : () => canvas.sortingOrder = SortingOrder();

        loop = new UpdateLoop(UpdateLoop.FPS15, this, CalculateSortingOrder);
        loop.StartUpdate();

        if (rend == null && canvas == null)
        {
            Destroy(this);
            return;
        } 

        if (gameObject.isStatic && Application.isPlaying)
        {
            CalculateSortingOrder();
            Destroy(this);
            return;
        }
            
    }

    //private void Update() => CalculateSortingOrder();

    public void CalculateSortingOrder() => sortAction?.Invoke();

    private int SortingOrder() => SortOrder - Mathf.FloorToInt((transform.position.y + yBias) * RESOLUTION);

    private void OnDrawGizmosSelected() => Gizmos.DrawSphere(transform.position + Vector3.up * yBias, GIZMOS_SCALE);

    public void SetSortingOrder(int order) => SortOrder = order;
}

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CircleArea))]
public class CircleAreaEditor : Editor
{
    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.Selected | GizmoType.Pickable)]

    public static void RenderCustomGizmo(CircleArea area, GizmoType gizmo)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(area.SpawnCenter, area.Radius);
    }
}

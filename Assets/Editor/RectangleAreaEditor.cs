using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RectangleArea))]
public class RectangleAreaEditor : Editor
{
    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.Selected | GizmoType.Pickable)]
    public static void RenderCustomGizmo(RectangleArea area, GizmoType gizmo)
    {
        Gizmos.color = Color.red;
        //Left bound
        Gizmos.DrawLine(area.SpawnCenter + new Vector2(-area.HalfWidth, -area.HalfHeight),
            area.SpawnCenter + new Vector2(-area.HalfWidth, area.HalfHeight));
        //Right bound
        Gizmos.DrawLine(area.SpawnCenter + new Vector2(area.HalfWidth, -area.HalfHeight),
            area.SpawnCenter + new Vector2(area.HalfWidth, area.HalfHeight));
        //Upper bound
        Gizmos.DrawLine(area.SpawnCenter + new Vector2(-area.HalfWidth, area.HalfHeight),
            area.SpawnCenter + new Vector2(area.HalfWidth, area.HalfHeight));
        //Bottom bound
        Gizmos.DrawLine(area.SpawnCenter + new Vector2(-area.HalfWidth, -area.HalfHeight),
            area.SpawnCenter + new Vector2(area.HalfWidth, -area.HalfHeight));
    }
}

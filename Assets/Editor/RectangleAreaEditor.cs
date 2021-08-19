using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RectangleArea))]
public class RectangleAreaEditor : Editor
{
    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.Selected | GizmoType.Pickable)]
    public static void RenderCustomGizmo(RectangleArea area, GizmoType gizmo)
    {
        var leftBottomPoint = area.SpawnCenter +
                              area.RotatePositionAroundCenter(new Vector2(-area.HalfWidth, -area.HalfHeight));
        var leftUpperPoint = area.SpawnCenter +
                             area.RotatePositionAroundCenter(new Vector2(-area.HalfWidth, area.HalfHeight));
        var rightBottomPoint = area.SpawnCenter +
                               area.RotatePositionAroundCenter(new Vector2(area.HalfWidth, -area.HalfHeight));
        var rightUpperPoint = area.SpawnCenter +
                              area.RotatePositionAroundCenter(new Vector2(area.HalfWidth, area.HalfHeight));

        Gizmos.color = Color.red;
        Gizmos.DrawLine(leftBottomPoint, leftUpperPoint);
        Gizmos.DrawLine(leftBottomPoint, rightBottomPoint);
        Gizmos.DrawLine(leftUpperPoint, rightUpperPoint);
        Gizmos.DrawLine(rightBottomPoint, rightUpperPoint);
    }
}
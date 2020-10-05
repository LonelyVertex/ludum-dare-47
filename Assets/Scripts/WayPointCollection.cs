using System;
using Malee.List;
using UnityEditor;
using UnityEngine;

public class WayPointCollection : MonoBehaviour
{
    public enum Mode
    {
        Loop = 0,
        PingPong = 1,
    }

    [Serializable]
    public class TransformList : ReorderableArray<Transform>
    {
    }

    public Color color = Color.cyan;
    public Mode mode;
    [Reorderable] public TransformList wayPoints;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (Selection.activeGameObject != gameObject) return;
        DrawCollectionGizmos();
    }

    public void DrawCollectionGizmos()
    {
        Handles.color = color;
        for (var i = 0; i < wayPoints.Count; i++)
        {
            var size = i == 0 ? 0.4f : 0.2f;
            Handles.DrawSolidDisc(GetPosition(i), transform.forward, size);

            if (i > 0)
            {
                Handles.DrawLine(GetPosition(i), GetPosition(i - 1));
            }
            else if (mode == Mode.Loop)
            {
                Handles.DrawDottedLine(GetPosition(i), GetPosition(wayPoints.Count - 1), 2);
            }
        }
    }

#endif

    public Vector3 GetPosition(int i)
    {
        return wayPoints[i].transform.position;
    }
}
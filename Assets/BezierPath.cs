using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class BezierPath : MonoBehaviour
{
    public Vector3[] controlPoints = new Vector3[4];
    private Vector2 gizmosPosition;

    public Vector2 GetPosAlongPath2D(float pctTraveled, Vector3 centerPos, float widthRatio = 1f, float heightRatio = 1f)
    {
        Vector3[] adjustedControlPoints = new Vector3[4];
        System.Array.Copy(controlPoints, adjustedControlPoints, 4);
        for(int i = 0; i < 4; ++i)
        {
            adjustedControlPoints[i].x *= widthRatio;
            adjustedControlPoints[i].y *= heightRatio;
        }
        return Mathf.Pow(1 - pctTraveled, 3) * (adjustedControlPoints[0] + centerPos) + 3 * Mathf.Pow(1 - pctTraveled, 2) * pctTraveled * (adjustedControlPoints[1] + centerPos) + 3 * (1 - pctTraveled) * Mathf.Pow(pctTraveled, 2) * (adjustedControlPoints[2] + centerPos) + Mathf.Pow(pctTraveled, 3) * (adjustedControlPoints[3] + centerPos);
    }

    private void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t += 0.02f)
        {
            Gizmos.DrawWireSphere(GetPosAlongPath2D(t, transform.position), 0.02f);
        }

        Gizmos.DrawLine(new Vector2(controlPoints[0].x + transform.position.x, controlPoints[0].y + transform.position.y), new Vector2(controlPoints[1].x + transform.position.x, controlPoints[1].y + transform.position.y));
        Gizmos.DrawLine(new Vector2(controlPoints[2].x + transform.position.x, controlPoints[2].y + transform.position.y), new Vector2(controlPoints[3].x + transform.position.x, controlPoints[3].y + transform.position.y));

    }

    void OnValidate()
    {
        if (this.controlPoints.Length != 4)
        {
            Debug.LogWarning("BezierPath must have exactly 4 control points.");
            Array.Resize(ref controlPoints, 4);
        }
    }

    [CustomEditor(typeof(BezierPath))]
    public class BezierPathEditor : Editor
    {
        protected virtual void OnSceneGUI()
        {
            BezierPath path = (BezierPath)target;

            for(int i = 0; i < path.controlPoints.Length; ++i)
            {
                EditorGUI.BeginChangeCheck();
                Vector3 pos = Handles.PositionHandle(path.transform.position + path.controlPoints[i], Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(path, "Change control point");
                    path.controlPoints[i] = pos - path.transform.position;
                }
            }
            
        }
    }
}

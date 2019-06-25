using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(coinControllerScript))]
public class coin_labels : Editor
{
     [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
     static void DrawHandles(coinControllerScript coinScript, GizmoType gizmoType)
     {
        GUIStyle style = new GUIStyle(); 
        Handles.ArrowCap(0,
        coinScript.transform.position,
        coinScript.transform.rotation*Quaternion.LookRotation(Vector3.right),
        1f
        );
     }
}

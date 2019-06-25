using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(asteroidControllerScript))]
public class asteroid_labels : Editor
{
     [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
     static void DrawHandles(asteroidControllerScript asteroidScript, GizmoType gizmoType)
     {
        GUIStyle style = new GUIStyle(); 
        Handles.ArrowCap(0,
        asteroidScript.transform.position,
        asteroidScript.transform.rotation*Quaternion.LookRotation(Vector3.right),
        1f
        );
     }
}

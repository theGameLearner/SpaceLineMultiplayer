using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Nodes))]
public class nodeLabels : Editor
{
    
   [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
     static void DrawHandles(Nodes node, GizmoType gizmoType)
     {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;
        style.fontSize = 15;            
        Handles.Label(node.transform.position, node.name, style); 
     }
}



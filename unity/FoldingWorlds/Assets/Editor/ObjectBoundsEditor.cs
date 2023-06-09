using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static UnityEngine.GraphicsBuffer;


[CustomEditor(typeof(Transform))]
public class ObjectBoundsEditor : Editor
{
    /*
    public override void OnInspectorGUI()
    {
        Transform transform = (Transform)target;
        Renderer renderer = transform.GetComponent<Renderer>();

        // Only show bounding box size if there is a Renderer component
        if (renderer != null)
        {
            Vector3 boundingBoxSize = renderer.bounds.size;
            EditorGUILayout.LabelField("Bounding Box Size", boundingBoxSize.ToString());
        }

        // Draw the rest of the inspector as usual
        DrawDefaultInspector();
    }*/
}


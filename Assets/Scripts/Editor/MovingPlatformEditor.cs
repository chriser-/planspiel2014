using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MovingPlatform))]
[CanEditMultipleObjects]
public class MovingPlatformEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MovingPlatform platform = (MovingPlatform)target;

        if (GUILayout.Button("Set Start to current position"))
            platform.startPoint = platform.gameObject.transform.position;
        if (GUILayout.Button("Set End to current position"))
            platform.endPoint = platform.gameObject.transform.position;
    }
}

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShadowCaster2DFromComposite))]
public class ShadowCastersGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ShadowCaster2DFromComposite generator = (ShadowCaster2DFromComposite)target;

        for(int i = 0; i < 3; i++)
            EditorGUILayout.Space();

        if (GUILayout.Button("Generate"))
            generator.Generate();

        EditorGUILayout.Space();
        if (GUILayout.Button("Destroy All Children"))
            generator.DestroyAllChildren();
    }
}
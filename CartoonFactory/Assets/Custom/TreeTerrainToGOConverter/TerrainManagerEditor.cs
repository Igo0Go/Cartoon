using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
    

[CustomEditor(typeof(TerrainManager))]
public class TerrainManagerEditor : Editor
{
    TerrainManager tc;

    private void OnEnable()
    {
        tc = (TerrainManager)target;
    }

    public override void OnInspectorGUI()
    {
        tc.spawnTreeObject = (GameObject)EditorGUILayout.ObjectField("Дерево, которое будет ставится", tc.spawnTreeObject, typeof(GameObject), false);
            if (GUILayout.Button("Change trees")) tc.Correct();
        if (GUILayout.Button("Delete all trees")) tc.Delete();
    }
}

#endif

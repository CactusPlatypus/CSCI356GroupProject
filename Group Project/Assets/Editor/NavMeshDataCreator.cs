using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

[CustomEditor(typeof(NavMeshUpdateOnEnable))]
public class NavMeshDataCreator : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        NavMeshUpdateOnEnable myScript = (NavMeshUpdateOnEnable)target;
        GameObject go = myScript.gameObject;
        if (GUILayout.Button("BuildMesh"))
        {

            NavMeshBuildSettings buildSettings = new NavMeshBuildSettings();
            NavMeshBuildSource source = new NavMeshBuildSource();
            source.sourceObject = go.GetComponent<MeshFilter>().sharedMesh;
            //source.sourceObject = go;

            List<NavMeshBuildSource> sources = new List<NavMeshBuildSource>() { source };

            Bounds bounds = new Bounds();
            bounds.center = go.transform.position;
            bounds.size = go.transform.localScale * 10;

            myScript.m_NavMeshData = NavMeshBuilder.BuildNavMeshData(
                buildSettings, 
                sources, 
                bounds, 
                go.transform.position, 
                go.transform.rotation);

            AssetDatabase.CreateAsset(myScript.m_NavMeshData, "Assets\\MeshData\\" + go.name + ".asset");
            //myScript.gameObject
        }
    }
}

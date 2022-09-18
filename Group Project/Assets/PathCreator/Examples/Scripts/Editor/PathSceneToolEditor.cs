using UnityEngine;
using UnityEditor;
using PathCreation;
using System.IO;
using UnityEditor.SceneManagement;

namespace PathCreation.Examples
{
    [CustomEditor(typeof(PathSceneTool), true)]
    public class PathSceneToolEditor : Editor
    {
        protected PathSceneTool pathTool;
        bool isSubscribed;

        public override void OnInspectorGUI()
        {
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                DrawDefaultInspector();

                if (check.changed)
                {
                    if (!isSubscribed)
                    {
                        TryFindPathCreator();
                        Subscribe();
                    }

                    if (pathTool.autoUpdate)
                    {
                        TriggerUpdate();

                    }
                }
            }

            if (GUILayout.Button("Manual Update"))
            {
                if (TryFindPathCreator())
                {
                    TriggerUpdate();
                    SceneView.RepaintAll();
                }
            }

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Bake Road"))
            {
                if (TryFindPathCreator())
                {
                    // Get road mesh from path tool, assuming Road Mesh Creator script was added
                    Mesh mesh = pathTool.GetMesh();
                    MeshUtility.Optimize(mesh);
                    
                    // Create folder for storing baked road prefabs
                    if (!Directory.Exists("Assets/Prefabs/BakedRoads"))
                    {
                        AssetDatabase.CreateFolder("Assets/Prefabs", "BakedRoads");
                    }

                    // Save mesh into folder
                    string name = pathTool.gameObject.name;
                    AssetDatabase.CreateAsset(mesh, "Assets/Prefabs/BakedRoads/" + name + ".asset");
                    AssetDatabase.SaveAssets();
                    
                    // Copy mesh rendering stuff to a new game object
                    GameObject copy = Instantiate(pathTool.GetMeshHolder(), pathTool.gameObject.transform.parent);
                    copy.name = "Baked " + name;
                    copy.AddComponent<MeshCollider>();

                    // Hide original mesh in case we want to edit it later
                    pathTool.gameObject.SetActive(false);

                    // Force prefab to autosave
                    EditorSceneManager.MarkSceneDirty(pathTool.gameObject.scene);
                }
            }

            if (GUILayout.Button("Unbake Road"))
            {
                if (TryFindPathCreator())
                {
                    // Delete saved mesh
                    string name = pathTool.gameObject.name;
                    string path = "Assets/Prefabs/BakedRoads/" + name + ".asset";
                    if (File.Exists(path))
                    {
                        AssetDatabase.DeleteAsset(path);
                        AssetDatabase.SaveAssets();
                    }

                    // Delete baked spline
                    Transform baked = pathTool.gameObject.transform.parent.Find("Baked " + name);
                    DestroyImmediate(baked.gameObject);

                    // Show original mesh
                    pathTool.gameObject.SetActive(true);

                    // Force prefab to autosave
                    EditorSceneManager.MarkSceneDirty(pathTool.gameObject.scene);
                }
            }

            GUILayout.EndHorizontal();
        }


        void TriggerUpdate() {
            if (pathTool.pathCreator != null) {
                pathTool.TriggerUpdate();
            }
        }


        protected virtual void OnPathModified()
        {
            if (pathTool.autoUpdate)
            {
                TriggerUpdate();
            }
        }

        protected virtual void OnEnable()
        {
            pathTool = (PathSceneTool)target;
            pathTool.onDestroyed += OnToolDestroyed;

            if (TryFindPathCreator())
            {
                Subscribe();
                TriggerUpdate();
            }
        }

        void OnToolDestroyed() {
            if (pathTool != null) {
                pathTool.pathCreator.pathUpdated -= OnPathModified;
            }
        }

 
        protected virtual void Subscribe()
        {
            if (pathTool.pathCreator != null)
            {
                isSubscribed = true;
                pathTool.pathCreator.pathUpdated -= OnPathModified;
                pathTool.pathCreator.pathUpdated += OnPathModified;
            }
        }

        bool TryFindPathCreator()
        {
            // Try find a path creator in the scene, if one is not already assigned
            if (pathTool.pathCreator == null)
            {
                if (pathTool.GetComponent<PathCreator>() != null)
                {
                    pathTool.pathCreator = pathTool.GetComponent<PathCreator>();
                }
                else if (FindObjectOfType<PathCreator>())
                {
                    pathTool.pathCreator = FindObjectOfType<PathCreator>();
                }
            }
            return pathTool.pathCreator != null;
        }
    }
}
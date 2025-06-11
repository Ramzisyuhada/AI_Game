using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
[CustomEditor(typeof(CreateWayPoint))]

public class EditorWayPoint : Editor
{
    [SerializeField] GameObject point;
    [SerializeField] GameObject parent;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CreateWayPoint myScript = (CreateWayPoint)target;
        point = (GameObject)EditorGUILayout.ObjectField("Point Prefab", point, typeof(GameObject), true);
        parent = (GameObject)EditorGUILayout.ObjectField("Parent Object", parent, typeof(GameObject), true);

        if (GUILayout.Button("Tambahkan WayPoint"))
        {
            myScript.OnButtonClicked(point, parent);
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Waypoints", EditorStyles.boldLabel);
        List<GameObject> waypoints = GetWayPoints();
        if (waypoints != null)
        {
            foreach (GameObject wp in waypoints)
            {
                EditorGUILayout.BeginHorizontal();

                Vector3 position = wp.transform.localPosition;
                EditorGUILayout.LabelField("X", GUILayout.Width(20)); 
                position.x = EditorGUILayout.FloatField(position.x, GUILayout.Width(60)); 

                EditorGUILayout.LabelField("Y", GUILayout.Width(20)); 
                position.y = EditorGUILayout.FloatField(position.y, GUILayout.Width(60)); 

                EditorGUILayout.LabelField("Z", GUILayout.Width(20)); 
                position.z = EditorGUILayout.FloatField(position.z, GUILayout.Width(60)); 
                wp.transform.localPosition = position;
                if (GUILayout.Button("Hapus", GUILayout.Width(60)))
                {
                    Undo.RecordObject(myScript, "Remove Waypoint");
                    GameObject.DestroyImmediate(wp); 
                    EditorUtility.SetDirty(myScript); 
                }
                EditorGUILayout.EndHorizontal();


            }
        }

    }

    private List<GameObject> GetWayPoints() {
        CreateWayPoint myScript = (CreateWayPoint)target;

        return myScript.GetWaypoint();
    } 
   
}

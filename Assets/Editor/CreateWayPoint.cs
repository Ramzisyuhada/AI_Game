using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;



public class CreateWayPoint : MonoBehaviour

{
    public void OnButtonClicked()
    {
        Debug.Log("Button in the inspector clicked!");
    }
}

/*[CustomEditor(typeof(CreateWayPoint))]
public class CreateWayPointEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Menampilkan properti default

        CreateWayPoint myScript = (CreateWayPoint)target;

        if (GUILayout.Button("Click Me!"))
        {
            myScript.OnButtonClicked();
        }
    }
}
*/

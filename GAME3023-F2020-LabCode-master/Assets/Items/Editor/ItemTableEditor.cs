using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemTeble))]
public class ItemTableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ItemTeble itemTeble = (ItemTeble)target;
        if (itemTeble)
        {
            if(GUILayout.Button("Assign item IDs"))
            {
                itemTeble.AssignItemIDs();
            }
        }
    }
}

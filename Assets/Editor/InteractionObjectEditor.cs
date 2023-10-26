using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InteractionObject))]
public class InteractionObjectEditor : Editor
{
    [SerializeField] GameObject _TObject;


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        InteractionObject interactionObject = (InteractionObject)target;

        GUI.enabled = interactionObject.playerTInteraction == null && interactionObject.playerTMove == null;

        if (GUILayout.Button("Create Child Tranform", GUILayout.Height(30))) 
        {            
            GameObject temObj1 = PrefabUtility.InstantiatePrefab(_TObject) as GameObject;
            GameObject temObj2 = PrefabUtility.InstantiatePrefab(_TObject) as GameObject;
         
            temObj1.transform.SetParent(interactionObject.transform);            
            temObj1.transform.localPosition = Vector3.zero + interactionObject.transform.forward;
            temObj1.transform.Find("TTitleIcon").name = $"T Move ({interactionObject.name.Substring(0, 5)})";
            temObj1.name = $"T Move ({interactionObject.name.Substring(0, 5)})";

            temObj2.transform.SetParent(interactionObject.transform);
            temObj2.transform.localPosition = Vector3.zero;
            temObj2.transform.Find("TTitleIcon").name = $"T Interaction ({interactionObject.name.Substring(0,5)})";
            temObj2.name = $"T Interaction ({interactionObject.name.Substring(0,5)})";

            interactionObject.playerTMove = temObj1.transform;
            interactionObject.playerTInteraction = temObj2.transform;
        }
    }
}

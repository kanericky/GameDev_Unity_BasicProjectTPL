using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Core_Manager.Global_Configurations;
using UnityEngine;
using UnityEditor;

public class GameDataObjectEditorWindow : ExtendedEditorWindow
{
    public static void Open(SOGameConfigs dataObject)
    {
        GameDataObjectEditorWindow window = GetWindow<GameDataObjectEditorWindow>("Game Data Editor");
        window.serializedObject = new SerializedObject(dataObject);
    }

    private void OnGUI()
    {
        currentProperty = serializedObject.FindProperty("gameData");

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));
        
        DrawSidebar(currentProperty);
        
        EditorGUILayout.EndVertical();
        
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));

        if (_selectedProperty != null)
        {
            DrawSelectedPropertiesPanel();
        }
        else
        {
            EditorGUILayout.LabelField("Select an item from the list");
        }
        
        EditorGUILayout.EndVertical();
        
        EditorGUILayout.EndHorizontal();
        
        Apply();
    }
    
    void DrawSelectedPropertiesPanel()
    {
        currentProperty = _selectedProperty;

        EditorGUILayout.BeginHorizontal("box");
        
        // Can be modified based on the data structure
        //DrawField("name", true);
        //DrawField("title", true);

        EditorGUILayout.EndHorizontal();
        
    }
}

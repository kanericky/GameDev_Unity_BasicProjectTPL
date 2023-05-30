using System;
using System.IO;
using System.Linq;
using Gameplay.SceneLoadSystemFramework;
using Gameplay.SceneLoadSystemFramework.SceneDataStructure.Base;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    [CustomEditor(typeof(GameSceneSO), editorForChildClasses: true)]
    public class GameSceneSOEditor : UnityEditor.Editor
    {
        private const string NO_SCENES_WARNING = "Cannot Find the Scene, please select a new scene with the dropdown" + 
                                                 "and check the Building Settings to ensure the scene is there";
        private GUIStyle _headerLabelStyle;
        private static readonly string[] _excludedProperties = { "m_Script", "sceneName" };

        private string[] _sceneList;
        private GameSceneSO _gameSceneInspected;


        private void OnEnable()
        {
            _gameSceneInspected = target as GameSceneSO;
            PopulateScenePicker();
            InitializeGuiStyles();
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Scene Information", _headerLabelStyle);
            EditorGUILayout.Space();

            DrawScenePicker();
            DrawPropertiesExcluding(serializedObject, _excludedProperties);
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawScenePicker()
        {
            var sceneName = _gameSceneInspected.sceneName;
            EditorGUI.BeginChangeCheck();
            var selectedScene = _sceneList.ToList().IndexOf(sceneName);

            if (selectedScene < 0)
            {
                EditorGUILayout.HelpBox(NO_SCENES_WARNING, MessageType.Warning);
            }

            selectedScene = EditorGUILayout.Popup("Scene", selectedScene, _sceneList);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed Selected Scene");
                _gameSceneInspected.sceneName = _sceneList[selectedScene];
                MarkAllDirty();
            }

        }

        private void InitializeGuiStyles()
        {
            _headerLabelStyle = new GUIStyle(EditorStyles.largeLabel)
            {
                fontStyle = FontStyle.Bold,
                fontSize = 18,
                fixedHeight = 70.0f
            };
        }

        private void PopulateScenePicker()
        {
            var sceneCount = SceneManager.sceneCountInBuildSettings;
            _sceneList = new string[sceneCount];

            for (int i = 0; i < sceneCount; i++)
            {
                _sceneList[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            }
        }

        private void MarkAllDirty()
        {
            EditorUtility.SetDirty(target);
            EditorSceneManager.MarkAllScenesDirty();
        }
    }
}

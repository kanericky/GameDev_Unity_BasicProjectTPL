using Gameplay.Core_Manager.Global_Configurations;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Editor
{
    public class AssetHandler
    {
        [OnOpenAsset()]
        public static bool OpenEditor(int instanceId, int line)
        {
            SOGameConfigs obj = EditorUtility.InstanceIDToObject(instanceId) as SOGameConfigs;

            if (obj != null)
            {
                GameDataObjectEditorWindow.Open(obj);
                return true;
            }
            
            return false;
        }
    }
    
    
    [CustomEditor(typeof(SOGameConfigs))]
    public class GameDataObjectCustomEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (GUILayout.Button("Open Editor"))
            {
                GameDataObjectEditorWindow.Open((SOGameConfigs)target);
            }
        }
    }
}

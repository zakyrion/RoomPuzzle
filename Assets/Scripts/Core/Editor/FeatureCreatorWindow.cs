using UnityEngine;    
using UnityEditor;    
using System.IO;    
using System.Text.RegularExpressions;    
namespace RoomPuzzle.Core.Editor    
{    
    public class FeatureCreatorWindow : EditorWindow    
    {    
        private string _featureName = "NewFeature";    
        private const string BasePath = "Assets/_Project/Scripts/Features";    
        [MenuItem("Assets/Create/New Feature")]    
        public static void ShowWindow()    
        {    
            var window = GetWindow<FeatureCreatorWindow>("Create New Feature");    
            window.minSize = new Vector2(300, 100);    
            window.maxSize = new Vector2(300, 100);    
        }    
        private void OnGUI()    
        {    
            GUILayout.Label("New Feature Setup", EditorStyles.boldLabel);    
            EditorGUILayout.HelpBox("Enter a feature name (e.g., EnemyAI, Inventory). Avoid spaces and special characters.", MessageType.Info);    
            _featureName = EditorGUILayout.TextField("Feature Name", _featureName);    
            if (GUILayout.Button("Create Feature"))    
            {    
                CreateFeatureStructure();    
                Close();    
            }    
        }    
        private void CreateFeatureStructure()    
        {    
            if (!IsValidFeatureName(_featureName))    
            {    
                EditorUtility.DisplayDialog("Invalid Name", "Feature name must be a valid C# identifier (no spaces or special characters).", "OK");    
                return;    
            }    
            var featurePath = Path.Combine(BasePath, _featureName);    
            if (Directory.Exists(featurePath))    
            {    
                EditorUtility.DisplayDialog("Directory Exists", $"The feature folder '{_featureName}' already exists.", "OK");    
                return;    
            }    
            var modelPath = Path.Combine(featurePath, "Model");    
            var presenterPath = Path.Combine(featurePath, "Presenter");    
            var viewPath = Path.Combine(featurePath, "View");    
            Directory.CreateDirectory(modelPath);    
            Directory.CreateDirectory(presenterPath);    
            Directory.CreateDirectory(viewPath);    
            CreateInterfaceFile(modelPath, _featureName, "Model");    
            CreateInterfaceFile(presenterPath, _featureName, "Presenter");    
            CreateInterfaceFile(viewPath, _featureName, "View");    
            AssetDatabase.Refresh();    
            Debug.Log($"<color=green>Feature '{_featureName}' created successfully at '{featurePath}'</color>");    
        }    
        private static void CreateInterfaceFile(string path, string featureName, string layer)    
        {    
            var interfaceName = $"I{featureName}{layer}";    
            var filePath = Path.Combine(path, $"{interfaceName}.cs");    
            var namespaceName = $"RoomPuzzle.Features.{featureName}.{layer}";    
            var content =    
$@"namespace {namespaceName}    
{{    
    public interface {interfaceName}    
    {{    
        // Contract for the {featureName} {layer}    
    }}    
}}    
";    
            File.WriteAllText(filePath, content);    
        }    
        private static bool IsValidFeatureName(string name)    
        {    
            return !string.IsNullOrWhiteSpace(name) && Regex.IsMatch(name, @"^[a-zA-Z_][a-zA-Z0-9_]*$");    
        }    
    }    
}
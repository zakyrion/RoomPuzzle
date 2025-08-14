using System.IO;
using RoomPuzzle.Core.DI;
using RoomPuzzle.Features.Camera.View;
using RoomPuzzle.Features.Player.View;
using UnityEditor;
using UnityEngine;

namespace RoomPuzzle.Core.Editor
{
    public class SceneSetupTool
    {
        private const string OneWayPlatformTag = "OneWayPlatform";
        private const string PlatformLayer = "Platforms";
        private const string PlayerPrefabPath = "Assets/_Project/Prefabs/Player.prefab";
        private const string PrefabFolder = "Assets/_Project/Prefabs";

        [MenuItem("Tools/Setup 2.5D Platformer Scene")]
        public static void SetupScene()
        {
            if (!EditorUtility.DisplayDialog("Confirm Scene Setup",
                "This will create new GameObjects, a Player prefab, and configure layers/tags for the platformer. Are you sure you want to proceed?",
                "Yes, Setup", "Cancel"))
            {
                return;
            }
            // Step 1: Create the necessary Layer and Tag in Project Settings
            CreateLayerAndTag();
            // Step 2: Create multiple platforms in the scene
            CreatePlatforms();
            // Step 3: Create the Player prefab with all its logic attached
            var playerPrefab = CreatePlayerPrefab();
            if (playerPrefab == null)
            {
                Debug.LogError("Failed to create Player prefab. Aborting setup. Ensure the PlayerView script exists and has no compile errors.");
                return;
            }
            // Step 4: Configure the Zenject installer to connect everything
            ConfigureInstaller(playerPrefab);
            EditorUtility.DisplayDialog("Setup Complete",
                "The 2.5D platformer scene has been set up successfully and is ready to play!", "OK");
        }

        private static void ConfigureInstaller(GameObject playerPrefab)
        {
            var installer = Object.FindObjectOfType<GameInstaller>();
            if (installer == null)
            {
                var installerGO = new GameObject("GameInstaller");
                installer = installerGO.AddComponent<GameInstaller>();
            }
            /*var mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("No Main Camera found in the scene. Please add one.");
                return;
            }
            mainCamera.transform.position = new Vector3(0, 3, -15); // Adjusted camera position
            mainCamera.transform.rotation = Quaternion.Euler(10, 0, 0);
            mainCamera.orthographic = false;
            // Add the CameraView script which contains the follow logic
            var cameraView = mainCamera.GetComponent<CameraView>() ?? mainCamera.gameObject.AddComponent<CameraView>();*/
            // Link the Player and Camera to the installer so Zenject can wire them up
            var installerSO = new SerializedObject(installer);
            installerSO.FindProperty("_playerPrefab").objectReferenceValue = playerPrefab;
            //installerSO.FindProperty("_cameraView").objectReferenceValue = cameraView;
            installerSO.ApplyModifiedProperties();
            Debug.Log("GameInstaller configured with Player prefab and Camera view.");
        }

        private static void CreateLayerAndTag()
        {
            var tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            // Create Layer "Platforms"
            var layers = tagManager.FindProperty("layers");
            if (!LayerExists(layers, PlatformLayer))
            {
                if (CreateLayer(layers, PlatformLayer))
                    Debug.Log($"Layer '{PlatformLayer}' created.");
                else
                    Debug.LogWarning($"Could not create layer '{PlatformLayer}'. All user layers might be in use.");
            }
            else
                Debug.Log($"Layer '{PlatformLayer}' already exists.");
            // Create Tag "OneWayPlatform"
            var tags = tagManager.FindProperty("tags");
            if (!TagExists(tags, OneWayPlatformTag))
            {
                CreateTag(tags, OneWayPlatformTag);
                Debug.Log($"Tag '{OneWayPlatformTag}' created.");
            }
            else
                Debug.Log($"Tag '{OneWayPlatformTag}' already exists.");
            tagManager.ApplyModifiedProperties();
            AssetDatabase.SaveAssets();
        }

        private static void CreatePlatforms()
        {
            var platformLayer = LayerMask.NameToLayer(PlatformLayer);
            // Ground Platform
            var ground = GameObject.Find("Ground") ?? GameObject.CreatePrimitive(PrimitiveType.Cube);
            ground.name = "Ground";
            ground.transform.position = new Vector3(0, -1, 0);
            ground.transform.localScale = new Vector3(30, 1, 1);
            ground.layer = platformLayer;
            // One-Way Platform (pass-through)
            var oneWayPlatform = GameObject.Find("OneWayPlatform") ?? GameObject.CreatePrimitive(PrimitiveType.Cube);
            oneWayPlatform.name = "OneWayPlatform";
            oneWayPlatform.transform.position = new Vector3(5, 2.5f, 0);
            oneWayPlatform.transform.localScale = new Vector3(8, 0.5f, 1);
            oneWayPlatform.layer = platformLayer;
            oneWayPlatform.tag = OneWayPlatformTag;
            // Second solid platform
            var secondPlatform = GameObject.Find("SecondPlatform") ?? GameObject.CreatePrimitive(PrimitiveType.Cube);
            secondPlatform.name = "SecondPlatform";
            secondPlatform.transform.position = new Vector3(-8, 4, 0);
            secondPlatform.transform.localScale = new Vector3(10, 1, 1);
            secondPlatform.layer = platformLayer;
        }

        private static GameObject CreatePlayerPrefab()
        {
            if (!Directory.Exists(PrefabFolder))
            {
                Directory.CreateDirectory(PrefabFolder);
            }
            var playerInstance = new GameObject("Player_Temp", typeof(CapsuleCollider), typeof(Rigidbody));
            // This adds the PlayerView script, which contains the movement and jump logic
            var playerView = playerInstance.AddComponent<PlayerView>();
            if (playerView == null)
                return null;
            var rb = playerInstance.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.useGravity = true;
            // Configure the PlayerView to detect the "Platforms" layer
            var playerViewSO = new SerializedObject(playerView);
            var layerMaskProp = playerViewSO.FindProperty("_platformLayerMask");
            if (layerMaskProp != null)
            {
                layerMaskProp.intValue = LayerMask.GetMask(PlatformLayer);
                playerViewSO.ApplyModifiedProperties();
            }
            // Save as a prefab and clean up the temporary instance
            var prefab = PrefabUtility.SaveAsPrefabAsset(playerInstance, PlayerPrefabPath);
            Object.DestroyImmediate(playerInstance);
            Debug.Log($"Player prefab created at '{PlayerPrefabPath}'.");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return prefab;
        }

        #region Helper Methods from Bootstrap
        private static bool LayerExists(SerializedProperty layers, string layerName)
        {
            for (var i = 8; i < layers.arraySize; i++)
            {
                if (layers.GetArrayElementAtIndex(i).stringValue == layerName)
                    return true;
            }
            return false;
        }

        private static bool CreateLayer(SerializedProperty layers, string layerName)
        {
            for (var j = 8; j < layers.arraySize; j++)
            {
                var layerSP = layers.GetArrayElementAtIndex(j);
                if (string.IsNullOrEmpty(layerSP.stringValue))
                {
                    layerSP.stringValue = layerName;
                    return true;
                }
            }
            return false;
        }

        private static bool TagExists(SerializedProperty tags, string tagName)
        {
            for (var i = 0; i < tags.arraySize; i++)
            {
                if (tags.GetArrayElementAtIndex(i).stringValue == tagName)
                    return true;
            }
            return false;
        }

        private static void CreateTag(SerializedProperty tags, string tagName)
        {
            tags.InsertArrayElementAtIndex(tags.arraySize);
            tags.GetArrayElementAtIndex(tags.arraySize - 1).stringValue = tagName;
        }
        #endregion
    }
}

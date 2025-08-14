using UnityEditor;
using System.IO;
using UnityEngine;
public class ProjectBootstrap
{
    private const string BasePath = "Assets/Scripts";
    [MenuItem("Tools/Bootstrap Full Platformer Project")]
    public static void GenerateAllCode()
    {
        if (!EditorUtility.DisplayDialog("Confirm Project Bootstrap",
            "This will generate the complete folder structure and all C# scripts for the platformer base. It's safe to run on a new project. Proceed?",
            "Yes, Generate Everything", "Cancel"))
        {
            return;
        }
        CreateAllFolders();
        CreateAllScripts();
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Bootstrap Complete",
            "All folders and scripts have been generated successfully.\n\nYou can now use 'Tools > Setup 2.5D Platformer Scene' to prepare your scene.",
            "Awesome!");
    }
    private static void CreateAllFolders()
    {
        Directory.CreateDirectory($"{BasePath}/Core/DI");
        Directory.CreateDirectory($"{BasePath}/Core/Editor");
        Directory.CreateDirectory($"{BasePath}/Features/Input/Model");
        Directory.CreateDirectory($"{BasePath}/Features/Player/Model");
        Directory.CreateDirectory($"{BasePath}/Features/Player/Presenter");
        Directory.CreateDirectory($"{BasePath}/Features/Player/View");
        Directory.CreateDirectory($"{BasePath}/Features/Camera/Presenter");
        Directory.CreateDirectory($"{BasePath}/Features/Camera/View");
        Directory.CreateDirectory("Assets/_Project/Prefabs");
        Debug.Log("All project folders created.");
    }
    private static void CreateAllScripts()
    {
        // --- Input Feature ---
        WriteScriptToFile($"{BasePath}/Features/Input/Model/IInputModel.cs", GetIInputModelContent());
        WriteScriptToFile($"{BasePath}/Features/Input/Model/InputModel.cs", GetInputModelContent());
        // --- Player Feature ---
        WriteScriptToFile($"{BasePath}/Features/Player/Model/IPlayerModel.cs", GetIPlayerModelContent());
        WriteScriptToFile($"{BasePath}/Features/Player/Model/PlayerModel.cs", GetPlayerModelContent());
        WriteScriptToFile($"{BasePath}/Features/Player/View/IPlayerView.cs", GetIPlayerViewContent());
        WriteScriptToFile($"{BasePath}/Features/Player/View/PlayerView.cs", GetPlayerViewContent());
        WriteScriptToFile($"{BasePath}/Features/Player/Presenter/IPlayerPresenter.cs", GetIPlayerPresenterContent());
        WriteScriptToFile($"{BasePath}/Features/Player/Presenter/PlayerPresenter.cs", GetPlayerPresenterContent());
        // --- Camera Feature ---
        WriteScriptToFile($"{BasePath}/Features/Camera/View/ICameraView.cs", GetICameraViewContent());
        WriteScriptToFile($"{BasePath}/Features/Camera/View/CameraView.cs", GetCameraViewContent());
        WriteScriptToFile($"{BasePath}/Features/Camera/Presenter/ICameraPresenter.cs", GetICameraPresenterContent());
        WriteScriptToFile($"{BasePath}/Features/Camera/Presenter/CameraPresenter.cs", GetCameraPresenterContent());
        // --- Core ---
        WriteScriptToFile($"{BasePath}/Core/DI/GameInstaller.cs", GetGameInstallerContent());
        // --- Editor Tools ---
        WriteScriptToFile($"{BasePath}/Core/Editor/FeatureCreatorWindow.cs", GetFeatureCreatorWindowContent());
        WriteScriptToFile($"{BasePath}/Core/Editor/SceneSetupTool.cs", GetSceneSetupToolContent());
        Debug.Log("All script files generated.");
    }
    private static void WriteScriptToFile(string path, string content)
    {
        File.WriteAllText(path, content);
    }
    #region Script Content Getters
    private static string GetIInputModelContent() =>
@"using System;    
namespace RoomPuzzle.Features.Input.Model    
{    
    public interface IInputModel    
    {    
        float HorizontalInput { get; }    
        event Action OnJumpPressed;    
    }    
}";
    private static string GetInputModelContent() =>
@"using System;    
using UnityEngine;    
using Zenject;    
namespace RoomPuzzle.Features.Input.Model    
{    
    public class InputModel : IInputModel, ITickable    
    {    
        public float HorizontalInput { get; private set; }    
        public event Action OnJumpPressed;    
        public void Tick()    
        {    
            HorizontalInput = UnityEngine.Input.GetAxis(""Horizontal"");    
            if (UnityEngine.Input.GetButtonDown(""Jump""))    
            {    
                OnJumpPressed?.Invoke();    
            }    
        }    
    }    
}";
    private static string GetIPlayerModelContent() =>
@"using UnityEngine;    
namespace RoomPuzzle.Features.Player.Model    
{    
    public interface IPlayerModel    
    {    
        float MoveSpeed { get; }    
        float JumpForce { get; }    
        Vector3 Position { get; set; }    
        bool IsGrounded { get; set; }    
    }    
}";
    private static string GetPlayerModelContent() =>
@"using UnityEngine;    
namespace RoomPuzzle.Features.Player.Model    
{    
    public class PlayerModel : IPlayerModel    
    {    
        public float MoveSpeed { get; } = 7f;    
        public float JumpForce { get; } = 15f;    
        public Vector3 Position { get; set; }    
        public bool IsGrounded { get; set; }    
    }    
}";
    private static string GetIPlayerViewContent() =>
@"using UnityEngine;    
namespace RoomPuzzle.Features.Player.View    
{    
    public interface IPlayerView    
    {    
        void Move(Vector3 position);    
        void Jump(float force);    
    }    
}";
    private static string GetPlayerViewContent() =>
@"using UnityEngine;    
namespace RoomPuzzle.Features.Player.View    
{    
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]    
    public class PlayerView : MonoBehaviour, IPlayerView    
    {    
        [Header(""Collision Settings"")]    
        [SerializeField] private LayerMask _platformLayerMask;    
        [SerializeField] private string _oneWayPlatformTag = ""OneWayPlatform"";    
        [SerializeField] private float _groundCheckDistance = 0.2f;    
        private Rigidbody _rigidbody;    
        private CapsuleCollider _collider;    
        private Collider[] _oneWayPlatforms;    
        public Transform Transform => transform;    
        public bool IsGrounded { get; private set; }    
        private void Awake()    
        {    
            _rigidbody = GetComponent<Rigidbody>();    
            _collider = GetComponent<CapsuleCollider>();    
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;    
        }    
        private void Start()    
        {    
            var platformObjects = GameObject.FindGameObjectsWithTag(_oneWayPlatformTag);    
            _oneWayPlatforms = new Collider[platformObjects.Length];    
            for (int i = 0; i < platformObjects.Length; i++)    
            {    
                _oneWayPlatforms[i] = platformObjects[i].GetComponent<Collider>();    
            }    
        }    
        private void FixedUpdate()    
        {    
            CheckIfGrounded();    
            HandleOneWayPlatforms();    
        }    
        public void Move(Vector3 direction)    
        {    
            var velocity = _rigidbody.velocity;    
            velocity.x = direction.x;    
            _rigidbody.velocity = velocity;    
        }    
        public void Jump(float force)    
        {    
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, 0);    
            _rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);    
        }    
        private void CheckIfGrounded()    
        {    
            float radius = _collider.radius * 0.9f;    
            Vector3 origin = transform.position + new Vector3(0, _collider.radius, 0);    
            IsGrounded = Physics.SphereCast(origin, radius, Vector3.down, out _, _groundCheckDistance, _platformLayerMask);    
        }    
        private void HandleOneWayPlatforms()    
        {    
            if (_oneWayPlatforms == null) return;    
            foreach (var platformCollider in _oneWayPlatforms)    
            {    
                if (platformCollider == null) continue;    
                Physics.IgnoreCollision(_collider, platformCollider, _rigidbody.velocity.y > 0.01f);    
            }    
        }    
        private void OnDrawGizmosSelected()    
        {    
            Gizmos.color = Color.yellow;    
            var capsuleCollider = GetComponent<CapsuleCollider>();    
            float radius = capsuleCollider.radius * 0.9f;    
            Vector3 origin = transform.position + new Vector3(0, capsuleCollider.radius, 0);    
            Gizmos.DrawWireSphere(origin + Vector3.down * _groundCheckDistance, radius);    
        }    
    }    
}";
    private static string GetIPlayerPresenterContent() =>
@"namespace RoomPuzzle.Features.Player.Presenter    
{    
    public interface IPlayerPresenter { }    
}";
    private static string GetPlayerPresenterContent() =>
@"using System;    
using UnityEngine;    
using Zenject;    
using RoomPuzzle.Features.Input.Model;    
using RoomPuzzle.Features.Player.Model;    
using RoomPuzzle.Features.Player.View;    
namespace RoomPuzzle.Features.Player.Presenter    
{    
    public class PlayerPresenter : IPlayerPresenter, IInitializable, ITickable, IDisposable    
    {    
        private readonly IInputModel _inputModel;    
        private readonly IPlayerModel _playerModel;    
        private readonly IPlayerView _playerView;    
        public PlayerPresenter(IInputModel inputModel, IPlayerModel playerModel, IPlayerView playerView)    
        {    
            _inputModel = inputModel;    
            _playerModel = playerModel;    
            _playerView = playerView;    
        }    
        public void Initialize()    
        {    
            _inputModel.OnJumpPressed += HandleJump;    
        }    
        public void Tick()    
        {    
            var moveDirection = new Vector3(_inputModel.HorizontalInput * _playerModel.MoveSpeed, 0, 0);    
            _playerView.Move(moveDirection);    
            var view = _playerView as PlayerView;    
            if (view != null)    
            {    
                _playerModel.Position = view.Transform.position;    
                _playerModel.IsGrounded = view.IsGrounded;    
            }    
        }    
        private void HandleJump()    
        {    
            if (_playerModel.IsGrounded)    
            {    
                _playerView.Jump(_playerModel.JumpForce);    
            }    
        }    
        public void Dispose()    
        {    
            _inputModel.OnJumpPressed -= HandleJump;    
        }    
    }    
}";
    private static string GetICameraViewContent() =>
@"namespace RoomPuzzle.Features.Camera.View    
{    
    public interface ICameraView { }    
}";
    private static string GetCameraViewContent() =>
@"using UnityEngine;    
namespace RoomPuzzle.Features.Camera.View    
{    
    public class CameraView : MonoBehaviour, ICameraView    
    {    
        [SerializeField] private Vector3 _offset = new(0, 2, -10);    
        [SerializeField] private float _smoothSpeed = 0.125f;    
        private Transform _target;    
        public void SetTarget(Transform target)    
        {    
            _target = target;    
        }    
        private void LateUpdate()    
        {    
            if (_target == null) return;    
            Vector3 desiredPosition = _target.position + _offset;    
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);    
            transform.position = smoothedPosition;    
        }    
    }    
}";
    private static string GetICameraPresenterContent() =>
@"namespace RoomPuzzle.Features.Camera.Presenter    
{    
    public interface ICameraPresenter { }    
}";
    private static string GetCameraPresenterContent() =>
@"using Zenject;    
using UnityEngine;    
using RoomPuzzle.Features.Camera.View;    
using RoomPuzzle.Features.Player.View;    
namespace RoomPuzzle.Features.Camera.Presenter    
{    
    public class CameraPresenter : ICameraPresenter, IInitializable    
    {    
        private readonly ICameraView _cameraView;    
        private readonly IPlayerView _playerView;    
        public CameraPresenter(ICameraView cameraView, IPlayerView playerView)    
        {    
            _cameraView = cameraView;    
            _playerView = playerView;    
        }    
        public void Initialize()    
        {    
            var playerTransform = (_playerView as MonoBehaviour)?.transform;    
            if (playerTransform != null)    
            {    
                (_cameraView as CameraView)?.SetTarget(playerTransform);    
            }    
        }    
    }    
}";
    private static string GetGameInstallerContent() =>
@"using UnityEngine;    
using Zenject;    
using RoomPuzzle.Features.Input.Model;    
using RoomPuzzle.Features.Player.Model;    
using RoomPuzzle.Features.Player.Presenter;    
using RoomPuzzle.Features.Player.View;    
using RoomPuzzle.Features.Camera.Presenter;    
using RoomPuzzle.Features.Camera.View;    
namespace RoomPuzzle.Core.DI    
{    
    public class GameInstaller : MonoInstaller    
    {    
        [SerializeField] private GameObject _playerPrefab;    
        [SerializeField] private CameraView _cameraView;    
        public override void InstallBindings()    
        {    
            // Input    
            Container.BindInterfacesAndSelfTo<InputModel>().AsSingle();    
            // Player    
            Container.Bind<IPlayerModel>().To<PlayerModel>().AsSingle();    
            Container.Bind<IPlayerView>().FromComponentInNewPrefab(_playerPrefab).AsSingle().NonLazy();    
            Container.BindInterfacesAndSelfTo<PlayerPresenter>().AsSingle().NonLazy();    
            // Camera    
            Container.Bind<ICameraView>().FromInstance(_cameraView).AsSingle();    
            Container.BindInterfacesAndSelfTo<CameraPresenter>().AsSingle().NonLazy();    
        }    
    }    
}";
    private static string GetFeatureCreatorWindowContent() =>
@"using UnityEngine;    
using UnityEditor;    
using System.IO;    
using System.Text.RegularExpressions;    
namespace RoomPuzzle.Core.Editor    
{    
    public class FeatureCreatorWindow : EditorWindow    
    {    
        private string _featureName = ""NewFeature"";    
        private const string BasePath = ""Assets/_Project/Scripts/Features"";    
        [MenuItem(""Assets/Create/New Feature"")]    
        public static void ShowWindow()    
        {    
            var window = GetWindow<FeatureCreatorWindow>(""Create New Feature"");    
            window.minSize = new Vector2(300, 100);    
            window.maxSize = new Vector2(300, 100);    
        }    
        private void OnGUI()    
        {    
            GUILayout.Label(""New Feature Setup"", EditorStyles.boldLabel);    
            EditorGUILayout.HelpBox(""Enter a feature name (e.g., EnemyAI, Inventory). Avoid spaces and special characters."", MessageType.Info);    
            _featureName = EditorGUILayout.TextField(""Feature Name"", _featureName);    
            if (GUILayout.Button(""Create Feature""))    
            {    
                CreateFeatureStructure();    
                Close();    
            }    
        }    
        private void CreateFeatureStructure()    
        {    
            if (!IsValidFeatureName(_featureName))    
            {    
                EditorUtility.DisplayDialog(""Invalid Name"", ""Feature name must be a valid C# identifier (no spaces or special characters)."", ""OK"");    
                return;    
            }    
            var featurePath = Path.Combine(BasePath, _featureName);    
            if (Directory.Exists(featurePath))    
            {    
                EditorUtility.DisplayDialog(""Directory Exists"", $""The feature folder '{_featureName}' already exists."", ""OK"");    
                return;    
            }    
            var modelPath = Path.Combine(featurePath, ""Model"");    
            var presenterPath = Path.Combine(featurePath, ""Presenter"");    
            var viewPath = Path.Combine(featurePath, ""View"");    
            Directory.CreateDirectory(modelPath);    
            Directory.CreateDirectory(presenterPath);    
            Directory.CreateDirectory(viewPath);    
            CreateInterfaceFile(modelPath, _featureName, ""Model"");    
            CreateInterfaceFile(presenterPath, _featureName, ""Presenter"");    
            CreateInterfaceFile(viewPath, _featureName, ""View"");    
            AssetDatabase.Refresh();    
            Debug.Log($""<color=green>Feature '{_featureName}' created successfully at '{featurePath}'</color>"");    
        }    
        private static void CreateInterfaceFile(string path, string featureName, string layer)    
        {    
            var interfaceName = $""I{featureName}{layer}"";    
            var filePath = Path.Combine(path, $""{interfaceName}.cs"");    
            var namespaceName = $""RoomPuzzle.Features.{featureName}.{layer}"";    
            var content =    
$@""namespace {namespaceName}    
{{    
    public interface {interfaceName}    
    {{    
        // Contract for the {featureName} {layer}    
    }}    
}}    
"";    
            File.WriteAllText(filePath, content);    
        }    
        private static bool IsValidFeatureName(string name)    
        {    
            return !string.IsNullOrWhiteSpace(name) && Regex.IsMatch(name, @""^[a-zA-Z_][a-zA-Z0-9_]*$"");    
        }    
    }    
}";
    private static string GetSceneSetupToolContent() =>
@"using UnityEngine;    
using UnityEditor;    
using System.IO;    
using RoomPuzzle.Core.DI;    
using RoomPuzzle.Features.Camera.View;    
using RoomPuzzle.Features.Player.View;    
namespace RoomPuzzle.Core.Editor    
{    
    public class SceneSetupTool    
    {    
        private const string PlayerPrefabPath = ""Assets/_Project/Prefabs/Player.prefab"";    
        private const string PrefabFolder = ""Assets/_Project/Prefabs"";    
        private const string PlatformLayer = ""Platforms"";    
        private const string OneWayPlatformTag = ""OneWayPlatform"";    
        [MenuItem(""Tools/Setup 2.5D Platformer Scene"")]    
        public static void SetupScene()    
        {    
            if (!EditorUtility.DisplayDialog(""Confirm Scene Setup"",    
                ""This will create new GameObjects, a Player prefab, and configure layers/tags for the platformer. Are you sure you want to proceed?"",    
                ""Yes, Setup"", ""Cancel""))    
            {    
                return;    
            }    
            CreateLayerAndTag();    
            CreatePlatforms();    
            GameObject playerPrefab = CreatePlayerPrefab();    
            if (playerPrefab == null)    
            {    
                Debug.LogError(""Failed to create Player prefab. Aborting setup. Ensure the PlayerView script exists and has no compile errors."");    
                return;    
            }    
            ConfigureInstaller(playerPrefab);    
            EditorUtility.DisplayDialog(""Setup Complete"",    
                ""The 2.5D platformer scene has been set up successfully.\n\n"" +    
                ""- 'Platforms' layer created.\n"" +    
                ""- 'OneWayPlatform' tag created.\n"" +    
                ""- Ground and Platform objects added.\n"" +    
                ""- Player prefab created.\n"" +    
                ""- GameInstaller configured."", ""OK"");    
        }    
        private static void CreateLayerAndTag()    
        {    
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath(""ProjectSettings/TagManager.asset"")[0]);    
            SerializedProperty layers = tagManager.FindProperty(""layers"");    
            if (!LayerExists(layers, PlatformLayer))    
            {    
                if (CreateLayer(layers, PlatformLayer)) Debug.Log($""Layer '{PlatformLayer}' created."");    
                else Debug.LogWarning($""Could not create layer '{PlatformLayer}'. All user layers might be in use."");    
            }    
            else Debug.Log($""Layer '{PlatformLayer}' already exists."");    
            SerializedProperty tags = tagManager.FindProperty(""tags"");    
            if (!TagExists(tags, OneWayPlatformTag))    
            {    
                CreateTag(tags, OneWayPlatformTag);    
                Debug.Log($""Tag '{OneWayPlatformTag}' created."");    
            }    
            else Debug.Log($""Tag '{OneWayPlatformTag}' already exists."");    
            tagManager.ApplyModifiedProperties();    
            AssetDatabase.SaveAssets();    
        }    
        private static bool LayerExists(SerializedProperty layers, string layerName)    
        {    
            for (int i = 8; i < layers.arraySize; i++)    
            {    
                if (layers.GetArrayElementAtIndex(i).stringValue == layerName) return true;    
            }    
            return false;    
        }    
        private static bool CreateLayer(SerializedProperty layers, string layerName)    
        {    
            for (int j = 8; j < layers.arraySize; j++)    
            {    
                SerializedProperty layerSP = layers.GetArrayElementAtIndex(j);    
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
            for (int i = 0; i < tags.arraySize; i++)    
            {    
                if (tags.GetArrayElementAtIndex(i).stringValue == tagName) return true;    
            }    
            return false;    
        }    
        private static void CreateTag(SerializedProperty tags, string tagName)    
        {    
            tags.InsertArrayElementAtIndex(tags.arraySize);    
            tags.GetArrayElementAtIndex(tags.arraySize - 1).stringValue = tagName;    
        }    
        private static void CreatePlatforms()    
        {    
            GameObject ground = GameObject.Find(""Ground"") ?? GameObject.CreatePrimitive(PrimitiveType.Cube);    
            ground.name = ""Ground"";    
            ground.transform.position = new Vector3(0, -1, 0);    
            ground.transform.localScale = new Vector3(30, 1, 1);    
            ground.layer = LayerMask.NameToLayer(PlatformLayer);    
            GameObject oneWayPlatform = GameObject.Find(""OneWayPlatform"") ?? GameObject.CreatePrimitive(PrimitiveType.Cube);    
            oneWayPlatform.name = ""OneWayPlatform"";    
            oneWayPlatform.transform.position = new Vector3(0, 2.5f, 0);    
            oneWayPlatform.transform.localScale = new Vector3(10, 0.5f, 1);    
            oneWayPlatform.layer = LayerMask.NameToLayer(PlatformLayer);    
            oneWayPlatform.tag = OneWayPlatformTag;    
        }    
        private static GameObject CreatePlayerPrefab()    
        {    
            if (!Directory.Exists(PrefabFolder)) Directory.CreateDirectory(PrefabFolder);    
            GameObject playerInstance = new GameObject(""Player_Temp"", typeof(CapsuleCollider), typeof(Rigidbody));    
            PlayerView playerView = playerInstance.AddComponent<PlayerView>();    
            if (playerView == null) return null;    
            Rigidbody rb = playerInstance.GetComponent<Rigidbody>();    
            rb.constraints = RigidbodyConstraints.FreezeRotation;    
            SerializedObject playerViewSO = new SerializedObject(playerView);    
            SerializedProperty layerMaskProp = playerViewSO.FindProperty(""_platformLayerMask"");    
            if(layerMaskProp != null)    
            {    
                layerMaskProp.intValue = LayerMask.GetMask(PlatformLayer);    
                playerViewSO.ApplyModifiedProperties();    
            }    
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(playerInstance, PlayerPrefabPath);    
            Object.DestroyImmediate(playerInstance);    
            Debug.Log($""Player prefab created at '{PlayerPrefabPath}'."");    
            AssetDatabase.SaveAssets();    
            AssetDatabase.Refresh();    
            return prefab;    
        }    
        private static void ConfigureInstaller(GameObject playerPrefab)    
        {    
            GameInstaller installer = Object.FindObjectOfType<GameInstaller>();    
            if (installer == null)    
            {    
                GameObject installerGO = new GameObject(""GameInstaller"");    
                installer = installerGO.AddComponent<GameInstaller>();    
            }    
            Camera mainCamera = Camera.main;    
            if (mainCamera == null)    
            {    
                Debug.LogError(""No Main Camera found in the scene. Please add one."");    
                return;    
            }    
            mainCamera.transform.position = new Vector3(0, 1, -10);    
            mainCamera.orthographic = false;    
            CameraView cameraView = mainCamera.GetComponent<CameraView>() ?? mainCamera.gameObject.AddComponent<CameraView>();    
            SerializedObject installerSO = new SerializedObject(installer);    
            installerSO.FindProperty(""_playerPrefab"").objectReferenceValue = playerPrefab;    
            installerSO.FindProperty(""_cameraView"").objectReferenceValue = cameraView;    
            installerSO.ApplyModifiedProperties();    
            Debug.Log(""GameInstaller configured with Player prefab and Camera view."");    
        }    
    }    
}";
    #endregion
}
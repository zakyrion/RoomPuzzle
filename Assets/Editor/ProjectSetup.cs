using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
public class ProjectSetup : EditorWindow
{
    [MenuItem("Tools/Project Setup/1. Create Project Structure")]
    public static void CreateProjectStructure()
    {
        CreateFolders();
        CreateScripts();
        CreatePlaceholderPrefabs();
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Project Setup",
            "Project structure and scripts created successfully!\n\n" +
            "Wait for compilation to finish, then run step 2.",
            "OK");
    }
    // Folder and Prefab creation methods remain the same...
    private static void CreateFolders()
    {
        Directory.CreateDirectory(Path.Combine(Application.dataPath, "Features"));
        string[] features = { "Core", "Player", "Inventory", "Door", "Key" };
        string[] featuresWithMvp = { "Player", "Door", "Key" };
        foreach (string feature in features)
        {
            string featurePath = Path.Combine(Application.dataPath, "Features", feature);
            Directory.CreateDirectory(Path.Combine(featurePath, "Scripts"));
        }
        foreach (string feature in featuresWithMvp)
        {
            string scriptsPath = Path.Combine(Application.dataPath, "Features", feature, "Scripts");
            Directory.CreateDirectory(Path.Combine(scriptsPath, "Model"));
            Directory.CreateDirectory(Path.Combine(scriptsPath, "View"));
            Directory.CreateDirectory(Path.Combine(scriptsPath, "Presenter"));
            Directory.CreateDirectory(Path.Combine(scriptsPath, "Installer"));
        }
        Directory.CreateDirectory(Path.Combine(Application.dataPath, "Features", "Inventory", "Scripts", "Model"));
        Directory.CreateDirectory(Path.Combine(Application.dataPath, "Features", "Key", "ScriptableObjects"));
        Directory.CreateDirectory(Path.Combine(Application.dataPath, "Resources", "Prefabs"));
        Directory.CreateDirectory(Path.Combine(Application.dataPath, "Scenes"));
        Directory.CreateDirectory(Path.Combine(Application.dataPath, "Settings"));
    }
    private static void CreatePlaceholderPrefabs()
    {
        GameObject keyObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        keyObject.name = "Key";
        keyObject.GetComponent<SphereCollider>().isTrigger = true;
        PrefabUtility.SaveAsPrefabAsset(keyObject, "Assets/Resources/Prefabs/Key.prefab");
        Object.DestroyImmediate(keyObject);
        GameObject doorObject = new GameObject("Door");
        GameObject doorVisual = GameObject.CreatePrimitive(PrimitiveType.Cube);
        doorVisual.name = "Visual";
        doorVisual.transform.SetParent(doorObject.transform);
        doorVisual.transform.localScale = new Vector3(1, 2, 0.1f);
        doorVisual.transform.localPosition = new Vector3(0.5f, 1, 0);
        doorObject.AddComponent<BoxCollider>();
        PrefabUtility.SaveAsPrefabAsset(doorObject, "Assets/Resources/Prefabs/Door.prefab");
        Object.DestroyImmediate(doorObject);
        GameObject playerObject = new GameObject("Player");
        playerObject.AddComponent<CharacterController>();
        GameObject cameraObject = new GameObject("MainCamera");
        cameraObject.transform.SetParent(playerObject.transform);
        var cam = cameraObject.AddComponent<Camera>();
        cam.clearFlags = CameraClearFlags.Skybox;
        cam.nearClipPlane = 0.01f;
        cameraObject.tag = "MainCamera";
        cameraObject.transform.localPosition = new Vector3(0, 0.6f, 0);
        PrefabUtility.SaveAsPrefabAsset(playerObject, "Assets/Resources/Prefabs/Player.prefab");
        Object.DestroyImmediate(playerObject);
    }
    private static void CreateScripts()
    {
        // Core
        CreateScriptFile("Assets/Features/Core/Scripts/IPickupable.cs", GetIPickupableContent());
        CreateScriptFile("Assets/Features/Core/Scripts/IOpenable.cs", GetIOpenableContent());
        CreateScriptFile("Assets/Features/Core/Scripts/GameSceneInstaller.cs", GetGameSceneInstallerContent());
        // Settings
        CreateScriptFile("Assets/Settings/GameSettingsInstaller.cs", GetGameSettingsInstallerContent());
        // Inventory
        CreateScriptFile("Assets/Features/Inventory/Scripts/Model/InventoryModel.cs", GetInventoryModelContent());
        // Key
        CreateScriptFile("Assets/Features/Key/ScriptableObjects/KeyData.cs", GetKeyDataContent());
        CreateScriptFile("Assets/Features/Key/Scripts/View/KeyView.cs", GetKeyViewContent());
        CreateScriptFile("Assets/Features/Key/Scripts/Presenter/KeyPresenter.cs", GetKeyPresenterContent());
        CreateScriptFile("Assets/Features/Key/Scripts/Installer/KeyInstaller.cs", GetKeyInstallerContent());
        // Door
        CreateScriptFile("Assets/Features/Door/Scripts/Model/DoorModel.cs", GetDoorModelContent());
        CreateScriptFile("Assets/Features/Door/Scripts/View/DoorView.cs", GetDoorViewContent());
        CreateScriptFile("Assets/Features/Door/Scripts/Presenter/DoorPresenter.cs", GetDoorPresenterContent());
        CreateScriptFile("Assets/Features/Door/Scripts/Installer/DoorInstaller.cs", GetDoorInstallerContent());
        // Player
        CreateScriptFile("Assets/Features/Player/Scripts/View/PlayerMovement.cs", GetPlayerMovementContent());
        CreateScriptFile("Assets/Features/Player/Scripts/Presenter/PlayerInteraction.cs", GetPlayerInteractionPresenterContent());
        CreateScriptFile("Assets/Features/Player/Scripts/Installer/PlayerInstaller.cs", GetPlayerInstallerContent());
    }
    private static void CreateScriptFile(string path, string content)
    {
        string fullPath = Path.Combine(Application.dataPath, path.Replace("Assets/", ""));
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        if (!File.Exists(fullPath)) { File.WriteAllText(fullPath, content.Trim(), Encoding.UTF8); }
    }
    #region Script Content
    // *** REFACTORED DOOR LOGIC IS HERE ***
    private static string GetDoorModelContent() => @"    
using System;    
namespace RoomPuzzle    
{    
    public class DoorModel    
    {    
        public event Action OnStateChanged;    
        public bool IsOpen { get; private set; }    
        public bool IsLocked { get; private set; }    
        public KeyData RequiredKey { get; }    
        public DoorModel(bool isLocked, KeyData requiredKey)    
        {    
            IsLocked = isLocked;    
            RequiredKey = requiredKey;    
        }    
        /// <summary>    
        /// Contains the core logic for opening the door.    
        /// </summary>    
        /// <returns>True if the door was successfully opened.</returns>    
        public bool AttemptToOpen(InventoryModel inventory)    
        {    
            if (IsOpen) return false; // Already open, no state change.    
            bool canOpen = !IsLocked || (RequiredKey != null && inventory.HasKey(RequiredKey));    
            if (canOpen)    
            {    
                if (IsLocked)    
                {    
                    inventory.UseKey(RequiredKey);    
                }    
                IsLocked = false;    
                IsOpen = true;    
                OnStateChanged?.Invoke(); // Notify listeners (the presenter) of the change.    
                return true;    
            }    
            return false;    
        }    
    }    
}";
    private static string GetDoorPresenterContent() => @"    
using Zenject;    
using System;    
using UnityEngine;    
namespace RoomPuzzle    
{    
    public class DoorPresenter : IInitializable, IDisposable    
    {    
        private readonly DoorView _view;    
        private readonly DoorModel _model;    
        private readonly InventoryModel _inventory;    
        public DoorPresenter(DoorView view, DoorModel model, InventoryModel inventory)    
        {    
            _view = view;    
            _model = model;    
            _inventory = inventory;    
        }    
        public void Initialize()    
        {    
            _view.OnTryOpen += HandleTryOpen;    
            _model.OnStateChanged += HandleStateChanged;    
            // Set initial visual state    
            _view.UpdateVisuals(_model.IsOpen);    
        }    
        public void Dispose()    
        {    
            _view.OnTryOpen -= HandleTryOpen;    
            _model.OnStateChanged -= HandleStateChanged;    
        }    
        // 1. View signals an intent to open.    
        private void HandleTryOpen()    
        {    
            // 2. Presenter asks the model to perform the logic.    
            bool opened = _model.AttemptToOpen(_inventory);    
            // Presenter can provide immediate feedback if logic fails.    
            if (!opened && _model.IsLocked)    
            {    
                Debug.Log(""Door is locked. You don't have the right key."");    
            }    
        }    
        // 3. Model signals that its state has changed.    
        private void HandleStateChanged()    
        {    
            // 4. Presenter updates the view based on the new model state.    
            _view.UpdateVisuals(_model.IsOpen);    
            Debug.Log(""Door opened."");    
        }    
    }    
}";
    // --- Other scripts remain the same ---
    private static string GetIPickupableContent() => @"namespace RoomPuzzle { public interface IPickupable { void Pickup(); } }";
    private static string GetIOpenableContent() => @"namespace RoomPuzzle { public interface IOpenable { void TryOpen(); } }";
    private static string GetGameSceneInstallerContent() => @"    
using UnityEngine;    
using Zenject;    
namespace RoomPuzzle    
{    
    public class GameSceneInstaller : MonoInstaller    
    {    
        public override void InstallBindings()    
        {    
            Container.InstantiatePrefabResource(""Prefabs/Player"", Vector3.zero, Quaternion.identity, null);    
        }    
    }    
}";
    private static string GetGameSettingsInstallerContent() => @"    
using Zenject;    
using UnityEngine;    
namespace RoomPuzzle    
{    
    [CreateAssetMenu(fileName = ""GameSettingsInstaller"", menuName = ""Installers/GameSettingsInstaller"")]    
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>    
    {    
        public override void InstallBindings()    
        {    
            Container.BindInterfacesAndSelfTo<InventoryModel>().AsSingle();    
        }    
    }    
}";
    private static string GetInventoryModelContent() => @"    
using System.Collections.Generic;    
namespace RoomPuzzle    
{    
    public class InventoryModel    
    {    
        private readonly List<KeyData> _keys = new List<KeyData>();    
        public bool AddKey(KeyData key) { if (key == null) return false; _keys.Add(key); return true; }    
        public bool HasKey(KeyData key) => key != null && _keys.Contains(key);    
        public void UseKey(KeyData key) { if (HasKey(key)) _keys.Remove(key); }    
    }    
}";
    private static string GetKeyDataContent() => @"    
using UnityEngine;    
namespace RoomPuzzle    
{    
    [CreateAssetMenu(fileName = ""KeyData"", menuName = ""RoomPuzzle/KeyData"")]    
    public class KeyData : ScriptableObject { public string KeyId; }    
}";
    private static string GetKeyViewContent() => @"    
using UnityEngine;    
using System;    
namespace RoomPuzzle    
{    
    [RequireComponent(typeof(Collider))]    
    public class KeyView : MonoBehaviour, IPickupable    
    {    
        public event Action<KeyView> OnPickedUp;    
        public KeyData KeyData;    
        public void Pickup() => OnPickedUp?.Invoke(this);    
        public void DestroyView() => Destroy(gameObject);    
    }    
}";
    private static string GetKeyPresenterContent() => @"    
using Zenject;    
using System;    
namespace RoomPuzzle    
{    
    public class KeyPresenter : IInitializable, IDisposable    
    {    
        private readonly KeyView _view;    
        private readonly InventoryModel _inventory;    
        public KeyPresenter(KeyView view, InventoryModel inventory) { _view = view; _inventory = inventory; }    
        public void Initialize() => _view.OnPickedUp += HandlePickup;    
        public void Dispose() => _view.OnPickedUp -= HandlePickup;    
        private void HandlePickup(KeyView keyView)    
        {    
            if (_inventory.AddKey(keyView.KeyData)) { keyView.DestroyView(); }    
        }    
    }    
}";
    private static string GetKeyInstallerContent() => @"    
using Zenject;    
namespace RoomPuzzle    
{    
    public class KeyInstaller : MonoInstaller    
    {    
        public override void InstallBindings()    
        {    
            Container.BindInterfacesAndSelfTo<KeyPresenter>().AsSingle().NonLazy();    
            Container.Bind<KeyView>().FromComponentInHierarchy().AsSingle();    
        }    
    }    
}";
    private static string GetDoorViewContent() => @"    
using UnityEngine;    
using System;    
namespace RoomPuzzle    
{    
    public class DoorView : MonoBehaviour, IOpenable    
    {    
        public event Action OnTryOpen;    
        [Header(""Visuals"")]    
        [SerializeField] private Transform _doorVisual;    
        [SerializeField] private Vector3 _openRotation = new Vector3(0, 90, 0);    
        private Vector3 _closedRotation;    
        private void Awake() => _closedRotation = _doorVisual?.localEulerAngles ?? Vector3.zero;    
        public void TryOpen() => OnTryOpen?.Invoke();    
        public void UpdateVisuals(bool isOpen)    
        {    
            if (_doorVisual != null) { _doorVisual.localEulerAngles = isOpen ? _openRotation : _closedRotation; }    
        }    
    }    
}";
    private static string GetDoorInstallerContent() => @"    
using UnityEngine;    
using Zenject;    
namespace RoomPuzzle    
{    
    public class DoorInstaller : MonoInstaller    
    {    
        [SerializeField] private bool _isLocked = true;    
        [SerializeField] private KeyData _requiredKey;    
        public override void InstallBindings()    
        {    
            Container.Bind<DoorModel>().AsSingle().WithArguments(_isLocked, _requiredKey);    
            Container.BindInterfacesAndSelfTo<DoorPresenter>().AsSingle().NonLazy();    
            Container.Bind<DoorView>().FromComponentInHierarchy().AsSingle();    
        }    
    }    
}";
    private static string GetPlayerMovementContent() => @"    
using UnityEngine;    
namespace RoomPuzzle    
{    
    [RequireComponent(typeof(CharacterController))]    
    public class PlayerMovement : MonoBehaviour    
    {    
        [SerializeField] private float _speed = 5f;    
        [SerializeField] private float _gravity = -9.81f;    
        private CharacterController _controller;    
        private Vector3 _velocity;    
        private void Awake() => _controller = GetComponent<CharacterController>();    
        private void Update()    
        {    
            float x = Input.GetAxis(""Horizontal"");    
            float z = Input.GetAxis(""Vertical"");    
            Vector3 move = transform.right * x + transform.forward * z;    
            _controller.Move(move * _speed * Time.deltaTime);    
            if (_controller.isGrounded && _velocity.y < 0) _velocity.y = -2f;    
            _velocity.y += _gravity * Time.deltaTime;    
            _controller.Move(_velocity * Time.deltaTime);    
        }    
    }    
}";
    private static string GetPlayerInteractionPresenterContent() => @"    
using UnityEngine;    
using Zenject;    
namespace RoomPuzzle    
{    
    public class PlayerInteraction : MonoBehaviour    
    {    
        [SerializeField] private float _interactionDistance = 3f;    
        [SerializeField] private LayerMask _interactableLayer;    
        private Camera _mainCamera;    
        [Inject]    
        private void Construct() { _mainCamera = Camera.main; }    
        private void Update()    
        {    
            if (Input.GetKeyDown(KeyCode.E)) TryInteract();    
        }    
        private void TryInteract()    
        {    
            Ray ray = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);    
            if (Physics.Raycast(ray, out RaycastHit hit, _interactionDistance, _interactableLayer))    
            {    
                if (hit.collider.TryGetComponent(out IPickupable pickupable)) { pickupable.Pickup(); }    
                else if (hit.collider.TryGetComponent(out IOpenable openable)) { openable.TryOpen(); }    
            }    
        }    
    }    
}";
    private static string GetPlayerInstallerContent() => @"    
using Zenject;    
namespace RoomPuzzle    
{    
    public class PlayerInstaller : MonoInstaller    
    {    
        public override void InstallBindings()    
        {    
            Container.Bind<PlayerMovement>().FromComponentInHierarchy().AsSingle();    
            Container.Bind<PlayerInteraction>().FromComponentInHierarchy().AsSingle();    
        }    
    }    
}";
    #endregion
}
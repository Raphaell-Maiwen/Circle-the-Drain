using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class RestartGame : MonoBehaviour
{
    [Header("Input")]
    [Tooltip("The Input Actions asset containing the 'LoadSceneShortcut' action in one or more Action Maps.")]
    [SerializeField] private InputActionAsset actions;

    [Tooltip("Exact name of the action to look for in every Action Map.")]
    [SerializeField] private string actionName = "LoadSceneShortcut";

    [Header("Scene")]
    [Tooltip("Name of the scene to load (must be added to Build Settings).")]
    [SerializeField] private string sceneToLoad;

    [Tooltip("Load asynchronously instead of blocking the main thread.")]
    [SerializeField] private bool loadAsync = true;

    // Keep track of every matching action so we can subscribe/unsubscribe cleanly.
    private readonly List<InputAction> _matchingActions = new List<InputAction>();
    private bool _hasTriggered;

    private void Awake()
    {
        if (actions == null)
        {
            Debug.LogError($"[{nameof(RestartGame)}] No InputActionAsset assigned.", this);
            return;
        }

        // Find the action by name in every Action Map that has it.
        foreach (InputActionMap map in actions.actionMaps)
        {
            InputAction action = map.FindAction(actionName, throwIfNotFound: false);
            if (action != null)
            {
                _matchingActions.Add(action);
            }
        }

        if (_matchingActions.Count == 0)
        {
            Debug.LogWarning(
                $"[{nameof(RestartGame)}] No action named \"{actionName}\" found in any Action Map of \"{actions.name}\".",
                this);
        }
    }

    private void OnEnable()
    {
        foreach (InputAction action in _matchingActions)
        {
            action.performed += OnLoadSceneShortcutPerformed;

            // Make sure the action is actually enabled even if its map isn't
            // the "active" one for your gameplay logic. Remove this if you
            // want the shortcut to only work while its map is manually enabled.
            if (!action.enabled)
            {
                action.Enable();
            }
        }
    }

    private void OnDisable()
    {
        foreach (InputAction action in _matchingActions)
        {
            action.performed -= OnLoadSceneShortcutPerformed;
        }
    }

    private void OnLoadSceneShortcutPerformed(InputAction.CallbackContext context)
    {
        if (_hasTriggered) return; // guard against multiple maps firing at once
        _hasTriggered = true;

        if (string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.LogError($"[{nameof(RestartGame)}] No scene name set.", this);
            return;
        }

        Debug.Log($"[{nameof(RestartGame)}] LB+RB+Start detected, loading scene \"{sceneToLoad}\".");

        if (loadAsync)
        {
            SceneManager.LoadSceneAsync(sceneToLoad);
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
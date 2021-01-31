using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    public bool disableUIOnStartup = true;

    [SerializeField]
    public bool enableUIToggle = true;

    public bool UIEnabled { get { return children != null && children.Count > 0 && children[0].gameObject.activeInHierarchy; } }

    private List<GameObject> children;

    [SerializeField]
    protected LevelData levelData = null;

    [SerializeField]
    private List<GameObject> uiViews = null;
    [SerializeField]
    private int defaultUIViewIndex = 0;

    [SerializeField]
    private GameObject backButton = null;

    public UnityEvent OnUIOpen;

    public UnityEvent OnUIClosed;

    protected void Awake()
    {
        children = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            children.Add(child);
        }

        // Activates the default UI view
        ToggleUIView(defaultUIViewIndex);

        // Disables all children
        ToggleChildren(!disableUIOnStartup);
    }

    protected void ToggleChildren(bool enabled)
    {
        foreach (GameObject child in children)
        {
            child.SetActive(enabled);
        }
    }

    public void ToggleUI()
    {
        Debug.Log("Toggling UI");

        foreach (GameObject child in children)
        {
            child.SetActive(!UIEnabled);
        }

        ToggleUIView(defaultUIViewIndex);

        if (UIEnabled)
        {
            OnUIOpen?.Invoke();
        }
        else
        {
            OnUIClosed?.Invoke();
        }
    }

    public void ToggleUIView(int uiViewIndex)
    {
        if (uiViews != null && uiViewIndex >= 0 && uiViewIndex < uiViews.Count)
        {
            foreach (GameObject uiView in uiViews)
            {
                uiView.SetActive(false);
            }
            uiViews[uiViewIndex].SetActive(true);

            if (backButton != null)
            {
                backButton.SetActive(uiViewIndex != defaultUIViewIndex);
            }
        }
        else
        {
            Debug.LogError("UI View Index out of Bounds");
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(levelData.currentLevel);
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(levelData.Levels[index].levelName);
    }

    public void LoadMainMenu()
    {
        LoadScene(levelData.MainMenu);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting");
        Application.Quit();
    }
}

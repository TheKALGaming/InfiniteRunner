using UnityEngine;

public class UIMainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneLoaderService.LoadGame();
    }

    public void QuitGame()
    {
        // # pour quand on est dans l'editeur Unity.
        #if UNITY_EDITOR
        Application.Quit();
        #else
        UnityEditor.EditorApplication.isPaying = false;
        #endif
    }
}

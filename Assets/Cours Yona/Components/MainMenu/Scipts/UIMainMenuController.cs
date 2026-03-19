using TMPro;
using UnityEngine;

public class UIMainMenuController : MonoBehaviour
{
    [SerializeField] private TMP_Text _runCountText;
    private SaveData _saveData;

    private void Start()
    {
        var saveData = SaveService.Load();
        _saveData = saveData ?? new SaveData();

        _runCountText.text = "Runs: " + _saveData.RunCount;
    }

    public void StartGame()
    {
        _saveData.RunCount++;
        SaveService.Save(_saveData);

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

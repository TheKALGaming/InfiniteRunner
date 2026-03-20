using System;
using TMPro;
using UnityEngine;

public class UIMainMenuController : MonoBehaviour
{
    [SerializeField] private TMP_Text _runCountText;
    [SerializeField] private TMP_Text _bestTimeText;
    private SaveData _saveData;

    private void Start()
    {
        _saveData = SaveService.Load();
        _runCountText.text = "Runs: " + _saveData.RunCount;

        if (_saveData.BestTime == 0)
        {
            _bestTimeText.text = "No Best Time";
        }
        else
        {
            var timeSpan = new TimeSpan(0, 0, _saveData.BestTime);
            _bestTimeText.text = "Best Time: " + timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00");
        }
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

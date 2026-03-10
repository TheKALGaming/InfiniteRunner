using TMPro;
using UnityEngine;

public class UILifeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _lifeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        EventSystem.OnPlayerLifeUpdate += HandlePlayerLifeUpdated;
    }

    private void OnDestroy()
    {
        EventSystem.OnPlayerLifeUpdate -= HandlePlayerLifeUpdated;
    }

    private void HandlePlayerLifeUpdated(int newLifeCount)
    {
        _lifeText.text = "Lives: " + newLifeCount;
    }
}

using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject WinLosePanel;
    [SerializeField] private Button tryAgainButton, abandonButton;
    private void Awake()
    {
        InitListeners();
    }

    private void InitListeners()
    {
        RemoveListeners();
        EventManager.GameLost.AddListener(ShowLoseSplashScreen);
    }

    private void RemoveListeners()
    {
        EventManager.GameLost.RemoveListener(ShowLoseSplashScreen);
    }

    private void ShowLoseSplashScreen()
    {
        WinLosePanel.SetActive(true);
        tryAgainButton.onClick.AddListener(TryAgainClicked);
    }

    private void TryAgainClicked()
    {
        EventManager.RestartGame.Invoke();
        WinLosePanel.SetActive(false);
        tryAgainButton.onClick.RemoveAllListeners();
    }
}

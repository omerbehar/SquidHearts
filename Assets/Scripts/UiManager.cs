using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject WinLosePanel;
    [SerializeField] private Button tryAgainButton, abandonButton;
    [SerializeField] private TextMeshProUGUI blobCountText;
    [SerializeField] private TextMeshProUGUI isoTimerText;
    [SerializeField] private TextMeshProUGUI winLoseText;
    private void Awake()
    {
        InitListeners();
    }

    private void InitListeners()
    {
        RemoveListeners();
        EventManager.GameLost.AddListener(ShowLoseSplashScreen);
        EventManager.BlobCreated.AddListener(UpdateBlobCountText);
        EventManager.UpdateIsoTimer.AddListener(OnIsoTimerUpdate);
        EventManager.ReachEscapeButton.AddListener(OnWin);
    }

    private void OnIsoTimerUpdate(float isoTime)
    {
        isoTime = (float)Math.Round(isoTime, 1);
        isoTimerText.text = isoTime.ToString();
    }

    private void UpdateBlobCountText(Blob blob, int blobCount)
    {
        blobCountText.text = blobCount.ToString();
    }

    private void RemoveListeners()
    {
        EventManager.GameLost.RemoveListener(ShowLoseSplashScreen);
        EventManager.UpdateIsoTimer.RemoveListener(OnIsoTimerUpdate);
        EventManager.BlobCreated.RemoveListener(UpdateBlobCountText);
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
     private void OnWin()
    {
        WinLosePanel.SetActive(true);
        winLoseText.text = "You win!?\nHOW???";
        tryAgainButton.onClick.AddListener(TryAgainClicked);
    }


}

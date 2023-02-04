using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private float tickTime = 1.37f;
    private float timeFromLastTick;
    [SerializeField] public PovState povState = PovState.Front;
    [SerializeField] private Camera zCamera, xCamera, isoCamera;
    [SerializeField] private GameObject zCameraWall, xCameraWall;
    [SerializeField] private Blob cage;
    [SerializeField] public int blobAmount = 15;
    private int restartLevelBlobAmount;
    private bool wasCageCreated;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitEventListeners();
        cage.isMovable = false;

            Grid.AddBlobPart(cage.GridPosition, cage);
        
        RestartGameInitData();
    }

    private void RestartGameInitData()
    {
        restartLevelBlobAmount = blobAmount;
    }


    void Update()
    {
        Ticker();
    }
    private void InitEventListeners()
    {
        RemoveEventListeners();
        EventManager.PovChanged.AddListener(OnPovChanged);
        EventManager.ThreeDimensionsViewActivated.AddListener(On3DActivated);
        EventManager.RestartGame.AddListener(OnRestartGame);
    }

    private void On3DActivated()
    {
        povState = povState == PovState.Front ? PovState.Iso : PovState.Front;
        if (povState == PovState.Front)
        {
            isoCamera.enabled = false;
            xCamera.enabled = true;
            xCameraWall.SetActive(true);
        }
        else
        {
            isoCamera.enabled = true;
            xCamera.enabled = false;
            xCameraWall.SetActive(false);
        }
    }

    private void RemoveEventListeners()
    {
        EventManager.PovChanged.RemoveListener(OnPovChanged);
        EventManager.ThreeDimensionsViewActivated.RemoveListener(On3DActivated);
        EventManager.RestartGame.RemoveListener(OnRestartGame);
    }

    private void OnRestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        blobAmount = restartLevelBlobAmount;
        Grid.ClearGrid();
        Grid.AddBlobPart(cage.GridPosition, cage);
    }

    private void OnPovChanged()
    {
        povState = povState == PovState.Front ? PovState.Side : PovState.Front;
        if (povState == PovState.Front)
        {
            zCamera.enabled = false;
            zCameraWall.SetActive(false);
            xCamera.enabled = true;
            xCameraWall.SetActive(true);
        }
        else
        {
            zCamera.enabled = true;
            zCameraWall.SetActive(true);
            xCamera.enabled = false;
            xCameraWall.SetActive(false);
        }
    }

    private void Ticker()
    {
        if (timeFromLastTick > tickTime)
        {
            EventManager.Tick.Invoke();
            timeFromLastTick = 0;
        }
        else
        {
            timeFromLastTick += Time.deltaTime;
        }
    }
}

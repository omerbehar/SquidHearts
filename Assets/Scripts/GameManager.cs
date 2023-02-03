using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private float tickTime = 1.37f;
    private float timeFromLastTick;
    public PovState povState = PovState.Front;
    [SerializeField] private Camera zCamera, xCamera;
    [SerializeField] private Blob cage;
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
    }


    void Update()
    {
        Ticker();
    }
    private void InitEventListeners()
    {
        RemoveEventListeners();
        EventManager.PovChanged.AddListener(OnPovChanged);
    }

    private void RemoveEventListeners()
    {
        EventManager.PovChanged.RemoveListener(OnPovChanged);
    }

    private void OnPovChanged()
    {
        povState = povState == PovState.Front ? PovState.Side : PovState.Front;
        if (povState == PovState.Front)
        {
            zCamera.enabled = false;
            xCamera.enabled = true;
        }
        else
        {
            zCamera.enabled = true;
            xCamera.enabled = false;
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

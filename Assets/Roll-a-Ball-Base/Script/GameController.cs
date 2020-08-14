using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

class GameController : MonoBehaviour
{
    //Singlton
    static GameController _S = null;
    private GameController()
    {
    }

    [SerializeField] Canvas canvas;
    [SerializeField] Image scoreImage;
    [SerializeField] GameObject winnerLabelObject;
    [SerializeField] GameObject winnerLabel;
    [SerializeField] GameObject gameStartCanvas;
    [SerializeField] ItemManager itemManager;
    public GameObject player;
    public bool isStarted = false;
    ReadyCanvas readyCanvas;


    [Header("ほめ言葉")]
    public string[] winnerComment = { "★★★", "★★", "★" };
    public float goodTime = 5.0f;
    public float normalTime = 10.0f;

    private int initialCount;
    private int currentCount;

    private ScoreController scoreController;
    private bool isStart = false;
    private float pastTime = 0.0f;


    public enum GameState
    {
        Awake, Ready, Play, Fault, Clear
    };
    public static GameState _gameState
    {
        get;
        private set;
    } = GameState.Awake;


    private void Awake()
    {
        if (_S == null)
        {
            _S = this;
        }
        else
        {
            Debug.LogError("Duplicated GameController");
        }
        canvas.gameObject.SetActive(true);
        ItemManager[] itemManagers = FindObjectsOfType<ItemManager>();
        if (itemManagers.Length > 1)
        {
            Debug.LogError("Duplicated ItemManager,assigne 1st Item");
        }
        itemManager = itemManagers[0];
    }
    private void OnEnable()
    {


    }
    void Start()
    {
        _gameState = GameState.Ready;
        readyCanvas = ReadyCanvas.GetInstance();
        readyCanvas.SetActive();
        readyCanvas.terminate = GameStart;

        initialCount = itemManager.lastCount;
        scoreController = scoreImage.GetComponent<ScoreController>();
        scoreController.SetGageUnit(initialCount);
        scoreController.ChangeValue(1);

    }

    void Update()
    {
        if (!isStart)
        {
            if (isStarted)
            {
                isStart = true;
                gameStartCanvas.SetActive(false);
                pastTime = 0.0f;
            }
        }
        else
        {
            pastTime += Time.deltaTime;
        }
    }

    public static void CatchItem(int lastItemCount)
    {
        _S.scoreController.ChangeValue((float)lastItemCount / _S.initialCount);
        if (lastItemCount == 0)
        {
            Clear();
        }
    }

    public void GameStart()
    {
        gameStartCanvas.SetActive(false);
        pastTime = 0.0f;
    }

    public static void Clear()
    {
        // オブジェクトをアクティブにする
        _S.winnerLabelObject.SetActive(true);
        if (_S.pastTime < _S.goodTime)
        {
            _S.winnerLabel.GetComponent<Text>().text += "\n" + _S.winnerComment[0];
        }
        else if (_S.pastTime < _S.normalTime)
        {
            _S.winnerLabel.GetComponent<Text>().text += "\n" + _S.winnerComment[1];
        }
        else
        {
            _S.winnerLabel.GetComponent<Text>().text += "\n" + _S.winnerComment[2];
        }
        _S.player.GetComponent<PlayerController>().SetClear();
    }

    int sceneIndex;
    float waitTimeSave;
    public void ResetScene(float waitTime)
    {
        waitTimeSave = waitTime;
        Debug.Log("RESET");
        StartCoroutine(ResetSceneCoroutine());
    }
    private IEnumerator ResetSceneCoroutine()
    {
        yield return new WaitForSeconds(waitTimeSave);
        //現在のシーン番号を取得
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        //現在のシーンを再読込する
        SceneManager.LoadScene(sceneIndex);

    }

}

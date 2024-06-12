using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
    }
    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion
    
    public GameObject player;
    public Rigidbody2D playerRigid;
    public GrappingHook playerHook;
    public Transform shotPosition;
    public float shotingPower;
    public int currentStar;

    public ParticleSystem deadParticleSystem;
    private bool _isGameEndCoroutine;

    public CameraMove cameraMove;

    public int Score { get; private set; }
    public int Level { get; private set; }

    public bool isGameStart { get; private set; } // UI 사라지고 클릭하면 게임 시작으로 바뀌어야 함
    public bool isShoot { get; private set; }

    public UnityEvent PlayerDeathEvent;

    void Start()
    {
        InitializeGame();
    }

    void Update()
    {
        HandleInput();
        CheckPlayerDeath();
    }

    private void InitializeGame()
    {
        currentStar = DataManager.Instance.GameDataLoad().star;

        playerHook = player.GetComponent<GrappingHook>();
        playerRigid = player.GetComponent<Rigidbody2D>();
        playerRigid.gravityScale = 0;
        player.transform.position = shotPosition.position;
        isGameStart = false;
        isShoot = false;
        Level = 0;
        Score = 0;
        // cameraMove = GameObject.Find("Main").GetComponent<CameraMove>();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0) && isGameStart && !isShoot)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        playerRigid.gravityScale = 1;
        player.SetActive(true);
        player.transform.position = shotPosition.position;
        playerRigid.AddForce(shotPosition.right * shotingPower, ForceMode2D.Impulse);
        cameraMove.target = player.transform;
        isGameStart = false;
        isShoot = true;
    }

    private void CheckPlayerDeath()
    {
        if (player.transform.position.y < -40 && _isGameEndCoroutine == false)
        {
            StartCoroutine(PlayerDead());
        }
    }

    public void ScoreUp(int value)
    {
        Score += value;
    }
    
    public void LevelUp(int value)
    {
        Level += value;
    }

    public void IsGameStartChangeValue(bool value)
    {
        isGameStart = value;
    }
    
    public bool IsGameStartValue()
    {
        return isGameStart;
    }

    public void SaveData()
    {
        //DataManager.Instance.GameDataSave(currentStar, Score);
    }

    public IEnumerator PlayerDead()
    {
        _isGameEndCoroutine = true;
        deadParticleSystem.transform.position = player.transform.position;
        playerRigid.gravityScale = 0;
        player.SetActive(false);
        if (!deadParticleSystem.isPlaying)
        {
            deadParticleSystem.Play();
        }
        yield return new WaitForSeconds(2f);
        player.SetActive(true);
        PlayerDeathEvent.Invoke();
        player.transform.position = shotPosition.position;
        playerHook.isHookActive = false;
        playerHook.isLineMax = false;
        playerHook.isAttach = false;
        IsGameStartChangeValue(false);
        isShoot = false;
        UIManager.Instance.ScoreTextActive(false);
        _isGameEndCoroutine = false;
    }
}

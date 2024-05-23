using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
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
    
    public GameObject player;
    public Transform shotPosition;
    public float shotingPower;

    public CameraMove cameraMove;

    public int Score;
    public int Level;

    private bool isGameStart;
    
    public UnityEvent PlayerDeath;
    
    // Start is called before the first frame update
    void Start()
    {
        isGameStart = false;
        Level = 0;
        //cameraMove = GameObject.Find("Main").GetComponent<CameraMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isGameStart == false)
        {
            //var playerObject = Instantiate(player);
            player.transform.position = shotPosition.position;
            player.GetComponent<Rigidbody2D>().AddForce(shotPosition.right * shotingPower, ForceMode2D.Impulse);
            cameraMove.target = player.transform;
            isGameStart = true;
        }

        if (player.transform.position.y < -40)
        {
            PlayerDeath.Invoke();
        }
    }
}

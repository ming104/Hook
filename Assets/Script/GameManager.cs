using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public Transform shotPosition;
    public float shotingPower;

    public CameraMove cameraMove;

    private bool isGameStart;
    // Start is called before the first frame update
    void Start()
    {
        isGameStart = false;
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
    }
}

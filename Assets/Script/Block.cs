using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameObject Player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameStartValue() == true)
        {
            Destroy(gameObject);
        }
        if (Player.transform.position.x - transform.position.x > 80f)
        {
            GameManager.Instance.ScoreUp(1);
            if (GameManager.Instance.Score % 25 == 0)
            {
                GameManager.Instance.LevelUp(1);
            }
            Destroy(gameObject);
        }
    }
}

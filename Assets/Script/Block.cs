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
        if (Player.transform.position.x - transform.position.x > 80f)
        {
            GameManager.Instance.Score++;
            if (GameManager.Instance.Score % 50 == 0)
            {
                GameManager.Instance.Level++;
            }
            Destroy(gameObject);
        }
    }
}

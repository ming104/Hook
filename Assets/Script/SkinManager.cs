using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SkinRandomPick()
    {
        int randomnumber = Random.Range(1, 100);
        if (0 < randomnumber && randomnumber <= 70)
        {
            Debug.Log("Common");
        }
        else if (70 < randomnumber && randomnumber <= 90)
        {
            Debug.Log("rare");
        }
        else if (90 < randomnumber && randomnumber <= 100)
        {
            Debug.Log("epic");
        }
    }
}

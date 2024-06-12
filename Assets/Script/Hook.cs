 using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private GrappingHook _grapping;
    public DistanceJoint2D joint2D;
    
    // Start is called before the first frame update
    void Start()
    {
        _grapping = GameObject.Find("Player").GetComponent<GrappingHook>();
        joint2D = GetComponent<DistanceJoint2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ring"))
        {
            joint2D.enabled = true;
            _grapping.isAttach = true;
        }
    }
}

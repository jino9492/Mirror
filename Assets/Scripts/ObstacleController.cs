﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 5));
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.gameObject.SetActive(false);
    }
}

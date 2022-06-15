using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashEffect : MonoBehaviour
{
    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Furry").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
            transform.position = player.transform.position;
    }
}

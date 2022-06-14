using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReplicator : MonoBehaviour
{
    public GameObject replicatedPlayer;
    CameraController cam;
    GameObject player;
    
    void Awake(){
        replicatedPlayer = Instantiate(Resources.Load("Prefabs/Furry")) as GameObject;
        replicatedPlayer.transform.name = "Furry Clone";
        replicatedPlayer.GetComponent<PlayerController>().crashingObjectName = "Furry";
    }

    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
        player = GameObject.Find("Furry");

        replicatedPlayer.transform.position = new Vector3(replicatedPlayer.transform.position.x, cam.transform.position.y + (cam.transform.position.y - replicatedPlayer.transform.position.y), 0);
        replicatedPlayer.transform.rotation = Quaternion.Euler(0, 180, 180);
        replicatedPlayer.GetComponent<Rigidbody2D>().gravityScale *= -1;
        replicatedPlayer.GetComponent<PlayerController>().cloneFlag = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(replicatedPlayer.transform.position.x - player.transform.position.x) > 0.1f)
            replicatedPlayer.transform.position = new Vector3(player.transform.position.x , replicatedPlayer.transform.position.y, replicatedPlayer.transform.position.z);
    }
}

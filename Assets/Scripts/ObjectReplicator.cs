using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReplicator : MonoBehaviour
{
    CameraController cam;
    GameObject replicatedPlayer;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<CameraController>();

        replicatedPlayer = Instantiate(Resources.Load("Prefabs/Furry")) as GameObject;
        replicatedPlayer.transform.position = new Vector3(replicatedPlayer.transform.position.x, cam.transform.position.y + (cam.transform.position.y - replicatedPlayer.transform.position.y), 0);
        replicatedPlayer.transform.rotation = Quaternion.Euler(0, 180, 180);
        replicatedPlayer.GetComponent<Rigidbody2D>().gravityScale *= -1;
        replicatedPlayer.GetComponent<PlayerController>().cloneFlag = -1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

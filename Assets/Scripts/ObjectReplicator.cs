using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReplicator : MonoBehaviour
{
    public GameObject replicatedPlayer;
    private GameObject[] ObstacleObjects;
    private GameObject[] resetJump;
    CameraController cam;
    GameObject player;
    
    void Awake(){
        ObstacleObjects = GameObject.FindGameObjectsWithTag("Obstacle");
        resetJump = GameObject.FindGameObjectsWithTag("ResetJump");
        replicatedPlayer = Instantiate(Resources.Load("Prefabs/Furry")) as GameObject;
        replicatedPlayer.transform.name = "Furry Clone";

        cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
        player = GameObject.Find("Furry");

        foreach (GameObject obstacle in ObstacleObjects)
        {
            GameObject newObstacle = Instantiate(obstacle) as GameObject;
            newObstacle.transform.position = new Vector3(newObstacle.transform.position.x, cam.transform.position.y + (cam.transform.position.y - newObstacle.transform.position.y), 0);
            if (newObstacle.transform.position.y > cam.transform.position.y && newObstacle.GetComponent<BoxCollider2D>() != null)
            {
                newObstacle.transform.position =
                    new Vector3(
                        newObstacle.transform.position.x,
                        newObstacle.transform.position.y - newObstacle.GetComponent<BoxCollider2D>().offset.y,
                        newObstacle.transform.position.z
                        );
            }

            newObstacle.transform.SetParent(GameObject.Find("Obstacles").transform);
            if (newObstacle.GetComponent<SpriteRenderer>() != null)
                newObstacle.GetComponent<SpriteRenderer>().sprite = null;
            else
                newObstacle.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        }

        foreach (GameObject obstacle in resetJump)
        {
            GameObject newObstacle = Instantiate(obstacle) as GameObject;
            newObstacle.transform.position = new Vector3(newObstacle.transform.position.x, cam.transform.position.y + (cam.transform.position.y - newObstacle.transform.position.y), 0);
            if (newObstacle.transform.position.y > cam.transform.position.y && newObstacle.GetComponent<BoxCollider2D>() != null)
            {
                newObstacle.transform.position =
                    new Vector3(
                        newObstacle.transform.position.x,
                        newObstacle.transform.position.y - newObstacle.GetComponent<BoxCollider2D>().offset.y,
                        newObstacle.transform.position.z
                        );
            }

            newObstacle.GetComponent<ParticleSystem>().startColor = new Vector4(0,0,0,0f);
            newObstacle.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = new Vector4(0, 0, 0, 0f);

            newObstacle.transform.SetParent(GameObject.Find("Obstacles").transform);
        }
    }

    void Start()
    {
        replicatedPlayer.transform.SetParent(GameObject.Find("Players").transform);
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

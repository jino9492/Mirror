using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReplicator : MonoBehaviour
{
    public GameObject replicatedPlayer;
    private GameObject[] ObstacleObjects;
    private GameObject[] HarmfulObstacleObjects;
    private GameObject[] resetJump;
    CameraController cam;
    GameObject player;
    
    void Awake(){
        ObstacleObjects = GameObject.FindGameObjectsWithTag("Obstacle");
        HarmfulObstacleObjects = GameObject.FindGameObjectsWithTag("HarmfulObstacle");
        resetJump = GameObject.FindGameObjectsWithTag("ResetJump");
        cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
        player = GameObject.Find("Furry");

        ReplicatePlayer(player);

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

        foreach (GameObject obstacle in HarmfulObstacleObjects)
        {
            if (!obstacle.name.Contains("Camera"))
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

    }

    // Update is called once per frame
    void Update()
    {
        if(replicatedPlayer != null && player != null)
            replicatedPlayer.transform.position = 
                new Vector3(
                    player.transform.position.x ,
                    cam.transform.position.y + (cam.transform.position.y - player.transform.position.y),
                    player.transform.position.z
                    );
    }

    public GameObject ReplicatePlayer(GameObject player)
    {
        this.player = player;
        replicatedPlayer = Instantiate(Resources.Load("Prefabs/Furry")) as GameObject;
        replicatedPlayer.transform.name = "Furry Clone";
        replicatedPlayer.transform.SetParent(GameObject.Find("Players").transform);
        replicatedPlayer.transform.position = new Vector3(player.transform.position.x, cam.transform.position.y + (cam.transform.position.y - player.transform.position.y), 0);
        replicatedPlayer.transform.rotation = Quaternion.Euler(0, 180, 180);
        replicatedPlayer.GetComponent<Rigidbody2D>().gravityScale *= -1;
        replicatedPlayer.GetComponent<PlayerController>().cloneFlag = -1;

        return replicatedPlayer;
    }
}

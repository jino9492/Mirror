using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraController cam;

    public static bool isPlayerDead;
    private bool playerCanReset;
    private float playerResetTimer = 0f;
    private float playerResetTime = 1f;
    private GameObject[] players = new GameObject[2];

    private float regenerateTime = 2f;
    private GameObject[] resets;

    private ObjectReplicator objRepl;

    // Start is called before the first frame update
    void Start()
    {
        resets = GameObject.FindGameObjectsWithTag("ResetJump");

        players = GameObject.FindGameObjectsWithTag("Player");
        objRepl = GetComponent<ObjectReplicator>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject reset in resets)
        {
            if (!reset.transform.gameObject.activeSelf)
            {
                reset.GetComponent<ResetJump>().timer += Time.deltaTime;
                if (reset.GetComponent<ResetJump>().timer > regenerateTime)
                {
                    reset.transform.gameObject.SetActive(true);
                    reset.GetComponent<ResetJump>().timer = 0f;
                }
            }
        
        }

        if (isPlayerDead)
        {
            playerResetTimer += Time.deltaTime;
            if(playerResetTimer > playerResetTime)
            {
                playerCanReset = true;
            }
        }

        if (Input.anyKeyDown && playerCanReset)
        {
            playerCanReset = false;
            isPlayerDead = false;
            playerResetTimer = 0f;
            GameObject playerObj = Instantiate(Resources.Load("Prefabs/Furry"), PlayerController.lastObstacle.transform.position + new Vector3(0, 1f, 0), Quaternion.identity) as GameObject;
            cam.playerTransform = playerObj.transform;
            playerObj.name = "Furry";
            objRepl.ReplicatePlayer(playerObj);
        }

        print(PlayerController.lastObstacle.transform.position);
    }
}

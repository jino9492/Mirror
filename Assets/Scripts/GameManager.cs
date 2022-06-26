using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public CameraController cam;

    public static bool isPlayerDead;
    private bool playerCanReset;
    private float playerResetTimer = 0f;
    private float playerResetTime = 1f;
    private GameObject[] players = new GameObject[2];

    private float regenerateTime = 2f;
    private GameObject[] resetObjects;
    private ResetJump[] resets;

    private ObjectReplicator objRepl;

    // Start is called before the first frame update
    void Start()
    {
        resetObjects = GameObject.FindGameObjectsWithTag("ResetJump");

        resets = new ResetJump[resetObjects.Length];
        foreach (var resetObject in resetObjects.Select((value, index) => (value, index)))
            resets[resetObject.index] = resetObject.value.GetComponent<ResetJump>();

        players = GameObject.FindGameObjectsWithTag("Player");
        objRepl = GetComponent<ObjectReplicator>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(ResetJump reset in resets)
        {
            if (!reset.transform.gameObject.activeSelf)
            {
                reset.timer += Time.deltaTime;
                if (reset.timer > regenerateTime)
                {
                    reset.transform.gameObject.SetActive(true);
                    reset.timer = 0f;
                }
            }
        
        }

        if (isPlayerDead)
        {
            if (players[0] != null)
                Destroy(players[0]);
            if (players[1] != null)
                Destroy(players[1]);

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

            Vector3 savePoint = PlayerController.lastObstacle.transform.position;
            if (PlayerController.lastObstacle.GetComponent<ObjectMover>().hasOwnSavePoint)
            {
                savePoint = savePoint + (Vector3)PlayerController.lastObstacle.GetComponent<ObjectMover>().savePointOffset;
            }

            GameObject playerObj = Instantiate(Resources.Load("Prefabs/Furry"), savePoint + new Vector3(0, 1f, 0), Quaternion.identity) as GameObject;
            players[0] = playerObj;
            cam.playerTransform = playerObj.transform;
            playerObj.name = "Furry";

            GameObject ClonePlayerObj = objRepl.ReplicatePlayer(playerObj);
            players[1] = ClonePlayerObj;
        }
    }
}

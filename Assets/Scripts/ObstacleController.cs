using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private CameraController cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).Rotate(new Vector3(0, 0, 5));
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        print(collider.name);

        cam.StartCoroutine("CameraShake");

        if (collider.name == "Furry Clone")
            Instantiate(Resources.Load("Prefabs/ReplicatedPlayerBloodParticle"), collider.transform.position, collider.transform.rotation);
        else if (collider.name == "Furry")
            Instantiate(Resources.Load("Prefabs/PlayerBloodParticle"), collider.transform.position, collider.transform.rotation);


        if (collider.CompareTag("Player"))
        {
            GameManager.isPlayerDead = true;
            Destroy(collider.gameObject);
        }
    }
}

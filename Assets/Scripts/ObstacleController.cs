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
        /*if (collider.name == "Furry")
            collider.transform.position = new Vector3(-3, -2, 0);
        else
            collider.transform.position = new Vector3(-3, 2, 0);*/

        cam.StartCoroutine("CameraShake");

        GameObject bloodParticle = Instantiate(Resources.Load("Prefabs/BloodParticle"), collider.transform.position, collider.transform.rotation) as GameObject;

        if (collider.name == "Furry Clone")
            bloodParticle.GetComponent<ParticleSystem>().gravityModifier *= -1;

        collider.gameObject.SetActive(false);
    }
}

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public Transform playerTransform;
	public float m_speed = 0.1f;
	public GameObject background;
	Camera mycam;

	public void Start()
	{
		mycam = GetComponent<Camera> ();
		background = GameObject.Find("BackGround");
	}

	public void Update()
	{

		if (playerTransform) 
		{
			transform.position = Vector3.Lerp(transform.position, playerTransform.position, m_speed) + new Vector3(0, 0, -12);
			background.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
		}


	}
}

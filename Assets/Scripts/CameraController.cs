using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public Transform playerTransform;
	public float m_speed = 0.1f;
	public float Y_cameraOffset = 0f;
	public GameObject background;
	public GameObject mirror;
	private Camera mycam;

	public float shakeDuration;
	public float magnitude;

	public void Start()
	{
		mycam = GetComponent<Camera> ();
		background = GameObject.Find("BackGround");
		mirror = GameObject.Find("Mirror");
	}

	public void Update()
	{

		if (playerTransform) 
		{
			transform.position = Vector3.Lerp(transform.position, new Vector3(playerTransform.position.x, Y_cameraOffset, playerTransform.position.z), m_speed) + new Vector3(0, 0, -12);
			background.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
			mirror.transform.position = new Vector3(transform.position.x, mirror.transform.position.y, mirror.transform.position.z);
		}


	}

	public IEnumerator CameraShake()
    {
		float elapsed = 0.0f;
		Vector3 originalPosition = transform.position;

		while (elapsed < shakeDuration)
        {
			float x = Random.Range(-1f, 1f) * magnitude;
			float y = Random.Range(-1f, 1f) * magnitude;

			transform.position = Vector3.Lerp(transform.position, new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z), 0.1f);

			elapsed += Time.deltaTime;

			yield return null;
		}
    }
}

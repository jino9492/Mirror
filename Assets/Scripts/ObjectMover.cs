using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float verticalSize;
    public float horizontalSize;
    public float speed;

    private GameObject leftEnd;
    private GameObject rightEnd;
    private GameObject topEnd;
    private GameObject bottomEnd;

    private int cloneFlag = 1;

    public bool isRight;
    public bool isLeft;
    public bool isTop;
    public bool isBottom;

    public float waittingTime;
    private float timer;
    private float speedScaler = 1;
    private CameraController cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();

        if (transform.name.Contains("Clone"))
            cloneFlag = -1;

        if (verticalSize != 0)
        {
            isLeft = true;

            leftEnd = new GameObject("LeftEnd");
            rightEnd = new GameObject("RightEnd");

            leftEnd.transform.SetParent(GameObject.Find("Anchors").transform);
            leftEnd.transform.position = new Vector3(transform.position.x - verticalSize / 2f, transform.position.y, transform.position.z);

            rightEnd.transform.SetParent(GameObject.Find("Anchors").transform);
            rightEnd.transform.position = new Vector3(transform.position.x + verticalSize / 2f, transform.position.y, transform.position.z);
        }
        else if(horizontalSize != 0)
        {
            isBottom = true;

            topEnd = new GameObject("TopEnd");
            bottomEnd = new GameObject("BottomEnd");

            topEnd.transform.SetParent(GameObject.Find("Anchors").transform);
            topEnd.transform.position = new Vector3(transform.position.x, transform.position.y + horizontalSize / 2f * cloneFlag, transform.position.z);

            bottomEnd.transform.SetParent(GameObject.Find("Anchors").transform);
            bottomEnd.transform.position = new Vector3(transform.position.x, transform.position.y - horizontalSize / 2f * cloneFlag, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (verticalSize != 0)
        {
            if (isRight)
            {
                transform.position = Vector3.MoveTowards(transform.position, leftEnd.transform.position, speed * speedScaler * Time.deltaTime);
                if (transform.position == leftEnd.transform.position)
                {
                    timer += Time.deltaTime;

                    if (timer > waittingTime)
                    {
                        isLeft = true;
                        isRight = false;
                        timer = 0f;
                    }
                }
            }
            else if (isLeft)
            {
                transform.position = Vector3.MoveTowards(transform.position, rightEnd.transform.position, speed * speedScaler * Time.deltaTime);
                if (transform.position == rightEnd.transform.position)
                {
                    timer += Time.deltaTime;

                    if (timer > waittingTime)
                    {
                        isLeft = false;
                        isRight = true;
                        timer = 0f;
                    }
                }
            }
        }
        else if (horizontalSize != 0)
        {
            if (isTop)
            {
                transform.position = Vector3.MoveTowards(transform.position, bottomEnd.transform.position, speed * speedScaler * Time.deltaTime);
                if (transform.position == bottomEnd.transform.position)
                {
                    timer += Time.deltaTime;

                    if (timer > waittingTime)
                    {
                        isBottom = true;
                        isTop = false;
                        timer = 0f;
                    }
                }
            }
            else if (isBottom)
            {
                transform.position = Vector3.MoveTowards(transform.position, topEnd.transform.position, speed * speedScaler * Time.deltaTime);
                if (transform.position == topEnd.transform.position)
                {
                    timer += Time.deltaTime;

                    if (timer > waittingTime)
                    {
                        isBottom = false;
                        isTop = true;
                        timer = 0f;
                    }
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Foot"))
        {
            other.transform.parent.transform.SetParent(transform);
            if (!transform.CompareTag("HarmfulObstacle") && transform.position.y < cam.transform.position.y)
            {
                PlayerController.lastObstacle = gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Foot") && other.gameObject.activeSelf)
        {
            other.transform.parent.transform.SetParent(GameObject.Find("Players").transform);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if (verticalSize != 0)
        {
            if (leftEnd == null)
                Gizmos.DrawLine(
                    new Vector3(transform.position.x - verticalSize / 2f, transform.position.y, transform.position.z),
                    new Vector3(transform.position.x + verticalSize / 2f, transform.position.y, transform.position.z)
                    );
            else
                Gizmos.DrawLine(leftEnd.transform.position, rightEnd.transform.position);
        }
        else if (horizontalSize != 0)
        {
            if (topEnd == null)
                Gizmos.DrawLine(
                    new Vector3(transform.position.x, transform.position.y + horizontalSize / 2f, transform.position.z),
                    new Vector3(transform.position.x, transform.position.y - horizontalSize / 2f, transform.position.z)
                    );
            else
                Gizmos.DrawLine(topEnd.transform.position, bottomEnd.transform.position);
        }
    }
}

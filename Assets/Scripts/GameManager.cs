using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float timer = 0f;
    private float regenerateTime = 2f;
    private ResetJump[] resets = new ResetJump[2];
    // Start is called before the first frame update
    void Start()
    {
        resets[0] = GameObject.Find("ResetJump").GetComponent<ResetJump>();
        resets[1] = GameObject.Find("ResetJump(Clone)").GetComponent<ResetJump>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!resets[0].transform.gameObject.activeSelf && !resets[1].transform.gameObject.activeSelf)
        {
            timer += Time.deltaTime;
            if (timer > regenerateTime)
            {
                resets[0].transform.gameObject.SetActive(true);
                resets[1].transform.gameObject.SetActive(true);
                timer = 0f;
            }
        }
    }
}

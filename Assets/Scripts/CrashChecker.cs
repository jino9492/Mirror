using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashChecker : MonoBehaviour
{
    public bool isCrashed;

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.CompareTag("Obstacle"))
        {
            isCrashed = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if (collider.CompareTag("Obstacle"))
        {
            isCrashed = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashChecker : MonoBehaviour
{
    public bool isCrashed;

    void OnTriggerEnter2D(Collider2D collider){
        isCrashed = true;
        print(isCrashed);
    }

    void OnTriggerExit2D(Collider2D collider){
        isCrashed = false;
        print(isCrashed);
    }
}

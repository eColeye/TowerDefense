using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDetection : MonoBehaviour
{    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger collision detected!");
    }
}

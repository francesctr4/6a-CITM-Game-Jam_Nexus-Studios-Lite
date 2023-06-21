using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Up : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position.Set(-12.43f, -16.58f, 0);
        }
    }
}

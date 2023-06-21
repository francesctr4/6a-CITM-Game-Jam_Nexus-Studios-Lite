using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Down : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position.Set(59.47f,23.42f,0);
        }
    }

}

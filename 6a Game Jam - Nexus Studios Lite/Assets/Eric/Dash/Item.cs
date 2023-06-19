using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool unlockedDash = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Item"))
        {
            unlockedDash = true;
            other.GetComponent<SpriteRenderer>().enabled = false;
            other.gameObject.active = false;
            Debug.Log("Item - Collar");
        }
    }
}

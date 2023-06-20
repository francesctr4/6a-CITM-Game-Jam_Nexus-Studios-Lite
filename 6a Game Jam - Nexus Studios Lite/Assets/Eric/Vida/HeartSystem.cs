using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeartSystem : MonoBehaviour
{
    public GameObject[] hearts;
    public int life;

    void Update()
    {
        if(life < 1)
        {
            Destroy(hearts[0].gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if(life < 2)
        {
            Destroy(hearts[1].gameObject);
        }
        else if(life < 3)
        {
            Destroy(hearts[2].gameObject);
        }

    }

    public void TakeDamage(int dmg)
    {
        life -= dmg;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Pincho"))
        {
            TakeDamage(1);
        }
    }
}

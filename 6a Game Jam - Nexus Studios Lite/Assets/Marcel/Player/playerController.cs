using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    private Rigidbody2D rb;
    Vector2 input;
    float shipAngle;

    public float speed;
    public float rotationInterpolation = 0.3f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        rb.velocity = input * speed * Time.fixedDeltaTime;
        GetRoitation();
        if (rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            rb.gravityScale = 0;
        }
        else { rb.gravityScale = 5; }
        
    }

    void GetRoitation()
    {
        Vector2 lookDir = new Vector2(-input.x, input.y);
        shipAngle = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg;
        if(rb.rotation<= -90 && shipAngle >=90)
        {
            rb.rotation += 360;
            rb.rotation = Mathf.Lerp(rb.rotation, shipAngle, rotationInterpolation);
        }
        else if(rb.rotation >= 90 && shipAngle <= -90)
        {
            rb.rotation -= 360;
            rb.rotation = Mathf.Lerp(rb.rotation, shipAngle, rotationInterpolation);
        }
        else
        {
            rb.rotation = Mathf.Lerp(rb.rotation, shipAngle, rotationInterpolation);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] private FieldOfView fieldOfView;

    Vector2 input;
    float shipAngle;

    public float rotationInterpolation = 0.3f;
    
    private float horizontal;
    private float speed = 4f;
    private bool isFacingRight = true;
    public float jump;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    //public string targetTag = "Player";

    //private GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Arreglar movimiento
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
       
        //if (Input.GetButtonDown("Jump"))
        //{
        //    rb.AddForce(new Vector2(rb.velocity.x, jump));
        //}
        //horizontal = Input.GetAxisRaw("Horizontal");

        Vector3 mousePositionScreen = Input.mousePosition;

        // Convertir la posición del mouse a coordenadas del mundo
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);

        // Crear un Vector2 con la posición del mouse en el mundo
        Vector3 dirAim = new Vector3(mousePositionWorld.x, mousePositionWorld.y, mousePositionWorld.z);

        fieldOfView.SetAimDirection(dirAim - gameObject.transform.position);
        fieldOfView.SetOrigin(gameObject.transform.position);

        
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

        //rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
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
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

}


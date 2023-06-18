using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] private FieldOfView fieldOfView;

    const string VolarAnimatorState = "Volar";

    Vector2 input;
    float shipAngle;

    public float rotationInterpolation = 0.3f;
    private float horizontal;
    private float vertical;
    private float speed = 4f;
    private bool isFacingRight = true;
    public float jump;

    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;
    private Vector2 mouseposition2D;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private TrailRenderer tr;

    Batstates currentState;
    private enum Batstates
    {
        Volar,
        Andar,
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDashing)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector3 mousePositionScreen = Input.mousePosition;

        // Convertir la posici�n del mouse a coordenadas del mundo
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);

        // Crear un Vector2 con la posici�n del mouse en el mundo
        mouseposition2D = new Vector2(mousePositionWorld.x, mousePositionWorld.y);
        Vector3 dirAim = new Vector3(mousePositionWorld.x, mousePositionWorld.y, mousePositionWorld.z);

        fieldOfView.SetAimDirection(dirAim - gameObject.transform.position);
        fieldOfView.SetOrigin(gameObject.transform.position);

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        if (horizontal == 0 && vertical == 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else { rb.constraints = RigidbodyConstraints2D.None; }
        Flip();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if(IsGrounded())
        {
           ChangeState(Batstates.Andar);
           Debug.Log("AAAAAAAAAAA");
        }
        if(!IsGrounded())
        {
           ChangeState(Batstates.Volar);
        }
    }
       
    
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if (rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            rb.gravityScale = 0;
        }
        else { rb.gravityScale = 5; }

        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
        
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
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

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = (mouseposition2D - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y)).normalized * dashingPower;
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void ChangeState(Batstates newState)
    {
        if (newState == currentState)
        {
            return;
        }

        currentState = newState;

        switch (newState)
        {
            case Batstates.Andar:
                animator.SetBool(VolarAnimatorState, false);
                break;
            case Batstates.Volar:
                animator.SetBool(VolarAnimatorState, true);
                break;
            
        }        
    }
}


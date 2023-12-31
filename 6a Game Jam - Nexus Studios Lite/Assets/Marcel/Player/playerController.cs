using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerController : MonoBehaviour
{
    private Stamina staminabar;
    private Item itemScript;

    [SerializeField] private FieldOfView fieldOfView;

    const string VolarAnimatorState = "Volar";

    Vector2 input;
    float shipAngle;

    private float horizontal;
    private float vertical;
    private float speed = 4f;
    private bool isFacingRight = true;
    public float jump;
    private bool derecha = false;

    private bool canDash = true;
    public bool isDashing;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;
    private Vector2 mouseposition2D;

    public Rigidbody2D rb;

    [SerializeField] private Transform groundCheckDown;
    [SerializeField] private Transform groundCheckUp;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private SpriteRenderer spriteRenderer;

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
        itemScript = GetComponent<Item>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDashing)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && IsGroundedDown())
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && itemScript.unlockedDash)
        {
            StartCoroutine(Dash());
        }

        if (horizontal == 0 && vertical == 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else
        { 
            rb.constraints = RigidbodyConstraints2D.None; 
        }

        Flip();

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        bool isFlying = !IsGroundedDown() && !IsGroundedUp();

        if (!isFlying)
        {
            ChangeState(Batstates.Andar);
        }
        else 
        {
            ChangeState(Batstates.Volar);
        }
        
        if (IsGroundedUp())
        {
            spriteRenderer.flipY = true;
        }
        else if (!IsGroundedUp())
        {
            spriteRenderer.flipY = false;
          
        }

        if (derecha == false)
        {
            if (IsGroundedRight())
            {
                Vector3 rotation = new Vector3(0, 0, 90);
                transform.Rotate(rotation);
                derecha = true;
            }

        }
        if (derecha == true && isFlying)
        {
            Vector3 rotation = new Vector3(0, 0, -90);
            transform.Rotate(rotation);
            derecha = false;
        }
        

    }
       
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
 
    }

    public bool IsGroundedDown()
    { 
        return IsTouchingWall(groundCheckDown.position);
    }

    public bool IsGroundedUp()
    {
        return IsTouchingWall(groundCheckUp.position);
    }

    public bool IsGroundedRight()
    {
        return IsTouchingWall(groundCheckRight.position);
    }

    private bool IsTouchingWall(Vector3 checkPosition)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkPosition, 0.3f);

        foreach (Collider2D collider in colliders)
        {
            bool isWall = collider.GetComponent<Wall>() != null;

            if (isWall)
            {
                return true;
            }
        }
        

        return false;
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

    public IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        if(horizontal == 0 && vertical == 0)
        {
            if(isFacingRight)
            {
                rb.velocity = new Vector2(1, 0).normalized * dashingPower;
            }
            else
            {
                rb.velocity = new Vector2(-1, 0).normalized * dashingPower;
            }
           
        }
        else
        {
            rb.velocity = new Vector2(horizontal, vertical).normalized * dashingPower;
        }
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


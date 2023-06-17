using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] private FieldOfView fieldOfView;

    private Rigidbody2D rb;
    Vector2 input;
    float shipAngle;

    public float speed;
    public float rotationInterpolation = 0.3f;

    private Animator anim;

    private BatStates currenteState;
    private enum BatStates
    {
        Volar,
        Andar,
        Dejar_Andar
    }

    //public string targetTag = "Player";

    //private GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //targetObject = GameObject.FindGameObjectWithTag(targetTag);
        
        //if (targetObject != null)
        //{
        //    // Se encontró un GameObject con el tag especificado
        //    Debug.Log("GameObject encontrado: " + targetObject.name);
        //}
        //else
        //{
        //    // No se encontró ningún GameObject con el tag especificado
        //    Debug.Log("No se encontró ningún GameObject con el tag: " + targetTag);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

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
    private void ChangeState(BatStates newState)
    {
        if (newState == currenteState) return;

        currenteState = newState;
        switch (newState)
        {
            case BatStates.Volar:
                anim.SetTrigger(name: "Volar");
                break;
            case BatStates.Andar:
                anim.SetTrigger(name: "Andar");
                break;
            case BatStates.Dejar_Andar:
                anim.SetTrigger(name: "Dejar_Andar");
                break;
        }
    }
}

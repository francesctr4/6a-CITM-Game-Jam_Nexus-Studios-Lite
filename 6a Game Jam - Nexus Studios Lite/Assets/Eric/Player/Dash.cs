using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector2 dashDirection;
    public float dashDistance;
    public float dashDuration;
    private bool isDashing = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && !isDashing)
        {
            // Obtener la direcci�n del dash (basado en la entrada de teclado)
            dashDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

            // Iniciar el dash
            StartCoroutine(DashCoroutine());
        }
    }

    IEnumerator DashCoroutine()
    {
        isDashing = true;

        // Desactivar el Rigidbody2D para permitir el movimiento mediante Translate
        body.simulated = false;

        // Calcular el punto final del dash
        Vector2 dashEnd = (Vector2)transform.position + dashDirection * dashDistance;

        // Moverse hacia el punto final durante la duraci�n del dash
        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            // Calcular la interpolaci�n de la posici�n
            float t = (Time.time - startTime) / dashDuration;
            transform.position = Vector2.Lerp(transform.position, dashEnd, t);
            yield return null;
        }

        // Restablecer el estado despu�s del dash
        transform.position = dashEnd;
        body.simulated = true;
        isDashing = false;
    }
}

using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float moveSpeed = 2f;        // Velocidad de movimiento del enemigo
    public Transform leftPoint;         // Punto izquierdo para girar
    public Transform rightPoint;        // Punto derecho para girar

    private bool isMovingRight = true;   // Variable para rastrear la dirección del movimiento

    private void Update()
    {
        // Mueve al enemigo en la dirección adecuada
        if (isMovingRight)
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        else
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        // Comprueba si el enemigo ha alcanzado los puntos de giro
        if (transform.position.x >= rightPoint.position.x)
        {
            Flip();  // Voltea el sprite del enemigo
            isMovingRight = false;
        }
        else if (transform.position.x <= leftPoint.position.x)
        {
            Flip();  // Voltea el sprite del enemigo
            isMovingRight = true;
        }
    }

    private void Flip()
    {
        // Voltea el sprite horizontalmente
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stamina : MonoBehaviour
{
    playerController playerController;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    [Range(0, 4000)]
    public float stamina;
    float maxStamina = 2000;


    [SerializeField] private Rigidbody2D rigid;

    public RectTransform uiBar;
    public float currentStaminaPercent;
    float percentUnit;
    float staminaPercentUnit;

    private void Start()
    {
        playerController = player.GetComponent<playerController>();
        percentUnit = 1f / uiBar.anchorMax.x;
        staminaPercentUnit = 100f / maxStamina;

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (stamina > maxStamina) stamina = maxStamina;

        currentStaminaPercent = stamina * staminaPercentUnit;

        uiBar.anchorMax = new Vector2((currentStaminaPercent * percentUnit) / 400f, uiBar.anchorMax.y);

        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            stamina -= 8;
        }

        if (stamina <= 1420)
        {
            playerController.rb.gravityScale = 30;
            stamina = 1420;
        }
        if (stamina > 1420)
        {
            playerController.rb.gravityScale = 0;
        }

        if (playerController.isDashing)
        {
            stamina -= 15;
        }

        if (playerController.IsGroundedDown() || playerController.IsGroundedUp() || playerController.IsGroundedRight())
        {
            stamina += 25;
        }
    }

}

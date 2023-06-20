using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stamina : MonoBehaviour
{
    playerController playerController;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    [Range(0,4000)]
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
    private void Update()
    {
        if (stamina > maxStamina) stamina = maxStamina;
        else if (stamina < 0) stamina = 0;
        
        currentStaminaPercent = stamina * staminaPercentUnit;

        uiBar.anchorMax = new Vector2((currentStaminaPercent * percentUnit) / 400f, uiBar.anchorMax.y);
        
        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
           stamina -= 15;
        }

        if(stamina <= 0)
        {
            playerController.rb.gravityScale = 30;
        }
        if (stamina > 0)
        {
            playerController.rb.gravityScale = 0;
        }

        if(playerController.isDashing)
        {
            stamina -= 40;
        }

        if (playerController.IsGroundedDown() || playerController.IsGroundedUp() || playerController.IsGroundedRight())
        {
            stamina += 25;
        }
       

    }

    private void OnValidate()
    {
        if (stamina > maxStamina) stamina = maxStamina;
        else if (stamina < 0) stamina = 0;
    }

}


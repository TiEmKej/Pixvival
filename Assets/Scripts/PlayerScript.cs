using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    //Components
    Rigidbody2D playerRB;

    //Variables
    float playerSpeed = 5.0f;
    float horizontalMoveInput;
    float verticalMoveInput;
    bool isFacingRight = true;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        playerRB.velocity = new Vector2(horizontalMoveInput * playerSpeed, verticalMoveInput * playerSpeed);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMoveInput = context.ReadValue<Vector2>().x;
        verticalMoveInput = context.ReadValue<Vector2>().y;
        CharacterFlip();
    }

    public void CharacterFlip()
    {
        if(isFacingRight && horizontalMoveInput < 0f || !isFacingRight && horizontalMoveInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}

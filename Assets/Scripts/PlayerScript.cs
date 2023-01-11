using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    public BoxCollider2D playerCollider;

    private float horSpeed;
    private float verSpeed;
    private float horizontal;
    private float vertical;
    private bool isFacingRight = true;
    private float playerSpeed = 5.0f;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        if (!isFacingRight && horizontal > 0f)
        {
            FlipCharacter();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            FlipCharacter();
        }
    }

    private void FixedUpdate()
    {
        playerRigidbody.velocity = new Vector2(horizontal * playerSpeed, vertical * playerSpeed);
    }

    private void FlipCharacter()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float jumpSpeed = 3;
    [SerializeField] ContactFilter2D groundFilter;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;
    [SerializeField] float dashCooldown;
    bool canDash = true;
    Vector2 moveInput;
    Rigidbody2D rb;
    bool isGrounded = true;
    float originalGravity;
    float lastDirection = 1;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
    }
    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    void OnJump()
    {
        if (isGrounded == true)
        {
            rb.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;
        if (moveInput.x != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(moveInput.x), transform.localScale.y); 
        }
    }
    private IEnumerator Dash()
    {
        if (!canDash) yield break;
        canDash = false;
        float dashDirection = moveInput.x != 0 ? Mathf.Sign(moveInput.x) : lastDirection;
        lastDirection = dashDirection; 

        rb.gravityScale = 0; 
        rb.velocity = new Vector2(dashDirection * dashSpeed, 0);
        yield return new WaitForSeconds(dashDuration); 

        rb.gravityScale = originalGravity; 
        rb.velocity = new Vector2(0, rb.velocity.y); 
        yield return new WaitForSeconds(dashCooldown); 
        canDash = true; 
    }
    private void OnDash()
    {   
        StartCoroutine(Dash());
    }
    // Update is called once per frame
    void Update()
    {
        if (moveInput.x != 0)
        {
            lastDirection = moveInput.x;
        }
        Run();
    }
    private void FixedUpdate()
    {
        isGrounded = rb.IsTouching(groundFilter);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{  
    public ParticleSystem dust; 
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public GameObject swordHitbox;
    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Rigidbody2D rb;
    //Raycast is for looking if space is available before moving 
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    bool canMove =true;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator =GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    // This part of the code is for sliding when 2 keys are pressed together...
    private void FixedUpdate() {
        if(canMove){
            if(movementInput != Vector2.zero){
                    bool success = TryMove(movementInput);
                    if(!success) {
                        success = TryMove(new Vector2(movementInput.x, 0));
                    }
                    if(!success) {
                        success = TryMove(new Vector2(movementInput.y, 0));
                    }
                    if(!success) {
                        success = TryMove(new Vector2(0, movementInput.y));
                    }
                    
                    animator.SetBool("isMoving", success);
                } else {
                    animator.SetBool("isMoving", false);
                }
                if(movementInput.x < 0) {
                    spriteRenderer.flipX = true;
                    gameObject.BroadcastMessage("IsFacingRight",false);
                } else if (movementInput.x > 0) {
                    spriteRenderer.flipX = false;
                    gameObject.BroadcastMessage("IsFacingRight",true);
                }
        }
    }
        //This part of the code is for movement for player 
    private bool TryMove(Vector2 direction) {
        if(direction != Vector2.zero) {
            // Check for potential collisions
            int count = rb.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset
            if(count == 0){
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            } else {
                return false;
            }
        } else {
            // Can't move if there's no direction to move in
            return false;
        }
    }
    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get<Vector2>();
        CreateDust();

    }
    void OnFire(){
        animator.SetTrigger("handAttack");
    }
    public void SwordAttack() {
        LockMovement();
    }
    public void EndSwordAttack() {
        UnlockMovement();
    }
    public void LockMovement(){
        canMove = false;
    }
    public void UnlockMovement(){
        canMove =true;
    }
    void CreateDust(){
        dust.Play();
    }
}

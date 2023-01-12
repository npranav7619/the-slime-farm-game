using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    Animator animator;
    public float moveSpeed;
    private Rigidbody2D myRigidbody;
    public bool isWalking;
    public float walkTime;
    private float walkCounter;
    public float waitTime;
    private float waitCounter;
    private int walkDirection;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
        myRigidbody=GetComponent<Rigidbody2D>();
        waitCounter=waitTime;
        walkCounter=walkTime;
        ChooseDirection();

    }
    // Update is called once per frame
    void Update()
    {
        if (isWalking){
            walkCounter -= Time.deltaTime;
            animator.SetBool("isMoving", true);
            

            switch(walkDirection){
                case 0:
                    myRigidbody.velocity = new Vector2(moveSpeed,0);
                break;
                case 1:
                    myRigidbody.velocity = new Vector2(-moveSpeed,0);
                break;        
            }

            if (walkCounter < 0){
                isWalking= false;
                waitCounter=waitTime;
            }            

        }
        else{
            waitCounter-=Time.deltaTime;
            myRigidbody.velocity = Vector2.zero;
            if(waitCounter<0){
                ChooseDirection();

            }
        }

    }
    public void ChooseDirection(){
        walkDirection=Random.Range(0,4);
        isWalking=true;
        walkCounter=walkTime;


    }    
}

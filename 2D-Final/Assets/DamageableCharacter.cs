using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageableCharacter : MonoBehaviour, IDamageable
{
    public bool disableSimulation = false;
    Animator animator;
    Animator animator2;
    ScoreManager sm;
    PauseMenu pm;
    public GameObject pausego;
    public GameObject healthimage;
    public GameObject player;
    public PlayerController pc;
    Rigidbody2D rb;
    Collider2D physicscollider;
    public bool Alive = true;
    public float Health{
        set{
            if(value < _health){
                animator.SetTrigger("hittrigger");
            }
            _health = value;
            if(_health <= 0){
                animator.SetBool("Alive",false);
                Targetable = false;
            }
        }
        get{
            return _health;
        }
    }
    public bool Targetable { get { return _targetable; }
    set {
        _targetable = value;
        if(disableSimulation){
            rb.simulated = false;
        }
        physicscollider.enabled=value;
    }}
    public float _health =3;
    public bool _targetable = true;
    public void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        healthimage=GameObject.FindGameObjectWithTag("HealthImage");
        pc = player.GetComponent<PlayerController>();
        sm = player.GetComponent<ScoreManager>();
        pausego= GameObject.FindGameObjectWithTag("PauseMenu");
        pm = pausego.GetComponent<PauseMenu>();
        animator = GetComponent<Animator>();
        animator2 = healthimage.GetComponent<Animator>();
        animator.SetBool("Alive",Alive);
        rb = GetComponent<Rigidbody2D>();
        physicscollider = GetComponent<Collider2D>();
    }
    public void OnHit(float damage, Vector2 knockback){
        Health -= damage;
        //Apply force
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }
    public void OnHit(float damage){
        Health -= damage;
    }
    public void OnObjectDestroyed(){
        Destroy(gameObject);
    }
    public void IncreasePlayerSpeed(){
        pc.moveSpeed+=0.05f;
    }
    public void PurplePointAdd(){
        sm.AddPurplePoint();
    }
    public void Pause(){
        pm.Pause();
    }
    public void PlayerHealthReduce(){
        animator2.SetTrigger("htrig");
    }
}
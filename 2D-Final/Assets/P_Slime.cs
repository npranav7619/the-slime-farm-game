using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Slime : MonoBehaviour 
{
    public float damage = 1;
    public float knockbackForce = 100f;
    public float moveSpeed = 50f;
    public GameObject player;
    public DamageableCharacter dc;
    public DetectionZone detectionZone;
    Rigidbody2D rb;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        dc = player.GetComponent<DamageableCharacter>();
    }
    void FixedUpdate(){
        if(detectionZone.detectedObjs.Count > 0){
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;
            rb.AddForce(direction * moveSpeed * Time.deltaTime);
        }
    }
    void OnCollisionEnter2D(Collision2D col){
        Collider2D collider = col.collider;
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if(damageable != null){
            Vector2 direction = (collider.transform.position - transform.position).normalized;
            Vector2 knockback = direction * knockbackForce;
            damageable.OnHit(damage, knockback);
            if(col.collider == player.GetComponent<Collider2D>()){
                dc.PlayerHealthReduce();
            }
        }
    }
}
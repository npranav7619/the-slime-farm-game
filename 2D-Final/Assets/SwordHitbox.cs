using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public float swordDamage = 1f;
    public float knockbackForce = 15f;
    public Collider2D swordCollider;
    public Vector3 faceRight = new Vector3(0.125f,-0.028f,0);
    public Vector3 faceLeft = new Vector3(-0.125f,-0.028f,0);
    void Start(){
        if(swordCollider == null){
            Debug.LogWarning("Sword Collider not set");
        }
    }
    void OnTriggerEnter2D(Collider2D collider){
      IDamageable damageableObject = collider.GetComponent<IDamageable>();
      if (damageableObject != null){
        Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
        Vector2 direction = (collider.gameObject.transform.position - parentPosition).normalized;
        Vector2 knockback = direction * knockbackForce;
       damageableObject.OnHit(swordDamage, knockback);
      }
      else{
        Debug.LogWarning("collider does not implement idamageable");
      }
    }
    void IsFacingRight(bool IsFacingRight){
        if(IsFacingRight){
            gameObject.transform.localPosition = faceRight;
        }
        else{
            gameObject.transform.localPosition = faceLeft;
        }
    }
}

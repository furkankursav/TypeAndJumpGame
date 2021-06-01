using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{

    public GameObject explosiveEffect;
    public LayerMask whatIsDamageable;
    public float damageRadius = 0.3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Patlat();
            
        }
    }

    void Patlat()
    {
        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, damageRadius, whatIsDamageable);
        foreach (Collider2D kutu in nearbyObjects)
        {
            PatlayanKutu patlayanKutu = kutu.gameObject.GetComponent<PatlayanKutu>();
            if(patlayanKutu != null)
            {
                
                patlayanKutu.Patlat();
            }
        }
        Instantiate(explosiveEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatlayanKutu : MonoBehaviour
{
    public GameObject explosionEffect;


    public void Patlat()
    {
        float gecikme = Random.Range(0.01f, 0.05f);
        Invoke("Gerceklestir", gecikme);
    }

    void Gerceklestir()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        AudioManager.instance.Play("Explosion");
        Destroy(this.gameObject);
    }

    
}

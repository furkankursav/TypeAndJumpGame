using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuzak : MonoBehaviour
{
    public GameObject diken;
    
    public float acikKalmaSuresi, kapaliKalmaSuresi;
    private float aktiflestirmeZamani;
    private float kapatmaZamani;
    private bool aktifMi;

    private void Start()
    {
        

        diken.SetActive(aktifMi);
        
    }

    private void Update()
    {
        if(aktifMi && Time.time >= aktiflestirmeZamani + acikKalmaSuresi)
        {
            TurnOffTrap();
        }

        else if(!aktifMi && Time.time >= kapatmaZamani + kapaliKalmaSuresi)
        {
            TurnOnTrap();
            AudioManager.instance.Play("Trap");
        }
    }

    private void TurnOnTrap()
    {
        aktifMi = true;
        aktiflestirmeZamani = Time.time;
        diken.SetActive(true);
        //ses ekle
    }

    private void TurnOffTrap()
    {
        aktifMi = false;
        kapatmaZamani = Time.time;
        diken.SetActive(false);
        //ses ekle
    }
}

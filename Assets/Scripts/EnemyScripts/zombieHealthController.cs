using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zombieHealthController : MonoBehaviour
{

    [SerializeField] int maxHealth = 100;
    public int mevcutCan;
    Animator anim;
    public bool isDead = false;

    private ZombieHitPoint[] hitPoints;

    private void Start()
    {
        anim = GetComponent<Animator>(); //karakterin i�erisindeki animat�re at�yoruz

        hitPoints = GetComponentsInChildren<ZombieHitPoint>(); //Alt objelerdeki hitpointleri bir dizide topluyoruz

        foreach (var hitPoint in hitPoints)
        {
            hitPoint.OnTakeHit += TakeHit; //T�m HitPointlerin OnTakeHit eventine bir Fonksiyon ba�lad�k
        }

        mevcutCan = maxHealth; //MAX canla mevcut can� birbirine e�itledik.



    }

    private void TakeHit(float mermiHasar, float azaltilanHasar) // Event tetiklendinde g�nderilen de�erleri bu parametrelerde ald�k
    {
        float totalDamage = (mermiHasar * azaltilanHasar) / 100f; //Al�nan hasar: MermiHasar� * HitNoktas�Y�zdesi / 100

        mevcutCan -= (int)totalDamage;

        if (mevcutCan <= 0) //E�er mevcut can 0'dan k���k ya da e�it olursa...
        {
            Death();
            //gameObject.SetActive(false);
        }
    }

    void Death()
    {
        isDead = true;
        GetComponent<zombieHareket>().DeathStop();
        
        anim.Play("ZombieDeath"); //anim play ile direkt o animasyona ge�ebiliriz... (LOOP VARSA AN�MASYONLARDA UNUTMA(!))

        foreach (var hitPoint in hitPoints)
        {
            hitPoint.Deactive();
        }
    }
    
   

}

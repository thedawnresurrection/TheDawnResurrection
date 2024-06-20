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
        anim = GetComponent<Animator>(); //karakterin içerisindeki animatöre atýyoruz

        hitPoints = GetComponentsInChildren<ZombieHitPoint>(); //Alt objelerdeki hitpointleri bir dizide topluyoruz

        foreach (var hitPoint in hitPoints)
        {
            hitPoint.OnTakeHit += TakeHit; //Tüm HitPointlerin OnTakeHit eventine bir Fonksiyon baðladýk
        }

        mevcutCan = maxHealth; //MAX canla mevcut caný birbirine eþitledik.



    }

    private void TakeHit(float mermiHasar, float azaltilanHasar) // Event tetiklendinde gönderilen deðerleri bu parametrelerde aldýk
    {
        float totalDamage = (mermiHasar * azaltilanHasar) / 100f; //Alýnan hasar: MermiHasarý * HitNoktasýYüzdesi / 100

        mevcutCan -= (int)totalDamage;

        if (mevcutCan <= 0) //Eðer mevcut can 0'dan küçük ya da eþit olursa...
        {
            Death();
            //gameObject.SetActive(false);
        }
    }

    void Death()
    {
        isDead = true;
        GetComponent<zombieHareket>().DeathStop();
        
        anim.Play("ZombieDeath"); //anim play ile direkt o animasyona geçebiliriz... (LOOP VARSA ANÝMASYONLARDA UNUTMA(!))

        foreach (var hitPoint in hitPoints)
        {
            hitPoint.Deactive();
        }
    }
    
   

}

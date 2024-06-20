using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;


public class Bullet : MonoBehaviour
{

    [SerializeField] public float mermiHizi = 10f;
    [SerializeField] public int mermiDamage = 10;
    public int BulletDamage => mermiDamage; 

    public GameObject kanEfekti;
    bool isTrigger = false;
    Rigidbody2D rb;
    public float BulletSpeed;
    public float Damage;




    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>(); 
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 force = transform.right * BulletSpeed;
        rb.AddForce(force, ForceMode2D.Impulse);
    }


    private void OnEnable()
    {
        Invoke("NoHit", 0.5f); //Invoke fonksiyonu ile bir fonksiyonu delay ekleyerek birkaç saniye sonra çalýþtýrabiliyoruz.
    }

    public void NoHit()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (rb != null) 
        {
            rb.velocity=transform.right*mermiHizi; 
        }
    }

    private void OnCollisionEnter2D (Collision2D other) //OnCollisionEnter2D => Collisionlar'ýn Enter2D (yani çarpýþmaya girdiðinde) | (Collision2D other => bu objenin çarptýðý objenin ismi other olsun)
    {
        if (isTrigger) return;

        ContactPoint2D contact = other.contacts[0]; //Merminin ilk çarptýðý contak objesini burda alýyoruz
        
        Vector2 point = contact.point; //Etkileþimin gerçekleþtiði nokta
        Vector2 normal = contact.normal; //Etkileþimin gerçekleþtiði yüzeyin bize olan açýsý, örneðin obje dikey ve biz düz bir hat yatay ateþ ettiysek dönecek deðer (x,y) = (0,90)

        ZombieHitPoint hitObj = other.gameObject.GetComponent<ZombieHitPoint>();

        

        if (hitObj != null)
        {
            isTrigger = true;

            hitObj.Hitting(this); //Etkileþim sýrasýnda objede yani organda bir ZombieHitPoint scripti var ise içindeki Hitting fonksiyonunu tetikliyoruz ve parametresine hangi bullet çarptýysa onu this ile gönderiyoruz.

            GameObject bloodEffect = Instantiate(kanEfekti, point, Quaternion.Euler(normal)); //Kan efektini spawnlama noktasýný yukarýdaki contact point ve normal deðerlerini atýyoruz

            Destroy(bloodEffect, 1f); //Efekt 1 saniye sonra siliniyor
        }

        gameObject.SetActive(false); //Çarpýþma sonrasý bullet objesini kapatýyoruz ki çarpma sonrasý baþka organa çarpmasýn

    }

    private void OnDisable()
    {
        isTrigger = false; 
    }
}




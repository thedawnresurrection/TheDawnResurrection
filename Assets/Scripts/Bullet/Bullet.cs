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
        Invoke("NoHit", 0.5f); //Invoke fonksiyonu ile bir fonksiyonu delay ekleyerek birka� saniye sonra �al��t�rabiliyoruz.
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

    private void OnCollisionEnter2D (Collision2D other) //OnCollisionEnter2D => Collisionlar'�n Enter2D (yani �arp��maya girdi�inde) | (Collision2D other => bu objenin �arpt��� objenin ismi other olsun)
    {
        if (isTrigger) return;

        ContactPoint2D contact = other.contacts[0]; //Merminin ilk �arpt��� contak objesini burda al�yoruz
        
        Vector2 point = contact.point; //Etkile�imin ger�ekle�ti�i nokta
        Vector2 normal = contact.normal; //Etkile�imin ger�ekle�ti�i y�zeyin bize olan a��s�, �rne�in obje dikey ve biz d�z bir hat yatay ate� ettiysek d�necek de�er (x,y) = (0,90)

        ZombieHitPoint hitObj = other.gameObject.GetComponent<ZombieHitPoint>();

        

        if (hitObj != null)
        {
            isTrigger = true;

            hitObj.Hitting(this); //Etkile�im s�ras�nda objede yani organda bir ZombieHitPoint scripti var ise i�indeki Hitting fonksiyonunu tetikliyoruz ve parametresine hangi bullet �arpt�ysa onu this ile g�nderiyoruz.

            GameObject bloodEffect = Instantiate(kanEfekti, point, Quaternion.Euler(normal)); //Kan efektini spawnlama noktas�n� yukar�daki contact point ve normal de�erlerini at�yoruz

            Destroy(bloodEffect, 1f); //Efekt 1 saniye sonra siliniyor
        }

        gameObject.SetActive(false); //�arp��ma sonras� bullet objesini kapat�yoruz ki �arpma sonras� ba�ka organa �arpmas�n

    }

    private void OnDisable()
    {
        isTrigger = false; 
    }
}




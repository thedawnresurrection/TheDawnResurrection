using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class zombieHareket : MonoBehaviour
{

    public float speed;
    Rigidbody2D rb;
    Animator anim;

    public bool isStatic;
    public bool isWalker;
    public bool isWalkingRight;

    public Transform wallCheck;
    public bool wallDetected;
    public float detectionRaidus;
    public LayerMask whatIsGround;

    

    public GameObject projectilePrefab;
    public Transform attackPoint;
    [SerializeField] public float attackSpeed = 2f;
    [SerializeField] private float nextAttackTime = 0f;


    [SerializeField] public int zombieDamageLow =10;
    [SerializeField] public int zombieDamageMedium = 20;
    [SerializeField] public int zombieDamageHigh = 25;

    public wall Wall;

    public Transform collidingObject;

    public enum State
    {

        WalkingToWall, AttackingToWall, WalkingToPlayer, AttackingToPlayer
    }

    public State state = State.WalkingToWall;


    private Transform target;
    

    public static zombieHareket instance;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


        SpriteRenderer[] childRenderers = GetComponentsInChildren<SpriteRenderer>(); //buradaki kod ile bilikte...

        foreach (SpriteRenderer renderer in childRenderers)
        {
            renderer.sortingOrder = (int)(transform.position.y * -10); //... her yukarýda olan zombie'nin layer'ý daha aþaðýda olsun ve önde olanlarla iç içe geçmemesi saðlandý
        }
        

        
    }

    
    void Update()
    {
        wallDetected = Physics2D.OverlapCircle(wallCheck.position, detectionRaidus, whatIsGround); //zombie'nin önümüzdeki duvarý farketmesini saðlayacak kod

        


        if (wallDetected) //duvarla karþý karþýya geldiðinde zombie ne yapsýn?
        {          
            //Attack();
           
        }
        else
        {
            anim.SetBool("hareketEtsinMi", true);  
        }

        

        if (Wall != null && Wall.wallMevcutCan <= 0 && state != State.AttackingToPlayer) //wall scriptindeki wallMevcutCan'ý 0'dan küçük ya da eþit ise...
        {
            DestroyWall();

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "TopWallCollider" || collision.name == "MidWallCollider" || collision.name == "BotWallCollider")
        {
            Wall = collision.GetComponentInParent<wall>();
        }
    }


    private void FixedUpdate()
    {
        if (Wall == null) return;
        
        
        if (isStatic) 
        {
            anim.SetBool("hareketEtsinMi", true);
            rb.velocity = Vector3.zero;
            
        }
        else if (isWalker)
        {
            anim.SetBool("hareketEtsinMi", true);

            if (Wall.wallMevcutCan <= 0) return;


            if(!isWalkingRight)
            {
                rb.velocity = new Vector2 (-speed*Time.deltaTime, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
            }

        }


    }

    public void Attack() //zombinin atak fonksiyonu
    {
        isStatic = true;
        anim.SetBool("Attack", true);
        

        if (collidingObject.tag == "wall")   //ZOMBÝE'NÝN VERECEÐÝ HASAR
        {

          Wall.wallHasar(zombieDamageLow);

          state = State.AttackingToWall;

        } 
        
        if (collidingObject.tag == "Player")
        {
            collidingObject.GetComponent<PlayerHareketController>().playerTakeDamage(zombieDamageLow);
            state = State.AttackingToPlayer;

        }

    }

    private void OnCollisionEnter2D(Collision2D collision) //2d objelerin birbirine çarpmasýný algýladýk
    {
        
        if(collision.transform.CompareTag("wall") || collision.transform.CompareTag("Player"))  //çarpan objenin tag'ini tespit ettik
        {
            collidingObject = collision.transform;
            InvokeRepeating(nameof(Attack), 0.1f, 0.4f); //ilki kaç saniye sonra baþlayacak, ikincisi kaç saniyede bir gerçekleþecek

            Attack();

        }

    }

   

    private void OnDisable() //duvarýn canýnýn bitip bitmediðini check etmeyi býrak
    {
        Wall = null;
        
        //wall.DestroyWallEvent -= DestroyWall;
    }

    private void DestroyWall() //peki ya duvar yok edilince ne yapýlsýn?
    {
        
        Wall.DestroyWall();
        Wall.GetComponent<BoxCollider2D>().enabled = false;
        Wall.GetComponent<SpriteRenderer>().enabled = false;


        anim.SetBool("Attack", false); //attack animasyonunu kes
        anim.SetBool("hareketEtsinMi", true); //hareketEtsinMi'yi true yap ve yürümeye devam et

        rb.velocity = new Vector2(0, 0);
        if (!GetComponent<zombieHealthController>().isDead) //düþmanýn pozisyonuyla hedef arasýndaki deðer durmaMesafesinden büyükse...
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed/48* Time.deltaTime); //...düþmanýn belli mesafede takip etmesini saðlar | bölü 32 yaptým çünkü hýzlarý bir anda *32 oluyor<<<<
        
        state = State.WalkingToPlayer;

        
    }




    public void DeathStop ()
    {
        isStatic = true;

        anim.SetBool("hareketEtsinMi", false);

        SpriteRenderer[] childRenderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer renderer in childRenderers)
        {
            renderer.sortingOrder = (int)(renderer.sortingOrder -1000); //ölen zombie -1000. layera geçsin ve hayattaki zombieler içe çakýþmasýn
        }
    }

    //Time.deltaTime nedir?: Hangi bilgisayarda hangi FPS'te olursa olsun hýzlarýn sabit olarak ayný þekilde olmasýný saðlar. Time'dan farký budur deltaTime'ýn.
}

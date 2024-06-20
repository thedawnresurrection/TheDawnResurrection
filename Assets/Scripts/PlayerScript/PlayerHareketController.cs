using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHareketController : MonoBehaviour
{
    [SerializeField] private float hareketHizi = 10f;

    Vector2 hareketVectoru;

    Rigidbody2D rb;

    Animator anim; 

    [SerializeField] Transform handTransform;

    //CAN KISIMLARI

    [SerializeField] int maxHealth = 1;
    public int mevcutCan;
    public bool isDead = false;
    public bool isStatic;

    public static PlayerHareketController instance;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>(); 
    }

    private void Start()
    {
        mevcutCan = maxHealth;
        
    }

    private void Update()
    {
        HareketFNC();
        SilahiDondurFNC();

        if (mevcutCan <= 0)
        {
            playerDeath();
        }
    }

    void HareketFNC()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        hareketVectoru = new Vector2(h, v);
        hareketVectoru.Normalize();
        
        

        rb.velocity = hareketVectoru * hareketHizi;
        

        if(rb.velocity!= Vector2.zero) 
        {
            anim.SetBool("hareketEtsinmi", true); 
        }
        else
        {
            anim.SetBool("hareketEtsinmi", false); 
        }
    }

    void SilahiDondurFNC() 
    {
        Vector3 mousePos =Input.mousePosition; 
        Vector3 playerPoint = Camera.main.WorldToScreenPoint(transform.position); 
        
        Vector2 hareketYonu=new Vector2(mousePos.x-playerPoint.x,mousePos.y-playerPoint.y); 

        float angle = Mathf.Atan2(hareketYonu.y, hareketYonu.x) * Mathf.Rad2Deg; 
        handTransform.rotation=Quaternion.Euler(0,0,angle);

        if (mousePos.x < playerPoint.x) 
        {
            transform.localScale = new Vector3(-1, 1, 1); 
            handTransform.localScale = new Vector3(-1, -1, 1); 

        }
        else
        {
            transform.localScale = Vector3.one; 
            handTransform.localScale = Vector3.one; 

        }
        
    }

    public void playerTakeDamage (int zombieDamageLow)
    {
        if (mevcutCan <= 0)
        {
            playerDeath();
            return;
        }
            
        mevcutCan -= zombieDamageLow;

    }

    void playerDeath ()
    {
        isDead = true;
        playerDeathStop();
        anim.Play("playerDeath");
        this.enabled = false;
        GetComponent<GunController>().enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        rb.freezeRotation = true;
    }

    void playerDeathStop ()
    {
        isStatic = true;
        anim.SetBool("hareketEtsinmi", false);
    }

}

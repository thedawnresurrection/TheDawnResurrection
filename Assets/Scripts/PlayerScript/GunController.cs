using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class GunController : MonoBehaviour
{

    public GunSO Gun;

    private Bullet mermiPrefab;
    [SerializeField] private UnityEngine.Transform mermiCikisNoktasi;

    [Range(0.01f,2f)] 
    [SerializeField] private float mermiAtisSuresi = .25f; 
    private float mermiAtisSayac;

    public float cooldown = 5f;
    public int shootCount = 0;
    private bool isCooldown = false;


    private void Start()
    {
        cooldown = Gun.cooldown;
        mermiAtisSuresi = Gun.mermiAtisSuresi;
    }



    private void Update()
    {
        Shoot();
        Cooldown();
    }



    public void Shoot () //ATEÞ ETME ÝÞLEMLERÝ
    {
        if (GetComponent<AmmoController>().currentAmmo > 0 && Input.GetMouseButton(0) && Time.time > mermiAtisSayac && !isCooldown)
        {

            mermiPrefab = ObjectPool.instance.MermiCikarFNC(mermiCikisNoktasi.position, mermiCikisNoktasi.rotation);
            mermiPrefab.mermiHizi = Gun.mermiHizi;
            mermiPrefab.mermiDamage = Gun.mermiDamage;

            mermiAtisSayac = Time.time + mermiAtisSuresi;

            GetComponent<AmmoController>().MermiHarca(); //farklý scriptten iþlem böyle yapýlýr. Ýlk script ismi sonrasýndna fonksiyon
            shootCount++;
            if (shootCount >= Gun.shootCount)
            {
                isCooldown = true;
                shootCount = 0;
            }

        }

      }

    

    void Cooldown()
    {
        if (isCooldown)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0f)
            {
                cooldown = Gun.cooldown;
                isCooldown = false;
            }
        }

    }




}

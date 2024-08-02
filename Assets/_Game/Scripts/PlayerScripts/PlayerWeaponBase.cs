using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponBase : MonoBehaviour
{
    public Transform body;
    private Camera cam;

    public SOWeaponData currentWeaponData;
    public BaseBullet baseBullet;
    public Transform bulletSpawnTransform;
    public GameObject muzzleFlash;
    private float fireTimer;

    private Animator playerAnimator;
    private bool fire = true;

    private void OnEnable()
    {
        playerAnimator = GetComponentInParent<Animator>();
        if (currentWeaponData.weaponType == WeaponType.Pistol)
        {
            playerAnimator.SetBool("HasPistol", true);
        }
        if (currentWeaponData.weaponType == WeaponType.Rifle)
        {
            playerAnimator.SetBool("HasPistol", false);
        }
        
    }

    private void Start()
    {
        GameEvents.AmmoResourceNoMoreEvent.AddListener(AmmoResourceNoMore);
    }
    private void OnDestroy()
    {
        GameEvents.AmmoResourceNoMoreEvent.RemoveListener(AmmoResourceNoMore);
    }

    private void AmmoResourceNoMore()
    {
        fire = false;
    }

    private void Awake()
    {
        cam = Camera.main;
        
        fireTimer = currentWeaponData.fireRate;
    }
    public void Update()
    {
        
        var dir  = Input.mousePosition - cam.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var rot = Quaternion.AngleAxis(angle, Vector3.forward);
        body.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        var bodyEuler = body.eulerAngles;
        bodyEuler.z = Mathf.Clamp(bodyEuler.z, 100, 250);
        body.eulerAngles = bodyEuler;


        if (fire)
        {
            fireTimer += Time.deltaTime;
            if (Input.GetMouseButton(0) && fireTimer >= currentWeaponData.fireRate)
            {
                fireTimer = 0;
                Fire();
            }
        }
        
    }

    private void Fire()
    {
        GameEvents.AmmoResourceUsedEvent?.Invoke(currentWeaponData.resourceAmount);

        var bullet = Instantiate(baseBullet, bulletSpawnTransform.position, Quaternion.identity);
        bullet.Initialize(body.transform.right,currentWeaponData.bulletSpeed,currentWeaponData.damage);
        muzzleFlash.SetActive(true);
        currentWeaponData.fireClip.PlayClip2D(this, 1, UnityEngine.Random.Range(0.92f, 1.05f));
        DOVirtual.DelayedCall(0.07f, delegate
        {
            muzzleFlash.SetActive(false);
        });
    }
}

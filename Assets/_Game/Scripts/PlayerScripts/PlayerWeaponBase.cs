using DG.Tweening;
using UnityEngine;

public class PlayerWeaponBase : MonoBehaviour
{
    public Transform rotateTransform;
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

        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Açıyı 0-360 arasında normalize et
        float normalizedAngle = (angle + 360) % 360;
        // Clamp işlemini tek bir seferde yap
        float clampedAngle = Mathf.Clamp(normalizedAngle, currentWeaponData.minRotAngle, currentWeaponData.maxRotAngle);

        rotateTransform.rotation = Quaternion.Euler(0, 0, clampedAngle);
        Debug.Log(normalizedAngle);



        if (fire)
        {
            fireTimer += Time.deltaTime;
            if (Input.GetMouseButton(0) && fireTimer >= currentWeaponData.fireRate)
            {
                fireTimer = 0;

                Vector3 bulletdir = (mousePosition - bulletSpawnTransform.position).normalized;
                if (normalizedAngle > currentWeaponData.minFireAngle && normalizedAngle < currentWeaponData.maxFireAngle)
                {
                    Fire(bulletdir);
                }

            }
        }

    }

    private void Fire(Vector3 dir)
    {
        GameEvents.AmmoResourceUsedEvent?.Invoke(currentWeaponData.resourceAmount);

        var bullet = Instantiate(baseBullet, bulletSpawnTransform.position, Quaternion.identity);
        bullet.Initialize(dir, currentWeaponData.bulletSpeed, currentWeaponData.damage);
        muzzleFlash.SetActive(true);
        currentWeaponData.fireClip.PlayClip2D(this, 1, UnityEngine.Random.Range(0.92f, 1.05f));
        DOVirtual.DelayedCall(0.07f, delegate
        {
            muzzleFlash.SetActive(false);
        });
    }
}

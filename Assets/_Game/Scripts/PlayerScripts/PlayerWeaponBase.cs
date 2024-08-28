using DG.Tweening;
using UnityEngine;
using UnityEngine.U2D.IK;

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
    public LimbSolver2D limbLeftArm, limbRightArm;

    private float magazineBulletAmount;
    private bool isReloading;

    private void OnEnable()
    {
        playerAnimator = GetComponentInParent<Animator>();
        if (currentWeaponData.weaponType == WeaponType.Pistol)
        {
            playerAnimator.SetBool("HasPistol", true);
        }
        if (currentWeaponData.weaponType == WeaponType.Machine)
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
        DOTween.KillAll();
        GameEvents.AmmoResourceNoMoreEvent.RemoveListener(AmmoResourceNoMore);
    }

    private void AmmoResourceNoMore()
    {
        fire = false;
    }
    private void OnGUI()
    {
        GUILayout.Label("MagazineBulletAmount : " + magazineBulletAmount);
    }
    private void Awake()
    {
        cam = Camera.main;

        fireTimer = currentWeaponData.fireRate;
        magazineBulletAmount = currentWeaponData.magazineBulletAmount;
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




        if (fire)
        {
            fireTimer += Time.deltaTime;
            if (Input.GetMouseButton(0) && fireTimer >= currentWeaponData.fireRate)
            {
                fireTimer = 0;

                Vector3 bulletdir = (mousePosition - bulletSpawnTransform.position).normalized;
                if (normalizedAngle > currentWeaponData.minFireAngle && normalizedAngle < currentWeaponData.maxFireAngle)
                {
                    if (magazineBulletAmount > 0)
                    {
                        Fire(bulletdir);
                    }
                    if (magazineBulletAmount <= 0 && !isReloading)
                    {
                        Reload();
                    }
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && magazineBulletAmount
        < currentWeaponData.magazineBulletAmount)
        {
            Reload();
        }

    }
    private void Reload()
    {
        GameEvents.PlayerMagazineReloadEvent?.Invoke();
        isReloading = true;
        Tween reloadTween = DOVirtual.Float(1, 0, 0.2f, delegate (float newValue)
        {
            limbRightArm.weight = newValue;
        });
        reloadTween.OnComplete(delegate
        {
            playerAnimator.SetTrigger("Reload");
            DOVirtual.DelayedCall(0.5f, delegate
            {
                Tween resetTween = DOVirtual.Float(0, 1, 0.4f, delegate (float newValue)
                {
                    limbRightArm.weight = newValue;
                });
                resetTween.OnComplete(delegate
                {
                    isReloading = false;
                    magazineBulletAmount = currentWeaponData.magazineBulletAmount;
                });
            });


        });
    }

    private void Fire(Vector3 dir)
    {
        GameEvents.AmmoResourceUsedEvent?.Invoke(currentWeaponData.resourceAmount);
        magazineBulletAmount--;

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

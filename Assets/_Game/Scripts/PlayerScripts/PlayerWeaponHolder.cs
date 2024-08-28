using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerWeaponHolder : MonoBehaviour
{
    private Animator playerAnimator;
    public List<PlayerWeaponBase> weapons;
    private Coroutine weaponSwitchCoroutine;
    private void Awake()
    {
        var childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            weapons.Add(transform.GetChild(i).GetComponent<PlayerWeaponBase>());
        }
        playerAnimator = GetComponentInParent<Animator>();
    }
    private void Start()
    {
        OpenWeapon(0);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OpenWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OpenWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            OpenWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            OpenWeapon(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            OpenWeapon(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            OpenWeapon(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            OpenWeapon(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            OpenWeapon(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            OpenWeapon(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            OpenWeapon(9);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            OpenWeapon(10);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            OpenWeapon(11);
        }


    }
    public void OpenWeapon(int index)
    {
        if (weapons[index].gameObject.activeSelf)
        { // elimizdeki silahi tekrar acmaya calisiyorsak
            return;
        }
        if (weaponSwitchCoroutine != null) return; // suan silah degistriyorsak


        GameEvents.PlayerWeaponChangeStartEvent?.Invoke();

        foreach (PlayerWeaponBase weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }

        PlayerWeaponBase weaponBase = weapons[index];
        playerAnimator.SetInteger("WeaponType", (int)weaponBase.currentWeaponData.weaponType);
        playerAnimator.SetTrigger("Switch");





        weaponSwitchCoroutine = StartCoroutine(SwitchWeapon());
        IEnumerator SwitchWeapon()
        {
            yield return new WaitForSeconds(0.5f);
            DOVirtual.Float(0, 1, 0.1f, delegate (float value)
            {
                weaponBase.limbLeftArm.weight = value;
                weaponBase.limbRightArm.weight = value;
            });
            GameEvents.PlayerWeaponChangeEndEvent?.Invoke();
            weapons[index].gameObject.SetActive(true);
            weaponSwitchCoroutine = null;
        }
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
    }

}

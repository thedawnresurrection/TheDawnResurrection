using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHolder : MonoBehaviour
{
    public List<PlayerWeaponBase> weapons;
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

    }
    public void OpenWeapon(int index)
    {
        foreach (PlayerWeaponBase weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }
        weapons[index].gameObject.SetActive(true);
    }
}

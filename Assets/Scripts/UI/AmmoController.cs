using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoController : MonoBehaviour
{


    [SerializeField] public int maxAmmo=100;
    public int currentAmmo;



    private void Start()
    {

        currentAmmo=maxAmmo;


    }

    

    public void MermiHarca()
    {
        currentAmmo--;
    }

    //AI için de bu void kullanýlabilir ayný mermiyi çekmesi için | AI ateþ etme voidi -> mermiHarca() |

}

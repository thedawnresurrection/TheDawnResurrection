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

    //AI i�in de bu void kullan�labilir ayn� mermiyi �ekmesi i�in | AI ate� etme voidi -> mermiHarca() |

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    [SerializeField] int wallHealth = 100;
    public int wallMevcutCan;
    private Rigidbody2D rb;

    public delegate void WallDestroyedAction();
    public static event WallDestroyedAction DestroyWallEvent;


    public static wall instance; //istedi�im her yerden, scriptten eri�mek i�in bu fonksiyonu kulland�k yani STATIC.

    

    


    private void Awake() //awake voidi = �lk uyan��ta direk sahneyi ba�latt���nda | start voidi = ilk karede | update voidi = her karede | fixedUpdate = fixlenmi� karede
    {
        instance = this; //instance = this  dedik yani instance bizim bu scriptimiz olsun.
    }

    private void Start()
    {
        wallMevcutCan = wallHealth;
    }

    public void wallHasar (int zombieDamageLow)
    {
        if (wallMevcutCan <= 0)
        {
            DestroyWall();
            //return;
        }

        wallMevcutCan -= zombieDamageLow;

        }
    public void DestroyWall()
    {
        if (DestroyWallEvent != null)              
        {
            DestroyWallEvent.Invoke(); //tetikle
            Destroy(gameObject);
        }
    }

   







}

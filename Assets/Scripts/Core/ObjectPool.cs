using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool instance; 


    [SerializeField] private Transform mermiHolder; 
    [SerializeField] private Bullet bullet; 

    [SerializeField]
    int bulletAdet = 40;

    private Queue<Bullet> bulletQueue = new Queue<Bullet>(); //Queue liste gibi i�erisinde elemanlar bar�nd�r�yor. Listeden tek fark�, Dequeue fonksiyonu ile bir s�ra varm�� gibi i�erisinden tektek elemanlar� �a��rabiliyoruz.
                                                             //Queue de son eklenen eleman her zaman son �a��r�l�r.

    private void Awake()
    {
        if(instance == null) 
        {
            instance = this; 
        }


    }

    private void Start()
    {
        for(int i = 0; i < bulletAdet; i++) 
        {
            Bullet mermi = Instantiate(bullet);
            mermi.gameObject.SetActive(false);
            mermi.gameObject.transform.SetParent(mermiHolder);

            bulletQueue.Enqueue(mermi);
        }
    }

    public Bullet MermiCikarFNC(Vector2 position, Quaternion rotation) 
    {
        Bullet bullet = bulletQueue.Dequeue(); //S�radan bir eleman �ektik.

        bullet.transform.position = position; 
        bullet.transform.rotation = rotation;
        bullet.gameObject.SetActive(true);      //Bu �ekilen objenin pozisyonlar�n� ve rotasyonunu parametre ile al�p aktif yapt�k

        bulletQueue.Enqueue(bullet);            //Daha sonra bu objeyi Enqueue ile tekrar s�raya soktuk, obje ilk s�radayken son s�rada �a��r�lmay� bekliyor.

        return bullet;
    }



}

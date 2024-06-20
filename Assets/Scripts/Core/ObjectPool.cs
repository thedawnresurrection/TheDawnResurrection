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

    private Queue<Bullet> bulletQueue = new Queue<Bullet>(); //Queue liste gibi içerisinde elemanlar barýndýrýyor. Listeden tek farký, Dequeue fonksiyonu ile bir sýra varmýþ gibi içerisinden tektek elemanlarý çaðýrabiliyoruz.
                                                             //Queue de son eklenen eleman her zaman son çaðýrýlýr.

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
        Bullet bullet = bulletQueue.Dequeue(); //Sýradan bir eleman çektik.

        bullet.transform.position = position; 
        bullet.transform.rotation = rotation;
        bullet.gameObject.SetActive(true);      //Bu çekilen objenin pozisyonlarýný ve rotasyonunu parametre ile alýp aktif yaptýk

        bulletQueue.Enqueue(bullet);            //Daha sonra bu objeyi Enqueue ile tekrar sýraya soktuk, obje ilk sýradayken son sýrada çaðýrýlmayý bekliyor.

        return bullet;
    }



}

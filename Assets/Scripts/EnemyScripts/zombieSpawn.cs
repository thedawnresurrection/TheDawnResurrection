using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieSpawn : MonoBehaviour
{

    [SerializeField] private GameObject Zombie1;
    [SerializeField] private GameObject spawnPoint;
    public Transform[] spawners;
    
    

    void Start()
    {
        
        InvokeRepeating("spawnZombie", 1f, 2f); //bir metodun sürekli tekrarlanmasýna yarar | Kaç saniyede bir baþlayacaðý, her kaç saniyede tekrarlanacaðý
    }

    
    //6 TANE HAT ÜZERÝNDEN RANDOM LOCATION OLUÞMASINI SAÐLAYALIM


    void spawnZombie()
    {
        Transform secilenSpawner = spawners[Random.Range(0, spawners.Length)];


        Instantiate(Zombie1, secilenSpawner.position, Quaternion.identity); //zombie1 yaratýlacak | Instantiate oluþturmayý saðlar





    }


}

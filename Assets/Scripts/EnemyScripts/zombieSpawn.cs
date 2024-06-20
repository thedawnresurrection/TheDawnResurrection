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
        
        InvokeRepeating("spawnZombie", 1f, 2f); //bir metodun s�rekli tekrarlanmas�na yarar | Ka� saniyede bir ba�layaca��, her ka� saniyede tekrarlanaca��
    }

    
    //6 TANE HAT �ZER�NDEN RANDOM LOCATION OLU�MASINI SA�LAYALIM


    void spawnZombie()
    {
        Transform secilenSpawner = spawners[Random.Range(0, spawners.Length)];


        Instantiate(Zombie1, secilenSpawner.position, Quaternion.identity); //zombie1 yarat�lacak | Instantiate olu�turmay� sa�lar





    }


}

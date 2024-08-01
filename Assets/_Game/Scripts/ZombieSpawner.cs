using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public List<Transform> spawnTransforms;
    public void Start()
    {
        GameEvents.ZombieDieEvent.AddListener(ZombieDie);

        SpawnZombie(4);
    }

    public void SpawnZombie(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            var rTransform = GetRandomTransform();
            var zombie = Instantiate(zombiePrefab, rTransform.position, Quaternion.identity);
        }
    }
    private void OnDestroy()
    {
        GameEvents.ZombieDieEvent.RemoveListener(ZombieDie);
    }

    private Transform GetRandomTransform()
    {
        int i = Random.Range(0, spawnTransforms.Count);
        return spawnTransforms[i]; 
    }
    private void ZombieDie()
    {
        SpawnZombie(1);
    }

}

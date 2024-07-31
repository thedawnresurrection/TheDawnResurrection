using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public List<Transform> spawnTransforms;
    public void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            var rTransform = GetRandomTransform();
            var zombie = Instantiate(zombiePrefab, rTransform.position, Quaternion.identity);
        }
    }
    private Transform GetRandomTransform()
    {
        int i = Random.Range(0, spawnTransforms.Count);
        return spawnTransforms[i]; 
    }
}

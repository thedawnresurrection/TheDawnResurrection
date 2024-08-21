using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public List<Transform> spawnTransforms;
    public float spawnDuration = 3;
    public void Start()
    {
        GameEvents.ZombieDieEvent.AddListener(ZombieDie);

        StartCoroutine(SpawnZombie(4));
    }

    private void OnDestroy()
    {
        GameEvents.ZombieDieEvent.RemoveListener(ZombieDie);
    }
    public IEnumerator SpawnZombie(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            var rTransform = GetRandomTransform();
            var zombie = Instantiate(zombiePrefab, rTransform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDuration);
        }
    }

    private Transform GetRandomTransform()
    {
        int i = Random.Range(0, spawnTransforms.Count);
        return spawnTransforms[i];
    }
    private void ZombieDie()
    {
        StartCoroutine(SpawnZombie(1));
    }

}

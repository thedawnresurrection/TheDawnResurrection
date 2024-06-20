using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcAtesEtme : MonoBehaviour
{
    public GameObject NPCmermiPrefab;
    public Transform NPCMermiCikisNoktasi;
    public float fireSpeed = 3f;
    public float maxY = 5f;
    public float minY = -9f;
    public float maxX = 21f;
    public float minX = 20f;
    

    [SerializeField] private float fireCooldown;

    private void Start()
    {
        fireCooldown = Time.time + Random.Range(0f, fireSpeed);
    }

    private void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (Time.time >= fireCooldown)
        {
            fireCooldown = Time.time + 1f / fireSpeed;

            Vector2 randomFirePosition = new Vector2 (Random.Range(minX, maxX), Random.Range(minY, maxY));

            Instantiate(NPCmermiPrefab, NPCMermiCikisNoktasi.position, NPCMermiCikisNoktasi.rotation);

            NPCMermiCikisNoktasi.position = new Vector2(NPCMermiCikisNoktasi.position.x - 1f, NPCMermiCikisNoktasi.position.y);
        }
    }










}

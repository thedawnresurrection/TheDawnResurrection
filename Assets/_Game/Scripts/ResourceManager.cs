using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private float ammoResource;
    public float AmmoResource => ammoResource;
    public float maxAmmoResource = 100;
    private void Awake()
    {
        ammoResource = maxAmmoResource;
    }
    private void Start()
    {
        GameEvents.AmmoResourceUsedEvent.AddListener(BulletResourceUsed);
    }

    private void OnDestroy()
    {
        GameEvents.AmmoResourceUsedEvent.RemoveListener(BulletResourceUsed);

    }
    private void BulletResourceUsed(float amount)
    {
        ammoResource -= amount;
        if (ammoResource <= 0)
        {
            ammoResource = 0;
            GameEvents.AmmoResourceNoMoreEvent?.Invoke();
        }
        GameEvents.AmmoResourceAmountUpdateEvent?.Invoke(maxAmmoResource, ammoResource);
    }

}

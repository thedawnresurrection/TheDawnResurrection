using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameEvents
{
    public static UnityEvent ZombieDieEvent = new UnityEvent();

    public static UnityEvent<float> AmmoResourceUsedEvent = new UnityEvent<float>();
    public static UnityEvent AmmoResourceNoMoreEvent = new UnityEvent();
    public static UnityEvent<float, float> AmmoResourceAmountUpdate = new UnityEvent<float, float>();

    public static UnityEvent BarricadeTakeDamageEvent = new UnityEvent();

    public static UnityEvent ExpolisionGrenadeBomb = new UnityEvent();
    public static UnityEvent ExpolisionMolotovBomb = new UnityEvent();
    public static UnityEvent<float, float> ExpolisionFlashBomb = new UnityEvent<float, float>();

}

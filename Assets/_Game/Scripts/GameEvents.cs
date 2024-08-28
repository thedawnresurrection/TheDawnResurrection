using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameEvents
{
    public static UnityEvent ZombieDieEvent = new UnityEvent();
    public static UnityEvent<BaseZombie, float, int> ZombieBearTrapEvent = new UnityEvent<BaseZombie, float, int>();
    public static UnityEvent<BaseZombie, float, int> ZombieToxicSlimeEnterEvent = new UnityEvent<BaseZombie, float, int>();
    public static UnityEvent<BaseZombie> ZombieToxicSlimeExitEvent = new UnityEvent<BaseZombie>();
    public static UnityEvent ZombieTriggeredBombEvent = new UnityEvent();



    public static UnityEvent<float> AmmoResourceUsedEvent = new UnityEvent<float>();
    public static UnityEvent AmmoResourceNoMoreEvent = new UnityEvent();
    public static UnityEvent<float, float> AmmoResourceAmountUpdateEvent = new UnityEvent<float, float>();

    public static UnityEvent BarricadeTakeDamageEvent = new UnityEvent();

    public static UnityEvent ExpolisionGrenadeBombEvent = new UnityEvent();
    public static UnityEvent ExpolisionMolotovBombEvent = new UnityEvent();
    public static UnityEvent<float, float> ExpolisionFlashBombEvent = new UnityEvent<float, float>();


    public static UnityEvent PlayerMagazineReloadStartEvent = new UnityEvent();
    public static UnityEvent PlayerMagazineReloadEndEvent = new UnityEvent();
    public static UnityEvent PlayerWeaponChangeStartEvent = new UnityEvent();
    public static UnityEvent PlayerWeaponChangeEndEvent = new UnityEvent();


}

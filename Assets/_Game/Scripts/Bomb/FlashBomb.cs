using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBomb : BaseBomb
{
    public float flashScreenDuration = 1f;
    public float freezeTime;
    public override void Explosion()
    {
        base.Explosion();
        GameEvents.ExpolisionFlashBomb?.Invoke(flashScreenDuration, freezeTime);
    }
}

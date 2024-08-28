using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float destroyTime;
    private bool isPlaying;
    public SpriteRenderer sp;
    void Start()
    {
        if (sp == null)
        {
            sp = GetComponent<SpriteRenderer>();
        }
        if (sp && !isPlaying)
        {
            isPlaying = true;
            DOVirtual.DelayedCall(destroyTime, delegate
            {
                sp.DOColor(Color.clear, destroyTime / 2).OnComplete(delegate
                {
                    Destroy(gameObject);
                });
            });
        }
    }

}

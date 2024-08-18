using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float destroyTime;
    private bool isPlaying;
    private void OnEnable()
    {

    }
    void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer && !isPlaying)
        {
            isPlaying = true;
            DOVirtual.DelayedCall(destroyTime, delegate
            {
                spriteRenderer.DOColor(Color.clear, destroyTime / 2).OnComplete(delegate
                {
                    Destroy(gameObject);
                });
            });
        }
    }

}

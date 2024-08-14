using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float destroyTime;
    void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer)
        {
            DOVirtual.DelayedCall(destroyTime / 2, delegate
            {
                spriteRenderer.DOColor(Color.clear, destroyTime / 2).OnComplete(delegate
                {
                    Destroy(gameObject);
                });
            });
        }
    }
    private void OnDestroy()
    {
        DOTween.KillAll();
    }


}

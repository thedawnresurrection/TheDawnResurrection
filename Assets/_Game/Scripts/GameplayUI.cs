using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    public Image ammoFillImage;
    public TextMeshProUGUI ammoText;
    public Image flashImage;
    private void Start()
    {
        GameEvents.AmmoResourceAmountUpdateEvent.AddListener(AmmoResourceAmountUpdate);
        GameEvents.ExpolisionFlashBombEvent.AddListener(ExpolisionFlashBomb);
    }

    private void OnDestroy()
    {
        GameEvents.AmmoResourceAmountUpdateEvent.RemoveListener(AmmoResourceAmountUpdate);
        GameEvents.ExpolisionFlashBombEvent.RemoveListener(ExpolisionFlashBomb);
    }
    private void AmmoResourceAmountUpdate(float maxAmmo, float currentAmmo)
    {
        ammoFillImage.fillAmount = currentAmmo / maxAmmo;
        ammoText.text = "%" + Convert.ToInt32((currentAmmo / maxAmmo) * 100).ToString();
    }
    private void ExpolisionFlashBomb(float duration, float freezeTime)
    {
        DOVirtual.Float(0, 1, duration / 4, delegate (float x)
        {
            Color color = flashImage.color;
            color.a = x;
            flashImage.color = color;
        }).OnComplete(delegate
        {
            DOVirtual.Float(1, 0, duration / 2, delegate (float x)
            {
                Color color = flashImage.color;
                color.a = x;
                flashImage.color = color;
            });
        });
    }
}

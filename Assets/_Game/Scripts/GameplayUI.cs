using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    public Image ammoFillImage;
    public TextMeshProUGUI ammoText;
    private void Start()
    {
        GameEvents.AmmoResourceAmountUpdate.AddListener(AmmoResourceAmountUpdate);
    }

    private void OnDestroy()
    {
        GameEvents.AmmoResourceAmountUpdate.RemoveListener(AmmoResourceAmountUpdate);
    }
    private void AmmoResourceAmountUpdate(float maxAmmo, float currentAmmo)
    {
        ammoFillImage.fillAmount = currentAmmo / maxAmmo;
        ammoText.text = "%"+((currentAmmo / maxAmmo) * 100).ToString();
    }
}

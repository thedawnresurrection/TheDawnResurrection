using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;


public class AmmoUI : MonoBehaviour
{
    
    public static AmmoUI Instance;

    [SerializeField] TextMeshProUGUI AmmoText;

    public AmmoController ammoController;

    [SerializeField] Image ammoFillImg;

    


    private void Awake()
    {
        Instance = this;

    }

    private void Update()
    {
        StartAmmoFNC();
    }

    public void StartAmmoFNC()
    {



        AmmoText.text = "%" + Math.Round((float)ammoController.currentAmmo / ammoController.maxAmmo * 100f);

        float fillAmount = (float)ammoController.currentAmmo / ammoController.maxAmmo;
        ammoFillImg.DOFillAmount(fillAmount, 0.3f);



    }

    


}

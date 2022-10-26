using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlant : BuildingBase
{
    private void OnEnable()
    {
        InputManager.Instance.onLeftClick += BuildingDeselected;
    }

    private void BuildingDeselected()
    {
        selectedImage.SetActive(false);
    }
}

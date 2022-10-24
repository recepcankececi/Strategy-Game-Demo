using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BuildingBase : MonoBehaviour
{
    public void Start()
    {
        AstarPath.active.Scan();
    }
}

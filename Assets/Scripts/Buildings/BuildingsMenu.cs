using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingsMenu : MonoSingleton<BuildingsMenu>
{
    [SerializeField] private List<GameObject> buildingBlueprints;

    public void CreateBlueprint(int index)
    {
        Instantiate(buildingBlueprints[index]);
    }
}

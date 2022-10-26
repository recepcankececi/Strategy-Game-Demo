using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingsMenu : MonoSingleton<BuildingsMenu>
{
    [SerializeField] private List<GameObject> buildingBlueprints;

    public void CreateBlueprint(int index)
    {
        buildingBlueprints[index].transform.position = InputManager.Instance.GetMouseWorldPosition();
        buildingBlueprints[index].SetActive(true);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Barracks : BuildingBase, ICanProduce
{
    [SerializeField] private Transform spawnPoint;
    private Vector3 targetPoint;

    private void Awake()
    {
        targetPoint = spawnPoint.position;
        targetFlag.transform.position = targetPoint;
    }

    private void OnEnable()
    {
        InputManager.Instance.onLeftClick += BuildingDeselected;
        InputManager.Instance.onRightClick += SetSpawnPoint;
    }

    public void SetSpawnPoint()
    {
        if(!buildingSelected) return;
        targetPoint = InputManager.Instance.GetMouseWorldPosition();
        targetFlag.transform.position = targetPoint;
    }

    public void BuildingDeselected()
    {
        buildingSelected = false;
        
        if (targetFlag)
        {
            print("kaldırıldı");
            targetFlag.SetActive(false);
        }
    }

    protected override void ProduceUnitMethod(GameObject unitToProduce)
    {
        GameObject unit = Instantiate(unitToProduce, spawnPoint.position, quaternion.identity);

        Vector3 randomness = new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), 0);
        Vector3 lastTarget = targetPoint + randomness;
        unit.GetComponent<UnitMovementController>().MoveToTarget(lastTarget);
    }
    
}

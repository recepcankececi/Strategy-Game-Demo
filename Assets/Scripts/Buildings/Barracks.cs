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
        //Subscribe for left click and right click events for building selection and spawn point placement
        InputManager.Instance.onLeftClick += BuildingDeselected;
        InputManager.Instance.onRightClick += SetSpawnPoint;
    }

    public void SetSpawnPoint()
    {
        //Sets new destination for spawned units when right clicked while a building selected
        if(!buildingSelected) return;
        targetPoint = InputManager.Instance.GetMouseWorldPosition();
        targetFlag.transform.position = targetPoint;
    }

    public void BuildingDeselected()
    {
        buildingSelected = false;
        
        if (targetFlag)
        {
            targetFlag.SetActive(false);
            selectedImage.SetActive(false);
        }
    }

    protected override void ProduceUnitMethod(GameObject unitToProduce)
    {
        //Producing a unit with given spawn point and moves it to given destination point
        GameObject unit = Instantiate(unitToProduce, spawnPoint.position, quaternion.identity);

        Vector3 randomness = new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), 0);
        Vector3 lastTarget = targetPoint + randomness;
        unit.GetComponent<UnitMovementController>().MoveToTarget(lastTarget);
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingBase : MonoBehaviour
{
    [SerializeField] private string buildingName;
    [SerializeField] private Sprite buildingImage;
    [SerializeField] private List<GameObject> producibleUnits;
    
    private InformationPanelUpdater informationPanelUpdater;

    public delegate void ProduceUnit(GameObject unitToProduce);

    private ProduceUnit produceUnit;

    protected void Start()
    {
        AstarPath.active.Scan();
        informationPanelUpdater = FindObjectOfType<InformationPanelUpdater>();
        produceUnit = ProduceUnitMethod;
    }

    protected void OnMouseDown()
    {
        informationPanelUpdater.UpdateInformationPanel(buildingName, buildingImage, producibleUnits, produceUnit);
    }
    
    private void ProduceUnitMethod(GameObject unittoproduce)
    {
        
    }

}

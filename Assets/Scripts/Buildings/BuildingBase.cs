using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingBase : MonoBehaviour
{
    [SerializeField] private string buildingName;
    [SerializeField] private Sprite buildingImage;
    [SerializeField] protected GameObject targetFlag;
    [SerializeField] protected GameObject selectedImage;
    [SerializeField] private List<GameObject> producibleUnits;
    
    private InformationPanelUpdater informationPanelUpdater; 
    protected bool buildingSelected;

    public delegate void ProduceUnit(GameObject unitToProduce); 

    private ProduceUnit produceUnit;

    protected void Start()
    {
        //When a building is completed perceive it as an obstacle and update the available paths for units
        AstarPath.active.Scan();
        informationPanelUpdater = FindObjectOfType<InformationPanelUpdater>();
        produceUnit = ProduceUnitMethod;
    } 

    protected void OnMouseUp()
    {
        //When a building is clicked gives its infos as parameters to information panel and updates information.
        informationPanelUpdater.UpdateInformationPanel(buildingName, buildingImage, producibleUnits, produceUnit);
        buildingSelected = true;
        selectedImage.SetActive(true);
        if (targetFlag)
        {
            targetFlag.SetActive(true);
        }
    }
    
    protected virtual void ProduceUnitMethod(GameObject unitToProduce)
    {
        GameObject unit = Instantiate(unitToProduce);
    }

}

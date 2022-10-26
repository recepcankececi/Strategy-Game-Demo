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
    [SerializeField] private List<GameObject> producibleUnits;
    
    private InformationPanelUpdater informationPanelUpdater;
    protected bool buildingSelected;

    public delegate void ProduceUnit(GameObject unitToProduce);

    private ProduceUnit produceUnit;

    protected void Start()
    {
        AstarPath.active.Scan();
        informationPanelUpdater = FindObjectOfType<InformationPanelUpdater>();
        produceUnit = ProduceUnitMethod;
    } 

    protected void OnMouseUp()
    {
        informationPanelUpdater.UpdateInformationPanel(buildingName, buildingImage, producibleUnits, produceUnit);
        buildingSelected = true;
        if (targetFlag)
        {
            print("seçildi");
            targetFlag.SetActive(true);
        }
    }
    
    protected virtual void ProduceUnitMethod(GameObject unitToProduce)
    {
        GameObject unit = Instantiate(unitToProduce);
    }

}

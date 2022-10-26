using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InformationPanelUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buildingName;
    [SerializeField] private Image buildingImage;
    [SerializeField] private GameObject scrollContent;

    private List<GameObject> producibleUnits = new List<GameObject>();

    public void UpdateInformationPanel(string buildName, Sprite buildImage, List<GameObject> units, BuildingBase.ProduceUnit produceUnit)
    {
        buildingName.text = buildName;
        buildingImage.sprite = buildImage;
        producibleUnits = units;

        if (scrollContent.transform.childCount > 0)
        {
            foreach (Transform child in scrollContent.transform)
            {
                Destroy(child.gameObject);
            }
        }

        if (units == null) return;
        foreach (var item in producibleUnits)
        {
            GameObject go = new GameObject();
            go.layer = 5;
            go.transform.parent = scrollContent.transform;
            go.transform.localScale = Vector3.one;
            go.AddComponent<Image>().sprite = item.GetComponent<Unit>().unitImage;
            go.AddComponent<Button>().onClick.AddListener((() => produceUnit(item)));
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    [SerializeField] private Transform selectionArea;
    private List<IControllable> selectedUnits;
    private Vector3 startPosition;
    private Camera mainCam;
    
    private void Awake()
    {
        mainCam = Camera.main;
        selectedUnits = new List<IControllable>();
    }

    private void Update()
    {
        BoxSelection();
    }

    private void BoxSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = GetMouseWorldPosition();
            selectionArea.gameObject.SetActive(true);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePosition = GetMouseWorldPosition();
            
            Vector3 lowerLeft = new Vector3(Mathf.Min(startPosition.x, currentMousePosition.x),
                Mathf.Min(startPosition.y, currentMousePosition.y));
            Vector3 upperRight = new Vector3(Mathf.Max(startPosition.x, currentMousePosition.x),
                Mathf.Max(startPosition.y, currentMousePosition.y));

            selectionArea.position = lowerLeft;
            selectionArea.localScale = upperRight - lowerLeft;
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectionArea.gameObject.SetActive(false);

            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, GetMouseWorldPosition());
            
            foreach (var item in selectedUnits)
            {
                item.Selected(false);
            }
            
            selectedUnits.Clear();
            
            foreach (var item in collider2DArray)
            {
                IControllable controllable = item.GetComponent<IControllable>();
                
                if (controllable != null)
                {
                    selectedUnits.Add(controllable);
                    controllable.Selected(true);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            List<Vector3> targetPositionList = GetPositionListAround(GetMouseWorldPosition(), 1f, selectedUnits.Count);
            int targetPositionIndex = 0;
            
            foreach (var item in selectedUnits)
            {
                item.MoveToTarget(targetPositionList[targetPositionIndex]);
                targetPositionIndex = (targetPositionIndex + 1) % targetPositionList.Count;
            }
        }
    }

    public Vector3 GetMouseWorldPosition()
    {
        var worldPosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        return worldPosition;
    }

    private List<Vector3> GetPositionListAround(Vector3 originPosition, float distance, int count)
    {
        List<Vector3> positionList = new List<Vector3>();

        for (int i = 0; i < count; i++)
        {
            float angle = i * (360f / count);
            Vector3 direction = ApplyRotationToVector(new Vector3(1, 0), angle);
            Vector3 position = originPosition + direction * distance;
            positionList.Add(position);
        }

        return positionList;
    }

    private Vector3 ApplyRotationToVector(Vector3 vector, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vector;
    }
}

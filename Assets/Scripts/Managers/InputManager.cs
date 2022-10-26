using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoSingleton<InputManager>
{
    [SerializeField] private Transform selectionArea;
    private List<IControllable> selectedUnits;
    private Vector3 startPosition;
    private Camera mainCam;
    
    public UnityAction onLeftClick;
    public UnityAction onRightClick;
    
    private void Awake()
    {
        mainCam = Camera.main;
        selectedUnits = new List<IControllable>();
    }

    private void Update()
    {
        BoxSelection();
        
        //Trigger an event when left clicked on a empty space or right click
        if (Input.GetMouseButtonDown(0) && !CheckForUiElement())
        {
            onLeftClick?.Invoke();
        }
        if (Input.GetMouseButtonDown(1))
        {
            onRightClick?.Invoke();
        }
    }

    private bool CheckForUiElement()
    {
        //Set up the new Pointer Event
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        //Set the Pointer Event Position to that of the game object
        pointerEventData.position = Input.mousePosition;
 
        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();
 
        //Raycast using the Graphics Raycaster and mouse click position
        GraphicRaycaster raycaster = FindObjectOfType<GraphicRaycaster>();
        raycaster.Raycast(pointerEventData, results);
        
        foreach (var item in results)
        {
            if (item.gameObject.layer == 5)
            {
                return true;
            }
        }

        return false;
    }

    private void BoxSelection()
    {
        //Create a rectangular box for selecting multiple units
        if (Input.GetMouseButtonDown(0) && !CheckForUiElement())
        {
            startPosition = GetMouseWorldPosition();
            selectionArea.gameObject.SetActive(true);
        }

        if (Input.GetMouseButton(0))
        {
            //Set lower left and upper right corners of the rectangular while cursor is moving
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
            //When selection is done all controllable objects put in a list and ready for command
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
            if(selectedUnits.Count > 0) AudioManager.Instance.PlaySelectSound();
        }

        if (Input.GetMouseButtonDown(1) && selectedUnits.Count > 0)
        {
            //When right clicked all the selected units is moving to clicked position with A* pathfinding movetotarget method.
            List<Vector3> targetPositionList = GetPositionListAround(GetMouseWorldPosition(), 1f, selectedUnits.Count);
            int targetPositionIndex = 0;
            
            foreach (var item in selectedUnits)
            {
                item.MoveToTarget(targetPositionList[targetPositionIndex]);
                targetPositionIndex = (targetPositionIndex + 1) % targetPositionList.Count;
            }
            AudioManager.Instance.PlayCommandSound();
        }
    }

    public Vector3 GetMouseWorldPosition()
    {
        //Returns world position of the mouse
        var worldPosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        return worldPosition;
    }

    private List<Vector3> GetPositionListAround(Vector3 originPosition, float distance, int count)
    {
        //Creates multiple different position for units for destination and simulates a more natural move pattern
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

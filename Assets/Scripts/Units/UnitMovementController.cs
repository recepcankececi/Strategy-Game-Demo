using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class UnitMovementController : MonoBehaviour, IControllable
{
    [SerializeField] private GameObject selectedImage;
    private AIPath aiPath;

    private void Awake()
    {
        aiPath = GetComponent<AIPath>();
    }

    public void MoveToTarget(Vector3 targetPosition)
    {
        aiPath.destination = targetPosition;
    }

    public void Selected(bool selected)
    {
        selectedImage.SetActive(selected);
    }
}

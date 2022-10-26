using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Build : MonoBehaviour
{
    [SerializeField] private GameObject buildingPrefab;
    private SpriteRenderer spriteRenderer;
    private Color blueprintColor;
    private bool canBuild;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        canBuild = true;
    }

    private void Update()
    {
        FollowCursor();
        
        if (Input.GetMouseButtonDown(0) && canBuild)
        {
            CompleteBuild();
        }
        if (Input.GetMouseButtonDown(1))
        {
            QuitBuilding();
        }
    }

    private void FollowCursor()
    {
        transform.position = InputManager.Instance.GetMouseWorldPosition();
    }

    private void CompleteBuild()
    {
        //Creates and place new building from blueprint at given location.
        Instantiate(buildingPrefab, transform.position, quaternion.identity);
        gameObject.SetActive(false);
    }

    private void QuitBuilding()
    {
        Destroy(gameObject);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        //While our building scheme is overlap with an existing building it can not be placed 
        if (other.gameObject.layer == 3)
        {
            canBuild = false;
            UpdateSpriteColor();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 3)
        {
            canBuild = true;
            UpdateSpriteColor();
        }
    }

    private void UpdateSpriteColor()
    {
        //Updates blueprint color according to suitability for placement; red if it can not place, green if it can
        blueprintColor = canBuild ? Color.green : Color.red;

        blueprintColor.a = 0.2f;
        spriteRenderer.color = blueprintColor;
    }
}

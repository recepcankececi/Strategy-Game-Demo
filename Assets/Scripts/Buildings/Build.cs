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
        Instantiate(buildingPrefab, transform.position, quaternion.identity);
        gameObject.SetActive(false);
    }

    private void QuitBuilding()
    {
        Destroy(gameObject);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
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
        blueprintColor = canBuild ? Color.green : Color.red;

        blueprintColor.a = 0.2f;
        spriteRenderer.color = blueprintColor;
    }
}

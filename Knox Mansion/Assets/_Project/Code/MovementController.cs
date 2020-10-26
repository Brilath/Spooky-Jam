using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Camera sceneCamera;

    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private float angleOffset;
    [SerializeField] private Vector2 mousePosition;
    [SerializeField] private Vector2 mouseScreenPosition;
    [SerializeField] private Vector2 priorMouseScreenPosition;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        sceneCamera = Camera.main;
        Health.OnHealthChanged += HandleHealthChange;
    }

    private void OnDestroy()
    {
        Health.OnHealthChanged -= HandleHealthChange;
    }

    private void HandleHealthChange(int health)
    {
        if(health <= 0 )
        {
            this.enabled = false;
        }
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        mouseScreenPosition = Input.mousePosition;
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        if(priorMouseScreenPosition != mouseScreenPosition)
        {
            mousePosition = sceneCamera.ScreenToWorldPoint(mouseScreenPosition);
            Vector2 aimDirection = mousePosition - body.position;
            float aimAngel = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg + angleOffset;

            body.rotation = aimAngel;
            priorMouseScreenPosition = mouseScreenPosition;
        }
    }
}

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

    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip walkClip;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        sceneCamera = Camera.main;
        Health.OnHealthChanged += HandleHealthChange;
        WinController.OnWinGame += HandleWinGame;
    }

    private void OnDestroy()
    {
        Health.OnHealthChanged -= HandleHealthChange;
        WinController.OnWinGame -= HandleWinGame;
    }

    private void HandleHealthChange(int health)
    {
        if(health <= 0 )
        {
            enabled = false;
        }
    }

    private void HandleWinGame()
    {
        enabled = false;
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
        anim.SetFloat("speed", body.velocity.magnitude);
        if(body.velocity.magnitude > 0 && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(walkClip);
        }

        if (priorMouseScreenPosition != mouseScreenPosition)
        {
            mousePosition = sceneCamera.ScreenToWorldPoint(mouseScreenPosition);
            Vector2 aimDirection = mousePosition - body.position;
            float aimAngel = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg + angleOffset;

            body.rotation = aimAngel;
            priorMouseScreenPosition = mouseScreenPosition;
        }
    }
}

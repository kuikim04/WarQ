using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private PlayerInput playerInput;

    [SerializeField] private float speed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = new PlayerInput();
    }
    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Update()
    {
        Vector2 input = playerInput.Input.Move.ReadValue<Vector2>();



        animator.SetFloat("xInput", input.x);
        animator.SetFloat("yInput", input.y);

        GetComponent<Rigidbody2D>().velocity = speed * input;

        Debug.Log(input.x);
        Debug.Log(input.y); 
    }

}

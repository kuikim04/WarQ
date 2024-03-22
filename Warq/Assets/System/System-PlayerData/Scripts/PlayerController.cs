using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private PlayerInput playerInput;

    [SerializeField] private float speed;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip walkSound;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = new PlayerInput();

        if(PlayerPrefs.HasKey(Key.KEY_PLAYERNAME))
            gameObject.SetActive(true);
        else gameObject.SetActive(false);
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
        bool isWalking = input.magnitude > 0;

        if (isWalking && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(walkSound);
        }
        animator.SetFloat("xInput", input.x);
        animator.SetFloat("yInput", input.y);

        GetComponent<Rigidbody2D>().velocity = speed * input;
    }
  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineWarrior : MonoBehaviour
{
    public State State;
    public float detectionRange;
    public LayerMask enemyLayer;
    CharacterMovement characterMovement;

    private Animator animator;
    void Start()
    {
        State = State.Walking;
        characterMovement = GetComponent<CharacterMovement>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(State == State.Walking)
        {
            animator.SetBool("Running", true);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, detectionRange, enemyLayer);

            if(hit.collider != null)
            {
                animator.SetBool("Running", false);

                State = State.Attack;
                StartCoroutine(Attack());

                characterMovement.enabled = false;
            }
            else
            {
                State = State.Walking;
            }
        }
    }
    IEnumerator Attack()
    {
        while (true)
        {
            animator.SetTrigger("Attacking");
            yield return new WaitForSeconds(characterMovement.speedAttack);
        }
    }
}

public enum State
{
    Idle,
    Walking,
    Attack,
    AttackCrit,
    Hit,
    Death
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private CharacterStat characterStat;

    private Rigidbody2D rb;
    [HideInInspector] public float speedWalk;
    [HideInInspector] public float speedAttack;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speedWalk = characterStat.SpeedWalk;
        speedAttack = characterStat.SpeedAttack;
    }

    void FixedUpdate()
    {
        rb.velocity = Vector2.right * speedWalk;

        Physics2D.IgnoreLayerCollision(8, 8);
    }
}

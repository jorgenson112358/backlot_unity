using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SandboxWerewolf : MonoBehaviour
{
    public int MaxHealth = 100;
    private int currentHealth;

    public LayerMask targetLayers;

    private bool isAttacking = false;
    private float timeToNextAttack;
    public float AttackRate = 3f;
    public int maxAttackDamage = 8;
    private float timeToNextJump;
    public float JumpRate = 5f;
    private bool isJumping = false;
    public float NeedsToJumpDistance = 2f;
    public float MaxJumpDistance = 10f;
    public Animator animator;

    private System.Random rand;
    public Transform attackPoint;
    public float attackRange = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        timeToNextAttack = Time.time + AttackRate;
        timeToNextJump = Time.time + JumpRate;
        rand = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking) {
            // check distance to player
            float distToPlayer = 11f;
            if (!isJumping && distToPlayer < MaxJumpDistance && distToPlayer >= NeedsToJumpDistance) {
                Jump();
            }
            else if (!isJumping && Time.time > timeToNextAttack) {
                Attack();
            }
        }
        
    }

    private void Jump() {
        Debug.Log("WW jumping");
        isJumping = true;
        animator.SetTrigger("isJumping");
    }

    private void Attack() {
        Debug.Log("WW attacking");
        isAttacking = true;
        animator.SetTrigger("isAttacking");
    }

    public void WWAttackHitCheck() {
        Debug.Log("WW Attack Hit Check");
        int dmg = rand.Next(1, maxAttackDamage);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, targetLayers);
        foreach(Collider2D enemy in hitEnemies) {
            Debug.Log("WW hit " + enemy.name);
            enemy.GetComponent<Huntress>().TakeDamage(dmg);
        }
    }

    public void DoneAttacking() {
        Debug.Log("WW done attacking");
        isAttacking = false;
        timeToNextAttack = Time.time + AttackRate;
    }

    public void DoneJumping() {
        Debug.Log("WW done jumping");
        isJumping = false;
        timeToNextJump = Time.time + JumpRate;
    }
}

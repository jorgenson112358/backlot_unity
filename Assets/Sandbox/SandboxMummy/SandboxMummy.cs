using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandboxMummy : MonoBehaviour
{
    public Animator animator;
    public LayerMask enemyLayers;
    public Transform attackPoint;
    public float attackRange = 1f;
    public int attackDamage = 4;
    private bool isAttacking = false;

    public float attackRate = 4f;
    float nextAttackTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        nextAttackTime = Time.time + attackRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking) {
            if (Time.time >= nextAttackTime) {
                Attack();
            }
        }
    }

    private void Attack() {
        isAttacking = true;
        animator.SetTrigger("isAttacking");

        // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // foreach(Collider2D enemy in hitEnemies) {
        //     Debug.Log("we hit " + enemy.name);

        //     enemy.GetComponent<Huntress>().TakeDamage(attackDamage);
        // }
    }

    public void AttackDone() {
        isAttacking = false;
        nextAttackTime = Time.time + attackRate;
        Debug.Log("NAT: " + nextAttackTime);

    }
}

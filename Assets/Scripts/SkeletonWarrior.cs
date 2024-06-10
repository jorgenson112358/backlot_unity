using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarrior : MonoBehaviour
{
    int currentHealth;
    public int MaxHealth = 21;
    public Animator animator;
    public GameManager gm;
    private float disappearAfterDeathTime = 3f;
    public int MaxAttackDamage = 4;
    public LayerMask targetLayers;
    public Transform attackPoint;
    public float attackRange = 0.1f;
    
    private System.Random rand;
    // Start is called before the first frame update
    void Start()
    {
        rand = new System.Random();
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0) {
            //Debug.Log("Busy dying");

            disappearAfterDeathTime -= Time.deltaTime;
            
            if (disappearAfterDeathTime <= 0f) {
                Destroy(gameObject);
            }
        }
    }

    public void AttackHitCheck() {
        Debug.Log("skelwar Attack Hit Check");
        int dmg = rand.Next(1, MaxAttackDamage);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, targetLayers);
        foreach(Collider2D enemy in hitEnemies) {
            //Debug.Log("WW hit " + enemy.name);
            enemy.GetComponent<Huntress>().TakeDamage(dmg);
        }
    }

    public void Attack() {
        Debug.Log("in attack()");
        animator.SetTrigger("AttackTrigger");
    }

    public void TakeDamage(int val) {
        if (currentHealth > 0) {
            Debug.Log("taking damage: " + val);
            currentHealth -= val;

            if (currentHealth <= 0) {
                Debug.Log("dying");
                Die();
            }
            else {
                Debug.Log("attacking");
                Attack();
            }
        }
    }

    void Die() {
        Debug.Log("skelwar died");

        animator.SetTrigger("DeathTrigger");
    }

        /* debugging help to visualize on screen, works in Scene mode in animator */
        // private void OnDrawGizmos() {
        //     Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        // }
}

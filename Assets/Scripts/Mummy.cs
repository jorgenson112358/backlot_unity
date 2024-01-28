using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mummy : MonoBehaviour
{
    public int MummyMaxHealth = 175;
    int currentHealth;
    public Animator animator;
    public GameManager gm;
    private float disappearAfterDeathTime = 5f;

    // patrolling
    private Rigidbody2D rb;
    private Transform destinationPoint;
    public float speed;
    public GameObject leftPatrolPoint;
    public GameObject rightPatrolPoint;
    private bool shouldPatrol = true;

    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MummyMaxHealth;

        rb = GetComponent<Rigidbody2D>();
        if (leftPatrolPoint != null && rightPatrolPoint != null) {
            destinationPoint = leftPatrolPoint.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0) {
            animator.SetBool("IsWalking", false);
            animator.SetTrigger("Dead");
            Debug.Log("Busy dying");

            disappearAfterDeathTime -= Time.deltaTime;
            
            if (disappearAfterDeathTime <= 0f) {
                Destroy(gameObject);
            }
        }
        else {
            if (shouldPatrol && leftPatrolPoint != null && rightPatrolPoint != null) {
                animator.SetBool("IsWalking", true);
                Vector2 point = destinationPoint.position - transform.position;
                if (destinationPoint == leftPatrolPoint.transform) {
                    rb.velocity = new Vector2(-speed, 0);
                }
                else if (destinationPoint == rightPatrolPoint.transform) {
                    rb.velocity = new Vector2(speed, 0);
                }

                if (Vector2.Distance(transform.position, destinationPoint.position) < 0.5f && destinationPoint == leftPatrolPoint.transform) {
                    flipDirection();
                    destinationPoint = rightPatrolPoint.transform;
                }
                else if (Vector2.Distance(transform.position, destinationPoint.position) < 0.5f && destinationPoint == rightPatrolPoint.transform) {
                    flipDirection();
                    destinationPoint = leftPatrolPoint.transform;
                }
            }
            else {
                Debug.Log("Not patrolling? What should happen here?");
            }
            // else if (animator.GetInteger("AnimState") == (int)BanditAnimState.CombatIdle) {
            //     Attack();
            // }
        }
    }

    private void Attack() {
        if (isAttacking == false) {
            Debug.Log("Attacking");
            animator.SetTrigger("Attack");
            
            isAttacking = true;
        }

        // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // foreach(Collider2D enemy in hitEnemies) {
        //     Debug.Log("we hit " + enemy.name);

        //     enemy.GetComponent<Enemy>().TakeDamage(weaponDamage);
        // }
    }

    //called from animation event on Heavy Bandit Attack animation
    public void DoneAttacking() {
        Debug.Log("Done attacking");
        isAttacking = false;
    }

    private void flipDirection() {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public void TakeDamage(int val) {
        shouldPatrol = false;
        currentHealth -= val;

        //play animation?
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0) {
            Die();
        }
        else {
            Attack();
        }
    }

    void Die() {
        //Debug.Log("enemy died");

        animator.SetTrigger("Death");

        // GetComponent<Collider2D>().enabled = false;
        // this.enabled = false;

        //gm.MummyDefeated();
    }
}

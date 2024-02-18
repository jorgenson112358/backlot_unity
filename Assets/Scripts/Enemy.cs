using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int WerewolfMaxHealth = 200;
    public int MummyMaxHealth = 175;
    private bool IsWerewolf = false;
    private bool IsMummy = false;
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
        if (gameObject.tag == "Werewolf") {
            currentHealth = WerewolfMaxHealth;
            IsWerewolf = true;
        }
        else if (gameObject.tag == "Mummy") {
            currentHealth = MummyMaxHealth;
            IsMummy = true;
        }
        else {
            currentHealth = maxHealth;
        }

        rb = GetComponent<Rigidbody2D>();
        if (leftPatrolPoint != null) {
            destinationPoint = leftPatrolPoint.transform;
        }
        //animator.SetBool("isMoving", true);
        animator.SetInteger("AnimState", (int)BanditAnimState.Run);
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
        else {
            if (shouldPatrol && leftPatrolPoint != null && rightPatrolPoint != null) {
                Vector2 point = destinationPoint.position - transform.position;
                if (destinationPoint == leftPatrolPoint.transform) {
                    rb.velocity = new Vector2(-speed, 0);
                }
                else if (destinationPoint == rightPatrolPoint.transform) {
                    rb.velocity = new Vector2(speed, 0);
                }

                if (destinationPoint == leftPatrolPoint.transform && Vector2.Distance(transform.position, destinationPoint.position) < 0.5f) {
                    flipDirection();
                    destinationPoint = rightPatrolPoint.transform;
                }
                else if (destinationPoint == rightPatrolPoint.transform && Vector2.Distance(transform.position, destinationPoint.position) < 0.5f) {
                    flipDirection();
                    destinationPoint = leftPatrolPoint.transform;
                }
            }
            else {
                Debug.Log("Not patrolling? What should happen here?");
                //Combat Idle to Attack loop?
                // chase able player?
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
        if (currentHealth > 0) {
            animator.SetInteger("AnimState", (int)BanditAnimState.CombatIdle);
        }
    }

    private void flipDirection() {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public void TakeDamage(int val) {
        if (currentHealth > 0) {
            shouldPatrol = false;
            animator.SetInteger("AnimState", (int)BanditAnimState.CombatIdle);
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
    }

    void Die() {
        //Debug.Log("enemy died");

        animator.SetTrigger("Death");

        // GetComponent<Collider2D>().enabled = false;
        // this.enabled = false;

        if (IsWerewolf) {
            gm.WerewolfDefeated();
        }
        else if (IsMummy) {
            //gm.MummyDefeated();
        }
    }
}

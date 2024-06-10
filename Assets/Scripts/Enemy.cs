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
    private SpriteRenderer enemySpriteRen;
    public int weaponDamage = 5;
    public LayerMask targetLayers;
    public Transform attackPoint;
    public float attackRange = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        enemySpriteRen = GetComponent<SpriteRenderer>();
        
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

                if (destinationPoint == leftPatrolPoint.transform) {
                    float dist = Vector2.Distance(transform.position, destinationPoint.position);
                    //Debug.Log("Dist to left: " + dist);
                    if (dist < 0.5f) {
                        destinationPoint = rightPatrolPoint.transform;
                        flipDirection(true);
                    }
                }
                else if (destinationPoint == rightPatrolPoint.transform)
                {
                    float dist = Vector2.Distance(transform.position, destinationPoint.position);
                    //Debug.Log("Dist to right: " + dist);
                    if (dist < 0.5f) {
                        destinationPoint = leftPatrolPoint.transform;
                        flipDirection(false);
                    }
                }
            }
            else {
                //Debug.Log("Not patrolling? What should happen here?");
                //Combat Idle to Attack loop?
                // chase able player?
            }
            // else if (animator.GetInteger("AnimState") == (int)BanditAnimState.CombatIdle) {
            //     Attack();
            // }
        }
    }

    public void AttackHitCheck() {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, targetLayers);
        foreach(Collider2D enemy in hitEnemies) {
            Debug.Log("enemy dude hit " + enemy.name);

            enemy.GetComponent<Huntress>().TakeDamage(weaponDamage);
        }

    }

    private void Attack() {
        if (isAttacking == false) {
            Debug.Log("Attacking");
            animator.SetTrigger("Attack");
            
            isAttacking = true;
        }
    }

    //called from animation event on Heavy Bandit Attack animation
    public void DoneAttacking() {
        Debug.Log("Done attacking");
        isAttacking = false;
        if (currentHealth > 0) {
            //animator.SetInteger("AnimState", (int)BanditAnimState.CombatIdle);
        }
    }

    private void flipDirection(bool flip) {
        // Vector3 localScale = transform.localScale;
        // localScale.x *= -1;
        // transform.localScale = localScale;
        //Debug.Log("flipping " + flip);
        enemySpriteRen.flipX = flip;

    }

    public void TakeDamage(int val) {
        if (currentHealth > 0) {
            Debug.Log("taking damage: " + val);
            shouldPatrol = false;
            currentHealth -= val;
            //animator.SetInteger("AnimState", (int)BanditAnimState.CombatIdle);

            //play animation?
            //animator.SetTrigger("Hurt");

            if (currentHealth <= 0) {
                Die();
            }
            else {
                Attack();
            }
        }
    }

    void Die() {
        Debug.Log("enemy died");

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

    /* debugging help to visualize on screen, works in Scene mode in animator */
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

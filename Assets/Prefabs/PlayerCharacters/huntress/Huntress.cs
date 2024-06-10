using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Huntress : MonoBehaviour
{

    //left off video at 1:46 - section 11 obstacles

    [SerializeField] private int moveSpeed = 10;
    //private Animation anim;
    private bool crouching = false;
    private bool jumping = false;
    private float horizontalMove = 0f;

    public CharacterController2D controller;
    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int weaponDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public int MaxHealth = 100;
    public int currentHealth;
    public GameManager gm;
    public Scoring scoreUI;

    private int healthPotCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        //controller = gameObject.Get
        //anim = gameObject.GetComponent<Animation>();
        //anim["spin"].layer = 123;

        currentHealth = MaxHealth;
        Debug.Log(currentHealth);
        Debug.Log(MaxHealth);
        Debug.Log(scoreUI);
        scoreUI.UpdateScore(currentHealth, MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > 0) {
            if (Time.time >= nextAttackTime) {
                if (Input.GetButtonDown("Fire1")) {
                    Attack();
                    nextAttackTime = Time.time + 1f/ attackRate;
                }
            }
            // if (anim.isPlaying) {
            //     return;
            // }

            horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump")) {
                //Debug.Log("jumping");
                jumping = true;
                animator.SetBool("isJumping", true);
            }

            if (Input.GetButtonDown("Crouch")) {
                crouching = true;
            } 
            else if (Input.GetButtonUp("Crouch")) {
                crouching = false;
            }

            if (Input.GetButtonDown("HealthPot")) {
                DrinkHealthPot();
            }
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouching, jumping);
        jumping = false;
    }

    public void OnLanding() {
        animator.SetBool("isJumping", false);
        //Debug.Log("landed");
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnMouseDrag()
    {
        //transform.position = Input.MousePosition; //screen coordinates

        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }

    public void Attack() {
        animator.SetTrigger("isAttacking");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies) {
            Debug.Log("huntress attack hits " + enemy.name);

            if (enemy.tag.ToLower() == "mummy") {
                enemy.GetComponent<Mummy>().TakeDamage(weaponDamage);
            }
            else if (enemy.tag.ToLower() == "werewolf") {
                enemy.GetComponent<SandboxWerewolf>().TakeDamage(weaponDamage);
            }
            else {
                //enemy.GetComponent<Enemy>().TakeDamage(weaponDamage);
                enemy.gameObject.SendMessage("TakeDamage", weaponDamage);
            }
        }
    }

    public void TakeDamage(int dmg) {
        Debug.Log("Huntress hit for " + dmg);
        UpdateHealth(dmg, true);

        if (currentHealth <= 0) {
            //SceneManager.LoadScene(SceneNamesEnum.Defeat.ToString());
            Debug.Log("DEFEAT!");
            gm.Defeat();
        }
    }

    private void UpdateHealth(int amt, bool isDamage) {
        if (isDamage) {
            currentHealth -= amt;
        }
        else {
            if ( (currentHealth + amt) > MaxHealth) {
                currentHealth = MaxHealth;
            }
            else {
                currentHealth += amt;
            }
        }

        scoreUI.UpdateScore(currentHealth, MaxHealth);
    }

    // do I want to have pots of different values?
    public void PickupHealthPot(int amount) {
        // //Debug.Log("health pot picked up");
        // if ( (currentHealth + amount) < MaxHealth) {
        //     // just drink the pot since we can use all of it
        //     UpdateHealth(amount, false);
        // }
        // else {
            healthPotCount += 1;
            scoreUI.UpdatePotCount(healthPotCount);
        //}
    }

    private void DrinkHealthPot() {
        if (healthPotCount > 0) {
            UpdateHealth(8, false);

            healthPotCount -= 1;
            scoreUI.UpdatePotCount(healthPotCount);
        }
    }
}

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

    public int Health = 100;

    // Start is called before the first frame update
    void Start()
    {
        //controller = gameObject.Get
        //anim = gameObject.GetComponent<Animation>();
        //anim["spin"].layer = 123;
    }

    // Update is called once per frame
    void Update()
    {
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

        // if (Input.GetButtonDown("Cancel")) {
        //     SceneManager.LoadScene(SceneNamesEnum.MainMenu.ToString());
        // }

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
            Debug.Log("we hit " + enemy.name);

            enemy.GetComponent<Enemy>().TakeDamage(weaponDamage);
        }

    }
}

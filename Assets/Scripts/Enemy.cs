using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int WerewolfMaxHealth = 200;
    private bool IsWerewolf = false;
    int currentHealth;
    public Animator animator;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Werewolf") {
            currentHealth = WerewolfMaxHealth;
            IsWerewolf = true;
        }
        else {
            currentHealth = maxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int val) {
        currentHealth -= val;

        //play animation?
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0) {
            Die();
        }
    }

    void Die() {
        Debug.Log("enemy died");

        animator.SetTrigger("Death");

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        gm.WerewolfDefeatedSaveContinue();
    }
}

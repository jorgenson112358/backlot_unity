using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Werewolf : MonoBehaviour
{
    public int maxHealth = 200;
    int currentHealth;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int val) {
        Debug.Log("Taking damage");
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
    }
}

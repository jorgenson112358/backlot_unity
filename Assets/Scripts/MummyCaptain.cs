using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyCaptain : MonoBehaviour
{
    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack() {
        animator.SetTrigger("isAttacking");

        // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // foreach(Collider2D enemy in hitEnemies) {
        //     Debug.Log("we hit " + enemy.name);

        //     if (enemy.tag.ToLower() == "mummy") {
        //         enemy.GetComponent<Mummy>().TakeDamage(weaponDamage);
        //     }
        //     else {
        //         enemy.GetComponent<Enemy>().TakeDamage(weaponDamage);
        //     }
        // }
    }
}

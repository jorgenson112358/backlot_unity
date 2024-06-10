using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingMace : MonoBehaviour
{
    private const int DamageAmount = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //collision detection
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.SendMessage("TakeDamage", DamageAmount);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapLog : MonoBehaviour
{
    public int DamageAmount = 24;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.SendMessage("TakeDamage", DamageAmount);
        }
    }
}
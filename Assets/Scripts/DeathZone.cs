using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player")) {
            SceneManager.LoadScene("Defeat");
            //GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().LoseLife();
        }
    }
}

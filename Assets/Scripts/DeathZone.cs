using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player")) {
            SceneManager.LoadScene(SceneNamesEnum.Defeat.ToString());

            //are we going with hit points or X number of lives before you lose the game?
            //GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().LoseLife();
        }
    }
}

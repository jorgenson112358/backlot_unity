using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVictory : MonoBehaviour
{
    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player")) {
            //SceneManager.LoadScene(SceneNamesEnum.MummyVictory.ToString());
            gm.YouWin();
        }
    }
}

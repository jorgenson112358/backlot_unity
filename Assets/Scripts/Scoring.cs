using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoring : MonoBehaviour
{
    public TMP_Text score;
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int playerHealth) {
        string output = string.Format("Health {0}", playerHealth);

        score.text = output;
    }
}

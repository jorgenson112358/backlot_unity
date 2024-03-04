using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoring : MonoBehaviour
{
    public TMP_Text score;
    public TMP_Text healthPotCount;
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int playerHealth, int playerMaxHealth) {
        string output = string.Format("Health {0}/{1}", playerHealth, playerMaxHealth);

        score.text = output;
    }

    public void UpdatePotCount(int count) {
        healthPotCount.text = count.ToString();
    }
}

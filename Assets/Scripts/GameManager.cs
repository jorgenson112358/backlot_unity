using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene("MummyScene");
    }

    public void SaveGame() {

    }

    public void MummyDefeatedSaveContinue() {
        SaveGame();
        SceneManager.LoadScene("WerewolfVillage");
    }

    public void QuitGame() {
        Debug.Log("quitting");
        Application.Quit();
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel")) {
            SceneManager.LoadScene("MainMenu");
        }
    }
}

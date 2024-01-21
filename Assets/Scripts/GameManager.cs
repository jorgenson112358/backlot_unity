using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene(SceneNamesEnum.MummyScene1Intro.ToString());
    }

    public void SaveGame() {
        Debug.Log("Save not yet implemented");
    }

    public void MummyDefeatedSaveContinue() {
        SaveGame();
        SceneManager.LoadScene(SceneNamesEnum.WerewolfVillage1Intro.ToString());
    }

    public void WerewolfDefeatedSaveContinue() {
        SaveGame();
        SceneManager.LoadScene(SceneNamesEnum.Vampire1Intro.ToString());
    }

    public void QuitGame() {
        Debug.Log("quitting");
        Application.Quit();
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene(SceneNamesEnum.MainMenu.ToString());
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel")) {
            SceneManager.LoadScene(SceneNamesEnum.MainMenu.ToString());
        }
    }
}

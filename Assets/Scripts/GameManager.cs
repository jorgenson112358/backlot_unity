using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void PlayGame() {
        Cursor.visible = false;
        SceneManager.LoadScene(SceneNamesEnum.MummyScene1Intro.ToString());
    }

    public void SaveGame() {
        Debug.Log("Save not yet implemented");
    }

    public void MummyDefeated() {
        Cursor.visible = true;
        SceneManager.LoadScene(SceneNamesEnum.MummyVictory.ToString());
    }

    public void MummyDefeatedSaveContinue() {
        SaveGame();
        Cursor.visible = false;
        SceneManager.LoadScene(SceneNamesEnum.WerewolfVillage1Intro.ToString());
    }

    public void WerewolfDefeated() {
        Cursor.visible = true;
        SceneManager.LoadScene(SceneNamesEnum.WerewolfVictory.ToString());
    }

    public void WerewolfDefeatedSaveContinue() {
        SaveGame();
        Cursor.visible = false;
        SceneManager.LoadScene(SceneNamesEnum.Vampire1Intro.ToString());
    }

    public void VampireDefeatedGameVictory() {
        Cursor.visible = true;
        SceneManager.LoadScene(SceneNamesEnum.GameVictory.ToString());
    }

    public void QuitGame() {
        Debug.Log("quitting");
        Application.Quit();
    }

    public void LoadMainMenu() {
        Cursor.visible = true;
        SceneManager.LoadScene(SceneNamesEnum.MainMenu.ToString());
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel")) {
            Cursor.visible = true;
            SceneManager.LoadScene(SceneNamesEnum.MainMenu.ToString());
        }
    }

    public void Defeat() {
        Cursor.visible = true;
        SceneManager.LoadScene(SceneNamesEnum.Defeat.ToString());
    }

    public void YouWin() {
        Cursor.visible = true;
        SceneManager.LoadScene(SceneNamesEnum.GameVictory.ToString());
    }
}

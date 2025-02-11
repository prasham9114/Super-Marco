using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void playGame() {
        SceneManager.LoadScene("GamePlay");
    }

    public void quitGame()
    {
        // Closes the game
        Application.Quit();

        // For testing in the Unity Editor (this won't work in a built game)
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}

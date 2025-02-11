using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(LoadMainMenu());
    }
    IEnumerator LoadMainMenu()
    {
        yield return new WaitForSecondsRealtime(6f);
        SceneManager.LoadScene("Main Menu");
    }
}

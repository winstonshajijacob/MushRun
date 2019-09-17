using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseScreen;
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
    }
    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
        FindObjectOfType<GameManager>().Reset();
    }
    public void QuitToMain()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

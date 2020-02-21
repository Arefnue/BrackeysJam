using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UiMaster : MonoBehaviour
{

    #region Singleton
    public static UiMaster instance;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    

    #endregion

    [Header("Panels")]
    public GameObject playerDeadPanel;
    public GameObject holwyDeadPanel;
    public GameObject nextLevelPanel;
    public GameObject pausePanel;
    public GameObject creditsPanel;

    public Health carrySlider;

    public void OpenPlayerDeadPanel(bool isOpen)
    {
        
        if (isOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            playerDeadPanel.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            playerDeadPanel.SetActive(false);
        }
    }
    
    public void OpenHolwyDeadPanel(bool isOpen)
    {
        if (isOpen) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            holwyDeadPanel.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            holwyDeadPanel.SetActive(false);
        }
    }
    
    public void OpenNextLevelPanel(bool isOpen)
    {
        if (isOpen) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            nextLevelPanel.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            nextLevelPanel.SetActive(false);
        }
    }
    
    public void OpenPausePanel(bool isOpen)
    {
        if (isOpen) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }

    public void OpenCreditsPanel( bool isOpen)
    {
        if (isOpen)
        {
            Time.timeScale = 0;
            creditsPanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            creditsPanel.SetActive(false);
        }
    }

    public void OpenScene(int id)
    {
        if (id == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(id);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
}

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

    public GameObject playerDeadPanel;
    public GameObject holwyDeadPanel;
    public GameObject nextLevelPanel;
    public GameObject pausePanel;

    

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
            Time.timeScale = 1;
            playerDeadPanel.SetActive(false);
        }
    }
    
    public void OpenHolwyDeadPanel(bool isOpen)
    {
        if (isOpen) 
        {
            Time.timeScale = 0;
            holwyDeadPanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            holwyDeadPanel.SetActive(false);
        }
    }
    
    public void OpenNextLevelPanel(bool isOpen)
    {
        if (isOpen) 
        {
            Time.timeScale = 0;
            nextLevelPanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            nextLevelPanel.SetActive(false);
        }
    }
    
    public void OpenPausePanel(bool isOpen)
    {
        if (isOpen) 
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }

    public void OpenScene(int id)
    {
        SceneManager.LoadScene(id);
    }
    
}

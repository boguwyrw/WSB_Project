using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerUI : MonoBehaviour
{
    #region ManagerUI_Singleton
    static ManagerUI _instance;

    public static ManagerUI Instance
    {
        get
        {
            if (_instance == null) _instance = new ManagerUI();
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }
    #endregion

    [SerializeField] GameObject winLosePanel;
    [SerializeField] GameObject infoPanel;
    [SerializeField] Text winLoseText;

    float timeToHidePanel = 1.6f;

    const string YouWin = "Wygrałeś";
    const string YouLose = "Przegrałeś";

    void ActivateDeactivatePanel(bool panelAction)
    {
        winLosePanel.SetActive(panelAction);
    }

    public void ShowWinText()
    {
        ActivateDeactivatePanel(true);
        winLoseText.color = Color.green;
        winLoseText.text = YouWin;
    }

    public void ShowLoseText()
    {
        ActivateDeactivatePanel(true);
        winLoseText.color = Color.red;
        winLoseText.text = YouLose;
    }

    public void ShowInfoPanel()
    {
        infoPanel.SetActive(true);
    }

    public void HideInfoPanel()
    {
        infoPanel.SetActive(false);
    }

    public IEnumerator DelayHideInfoPanel()
    {
        yield return new WaitForSeconds(timeToHidePanel);
        HideInfoPanel();
    }

    public void RestartGameButton()
    {
        ActivateDeactivatePanel(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGameButton()
    {
        GameController.Instance.ExitGame();
    }
}

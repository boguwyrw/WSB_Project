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
    [SerializeField] Text winLoseText;

    const string YouWin = "Wygrałeś";
    const string YouLose = "Przegrałeś";

    private void ActivateDeactivatePanel(bool panelAction)
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

    public void RestartGameButton()
    {
        ActivateDeactivatePanel(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region GameController_Singleton
    static GameController _instance;

    public static GameController Instance
    {
        get
        {
            if (_instance == null) _instance = new GameController();
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }
    #endregion

    [SerializeField] GameObject playerGO;
    [SerializeField] Transform enemies;
    [SerializeField] Transform cannonGuns;

    [SerializeField] Text livesText;
    [SerializeField] Text energyLevelText;

    int enemiesStage_1_Number = 0;
    int enemiesStage_2_Number = 0;
    int cannonGunsStage_1_Number = 0;
    int cannonGunsStage_2_Number = 0;
    int lives = 3;
    int energyLevel = 100;

    [HideInInspector] public int objectsToDestroyInStage_1_Number = 0;
    [HideInInspector] public int objectsToDestroyInStage_2_Number = 0;

    void Start()
    {
        enemiesStage_1_Number = enemies.GetChild(0).childCount;
        enemiesStage_2_Number = enemies.GetChild(1).childCount;
        cannonGunsStage_1_Number = cannonGuns.GetChild(0).childCount;
        cannonGunsStage_2_Number = cannonGuns.GetChild(1).childCount;

        objectsToDestroyInStage_1_Number = enemiesStage_1_Number + cannonGunsStage_1_Number;
        objectsToDestroyInStage_2_Number = enemiesStage_2_Number + cannonGunsStage_2_Number;

        UpdatePlayerHealth(0);
    }

    void Update()
    {
        if (lives == 0)
        {
            playerGO.SetActive(false);
            energyLevel = 0;
            energyLevelText.text = "Poziom gnergii: " + energyLevel.ToString() + " %";
            ManagerUI.Instance.ShowLoseText();
        }
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }

    public void UpdatePlayerHealth(int damage)
    {
        energyLevel = energyLevel - damage;

        if (energyLevel <= 0)
        {
            lives--;
            energyLevel = 100;
        }

        livesText.text = "Życia: " + lives.ToString();
        energyLevelText.text = "Poziom gnergii: " + energyLevel.ToString() + " %";
    }

    public void UpdatePlayerLives()
    {
        lives--;
        energyLevel = 100;

        livesText.text = "Życia: " + lives.ToString();
        energyLevelText.text = "Poziom gnergii: " + energyLevel.ToString() + " %";
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

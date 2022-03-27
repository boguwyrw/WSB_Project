using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Singleton
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

    [SerializeField] Transform enemies;
    [SerializeField] Transform cannonGuns;

    [SerializeField] Text livesText;
    [SerializeField] Text energyLevelText;

    int enemiesNumber = 0;
    int cannonGunsNumber = 0;
    int lives = 3;
    int energyLevel = 100;

    [HideInInspector] public int objectsToDestroyNumber = 0;

    void Start()
    {
        enemiesNumber = enemies.childCount;
        cannonGunsNumber = cannonGuns.childCount;

        objectsToDestroyNumber = enemiesNumber + cannonGunsNumber;

        UpdatePlayerHealth(0);
    }

    void Update()
    {
        
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
}

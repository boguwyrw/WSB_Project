using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    int enemiesNumber = 0;
    int cannonGunsNumber = 0;

    [HideInInspector] public int objectsToDestroyNumber = 0;

    void Start()
    {
        enemiesNumber = enemies.childCount;
        cannonGunsNumber = cannonGuns.childCount;

        objectsToDestroyNumber = enemiesNumber + cannonGunsNumber;
    }

    void Update()
    {
        
    }
}

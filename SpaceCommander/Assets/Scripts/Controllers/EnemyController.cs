using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ActionEnum currentAction;
    public BaseEnemy[] enemyList;
    public List<GameObject> lanePositions = new List<GameObject>(); // list of points where enemies will spawn

    public static EnemyController instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        enemyList = new BaseEnemy[lanePositions.Count];
    }


    public List<BaseEnemy> AllEnemies()
    {
        List<BaseEnemy> allEnemies = new List<BaseEnemy>();
        for (int i = 0; i < enemyList.Length; i++)
        {
            if (enemyList[i] != null)
            {
                allEnemies.Add(enemyList[i]);
            }
        }
        return allEnemies;

    }

    public int FreeFighterSpace()
    {
        int value = -1;
        for (int i = 0; i < enemyList.Length; i++)
        {
            if (enemyList[i] == null)
                value = i;

        }
        return value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

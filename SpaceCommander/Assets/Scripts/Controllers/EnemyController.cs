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

    // Update is called once per frame
    void Update()
    {
        
    }
}

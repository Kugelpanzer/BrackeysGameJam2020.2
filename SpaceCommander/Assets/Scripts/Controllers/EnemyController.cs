using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	private static System.Random rng = new System.Random ();
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
		return enemyList.Where( i => i != null ).ToList ();
    }

	public List<BaseEnemy> RandomEnemies(int amount)
	{
		return AllEnemies ().OrderBy ( i => rng.Next () ).Take ( amount ).ToList ();
	}

	public int FreeFighterSpace()
    {
		return Array.FindIndex ( enemyList, i => i == null );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

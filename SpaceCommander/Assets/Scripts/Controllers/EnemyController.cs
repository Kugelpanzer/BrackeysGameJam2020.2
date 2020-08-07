using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	private static System.Random rng = new System.Random ();
    public ActionEnum currentAction;
	[HideInInspector]
    public BaseEnemy[] enemyList;
    public List<GameObject> lanePositions = new List<GameObject>(); // list of points where enemies will spawn

	public GameObject EnemyPrefab;

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
        //DontDestroyOnLoad(gameObject);

        enemyList = new BaseEnemy[lanePositions.Count];
    }

	public void SpawnEnemy ()
	{
		int index = FreeEnemySpace ();
		if ( index == -1 ) return;

		GameObject newEnemy = Instantiate ( EnemyPrefab );

		newEnemy.transform.position = lanePositions [index].transform.position;
		enemyList [index] = newEnemy.GetComponent<BaseEnemy> ();
	}

	public List<BaseEnemy> AllEnemies()
    {
		return enemyList.Where( i => i != null ).ToList ();
    }

	public List<BaseEnemy> RandomEnemies(int amount)
	{
		return AllEnemies ().OrderBy ( i => rng.Next () ).Take ( amount ).ToList ();
	}

	public List<int> FreeSpaceLocations()
	{
		return enemyList
			.Select ( ( enemy, index ) => new { enemy, index } )
			.Where ( i => i.enemy == null )
			.Select ( i => i.index )
			.ToList ();
	}

	public int FreeEnemySpace()
    {
		List<int> freeLocations = FreeSpaceLocations ();
		int size = freeLocations.Count;
		if ( size == 0 ) return -1;
		return freeLocations [rng.Next ( size )];
	}

	public void Attack ()
	{
		foreach ( BaseEnemy enemy in AllEnemies () ) enemy.Attack ();
	}

	public void Spawn (int amount)
	{
		for ( int counter = 0; counter < amount; counter++ ) SpawnEnemy ();
	}

	public void Upgrade ()
	{
		foreach ( BaseEnemy enemy in AllEnemies () ) enemy.Upgrade ();
	}
	public void AfterPlayerAttack ()
	{
		// check if enemies are destroyed
		for ( int counter = enemyList.Length - 1; counter >= 0; counter-- )
		{
			if ( enemyList [counter] == null ) continue;
			if ( enemyList [counter].health < 0 )
			{
				// TODO animacija da je pukao i ukloniti
				enemyList [counter] = null;
			}
		}
	}
}

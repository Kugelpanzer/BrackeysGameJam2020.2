using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private static System.Random rng = new System.Random ();
	public static PlayerController instance;

    public List<GameObject> fighterPositions = new List<GameObject>();// positions where fighters can spawn on screen

    public GameObject fighterPrefab;

    public PlayerScript player;
    public BaseFighter[] fighters;

    public void SpawnFighter()
    {
        int index = FreeFighterSpace();
        if (index != -1)
        {
            GameObject currentFighter = Instantiate(fighterPrefab);

            fighters[index] = currentFighter.GetComponent<BaseFighter>();
            currentFighter.GetComponent<BaseFighter>().location = index;
            currentFighter.transform.position = fighterPositions[index].transform.position;
        }
        //fighters.Add(fighterPrefab.GetComponent<BaseFighter>());

    }

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
        fighters = new BaseFighter[fighterPositions.Count];
    }


    public List<BaseFighter> AllFighters()
    {
		return fighters.ToList ();
    }

	public List<int> FreeSpaceLocations ()
	{
		return fighters
			.Select ( ( enemy, index ) => new { enemy, index } )
			.Where ( i => i == null )
			.Select ( i => i.index )
			.ToList ();
	}

	public int FreeFighterSpace ()
	{
		List<int> freeLocations = FreeSpaceLocations ();
		if ( freeLocations.Count == 0 ) return -1;
		return freeLocations.OrderBy ( i => rng.Next () ).First ();
	}

	// Update is called once per frame
	void Update ()
    {
        
    }
}

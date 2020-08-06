﻿using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private static System.Random rng = new System.Random ();
    public int numberOfCards = 10;
    public List<Card> deck = new List<Card>();
	public int NextCardInDeck = 0;
	public bool MustRewind = false;
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
		return fighters.Where ( i => i != null ).ToList ();
	}

	public List<BaseFighter> RandomFighters ( int amount )
	{
		return AllFighters ().OrderBy ( i => rng.Next () ).Take ( amount ).ToList ();
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
		int size = freeLocations.Count;
		if ( size == 0 ) return -1;
		return freeLocations [rng.Next ( size )];
	}

    private void MakeDeck()
    {
        List<Card> allCards = Resources.LoadAll<Card>("Cards").ToList() ;
        List<Card> cardPool=new List<Card> ();
        foreach(Card c in allCards)
        {
            for (int i = 0;i< c.value;i++)
            {
                cardPool.Add(c);
            }
        }
        for(int i = 0; i < numberOfCards; i++)
        {
            int rand = UnityEngine.Random.Range(0, cardPool.Count);
            deck.Add(cardPool[rand]);
            cardPool.RemoveAt(rand);
        }

    }

	private void MoveToNextCard ()
	{
		for (int counter = NextCardInDeck + 1; counter != NextCardInDeck; counter ++ )
		{
			if ( counter == numberOfCards ) counter = 0;
			if (! deck[counter].isPlayed)
			{
				NextCardInDeck = counter;
				return;
			}
		}
		MustRewind = true;
	}

	public void Play()
	{
		// play cards
		deck[NextCardInDeck].doEffect ();

		// player attacks
		foreach ( BaseFighter fighter in AllFighters () ) fighter.Attack ();

		// enemies attack
		EnemyController.instance.Attack ();

		// spawn 1 enemy
		EnemyController.instance.Spawn ( 1 );

		// move to next card
		deck [NextCardInDeck].isPlayed = true;
		MoveToNextCard ();
	}

	public void Wind ()
	{
		// spawn 2 enemies
		EnemyController.instance.Spawn ( 2 );

		// wind
		MoveToNextCard ();
	}

	public void Rewind(int position)
	{
		// spawn 1 enemy
		EnemyController.instance.Spawn ( 1 );

		// upgrade enemies
		EnemyController.instance.Upgrade ();

		// rewind
		deck.RemoveAt ( NextCardInDeck );
		numberOfCards--;
		if ( position > NextCardInDeck ) position--;
		FadeBackAllCards ();
		NextCardInDeck = position;
	}

	private void FadeBackAllCards ()
	{
		foreach ( Card card in deck ) card.isPlayed = false;
	}

	// Update is called once per frame
	void Update ()
    {
        
    }
}

using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private static System.Random rng = new System.Random ();
    public int numberOfCards = 10;
    public List<CardScript> deck = new List<CardScript> ();
	public GameObject cardPrefab;
	public int NextCardInDeck = 0;
	public bool MustRewind = false;
	public static PlayerController instance;

    public List<GameObject> fighterPositions = new List<GameObject>();// positions where fighters can spawn on screen
	public List<GameObject> CardPositions = new List<GameObject> ();
	public GameObject DiscardPosition;
	public float DiscardStackDelta = 0.01f;

    public GameObject fighterPrefab;

    public PlayerScript player;
	[HideInInspector]
    public BaseFighter[] fighters;

	public bool shouldTarget=false;
	private int numberOfTargets = 0;
	private List<BaseEnemy> targetList = new List<BaseEnemy>();

	public void AddTarget(BaseEnemy enemy)
    {
		targetList.Add(enemy);
        if (targetList.Count == numberOfTargets)
        {
			shouldTarget = false;
			deck[NextCardInDeck].SetTargets(targetList);
			ExecutePlay();
        }
		
    }
	public void RemoveEnemy(BaseEnemy enemy)
	{
		targetList.Remove(enemy);
	}

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
        //DontDestroyOnLoad(gameObject);
        fighters = new BaseFighter[fighterPositions.Count];
    }
    private void Start()
    {
		MakeDeck();
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
			.Select ( ( fighter, index ) => new { fighter, index } )
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
			// TODO inicijalizacija iz prefaba
			GameObject card = Instantiate(cardPrefab);
			card.GetComponent<CardScript>().cardData = cardPool[rand];
			deck.Add(card.GetComponent<CardScript>());
			card.transform.position = CardPositions[deck.IndexOf(card.GetComponent<CardScript>())].transform.position;
			card.GetComponent<CardScript>().CardInit();//Dodati posle da mu se sprite dodeli i jos nesto ako je potrebno

			cardPool.RemoveAt(rand);
        }
		MoveCardLocations();
	}

	private void MoveToNextCard ()
	{
		for (int counter = NextCardInDeck + 1; counter != NextCardInDeck; counter ++ )
		{
			if ( counter == numberOfCards ) counter = 0;
			if (! deck[counter].isPlayed)
			{
				NextCardInDeck = counter;
				MoveCardLocations ();
				return;
			}
		}
		MustRewind = true;
	}

	private void MoveCardLocations()
	{
		for ( int counter = 0; counter < numberOfCards; counter++ )
		{
			int position = counter - NextCardInDeck;
			if ( position < 0 ) position += numberOfCards;
			deck [counter].MoveTowardsHere = CardPositions [position].transform.position;
		}
	}

	public void Play()
	{
		if ( MustRewind ||shouldTarget) return;

        if (deck[NextCardInDeck].targetType == DamageType.Target)
        {
			numberOfTargets=deck[NextCardInDeck].numberOfTargets;
			shouldTarget = true;
        }
        else
        {
			ExecutePlay();
        }
	}

	private void ExecutePlay()
    {
		// play cards
		deck[NextCardInDeck].ExecuteCard();

		// player attacks
		foreach (BaseFighter fighter in AllFighters()) fighter.Attack();

		// enemies attack
		EnemyController.instance.Attack();
		AfterEnemyAttack();

		// spawn 1 enemy
		EnemyController.instance.Spawn(1);

		// check if fighters are expired
		OnEndOfTurn();

		// move to next card
		deck[NextCardInDeck].isPlayed = true;
		MoveToNextCard();
	}
	public void Wind ()
	{
		if ( MustRewind || shouldTarget) return;

		// spawn 2 enemies
		EnemyController.instance.Spawn ( 2 );

		// check if fighters are expired
		OnEndOfTurn ();

		// wind
		MoveToNextCard ();
	}

	public void Rewind(int position)
	{
		if (shouldTarget) return;
		// spawn 1 enemy
		EnemyController.instance.Spawn ( 1 );

		// upgrade enemies
		EnemyController.instance.Upgrade ();

		// check if fighters are expired
		OnEndOfTurn ();

		// rewind
		MustRewind = false;

		MoveCardToDiscardPile ( deck [NextCardInDeck] );
		deck.RemoveAt ( NextCardInDeck );
		numberOfCards--;
		if ( position > NextCardInDeck ) position--;
		FadeBackAllCards ();
		NextCardInDeck = position;
		MoveCardLocations ();

		if ( numberOfCards <= 1 ) EndGame ();
	}

	private void AfterEnemyAttack()
	{
		// check if fighters are destroyed
		for ( int counter = fighters.Length - 1; counter >= 0; counter --  )
		{
			if ( fighters [counter] == null ) continue;
			if ( fighters [counter].health < 0 )
			{
				// TODO animacija da je pukao i ukloniti
				fighters [counter] = null;
			}
		}
	}

	private void OnEndOfTurn()
	{
		for ( int counter = fighters.Length - 1; counter >= 0; counter-- )
		{
			if ( fighters [counter] == null ) continue;
			fighters [counter].OnEndOfTurn ();
			if ( fighters [counter].Longevity < 0 )
			{
				// TODO animacija da je zardjao i ukloniti
				fighters [counter] = null;
			}
		}
	}

	private void MoveCardToDiscardPile(CardScript card)
	{
		card.isPlayed = true;
		Vector3 DiscardLocation = DiscardPosition.transform.position;
		card.MoveTowardsHere = DiscardLocation;
		DiscardLocation.y = DiscardLocation.y + DiscardStackDelta;
		DiscardPosition.transform.position = DiscardLocation;
	}

	private void FadeBackAllCards ()
	{
		foreach ( CardScript card in deck ) card.isPlayed = false;
	}

	private void EndGame()
	{
		// TODO
	}

	// Update is called once per frame
	void Update ()
    {
        
    }
}

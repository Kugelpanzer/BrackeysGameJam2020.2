using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    public Card cardData;
	public bool isPlayed = false;
	public Vector3 MoveTowardsHere;

    private List<BaseEnemy> targetList = new List<BaseEnemy>();

    [HideInInspector]
    public string cardName = "Default";
    [HideInInspector]
    public string description = "";
    [HideInInspector]
    public int value = 1;


    protected bool doDamage = false;
    [HideInInspector]
    public DamageType targetType;  //public so controller knows if it should wait for the targets to be selected 
    [HideInInspector]
    public int numberOfTargets = 0;
    protected int damageAmount = 0;

    protected bool doDefense = false;
    protected bool defendPlayer = false;
    protected bool defendFighter = false;
    protected int negateAmount = 0;

    protected bool doSpawn = false;
    protected int spawnAmount = 0;
    protected bool fireWhenSpawned = false;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = cardData.image;
        cardName = cardData.cardName;
        description = cardData.description;
        value = cardData.value;
        doDamage = cardData.doDamage;
        targetType = cardData.targetType;
        numberOfTargets = cardData.numberOfTargets;
        damageAmount = cardData.damageAmount;
        doDefense = cardData.doDefense;
        defendPlayer = cardData.defendPlayer;
        defendFighter = cardData.defendFighter;
        negateAmount = cardData.negateAmount;
        doSpawn = cardData.doSpawn;
        spawnAmount = cardData.spawnAmount;
        fireWhenSpawned = cardData.fireWhenSpawned;
    }

	private void Update ()
	{
		Vector3 position = transform.position;
		Vector3 delta = MoveTowardsHere - position;
		transform.position = position + delta * Time.deltaTime * 2f;
	}

	public void SetTargets(List<BaseEnemy> targetList)
    {
        this.targetList = targetList;
    }
    public void ExecuteCard()
    {
		if ( doDamage )
		{
			switch ( targetType )
			{
				case DamageType.Random:
					foreach ( BaseEnemy enemy in GetRandomEnemies ( numberOfTargets ) ) enemy.TakeDamage ( damageAmount );
					break;
				case DamageType.Target:
					if ( targetList == null ) break;
					foreach ( BaseEnemy enemy in targetList ) enemy.TakeDamage ( damageAmount );
					break;
				case DamageType.All:
					foreach ( BaseFighter fighter in PlayerController.instance.AllFighters () ) fighter.TakeDamage ( damageAmount );
					foreach ( BaseEnemy enemy in EnemyController.instance.AllEnemies () ) enemy.TakeDamage ( damageAmount );
					PlayerController.instance.player.TakeDamage ( damageAmount );
					break;
				case DamageType.AllEnemies:
					foreach ( BaseEnemy enemy in EnemyController.instance.AllEnemies () ) enemy.TakeDamage ( damageAmount );
					break;
			}
		}

        if (doDefense)
        {
            if (defendFighter)
            {
                foreach(BaseFighter fighter in PlayerController.instance.fighters)
                {
                    if(fighter!=null) fighter.shield = negateAmount;
                }
            }
            if (defendPlayer)
            {
                PlayerController.instance.player.shield = negateAmount;
            }
        }

        if (doSpawn)
        {
            for(int i=0; i < spawnAmount; i++)
            {
                PlayerController.instance.SpawnFighter();
            }

        }
    }


	private List<BaseEnemy> GetRandomEnemies ( int amount )
	{
		return EnemyController.instance.RandomEnemies ( amount );
	}

    public void CardInit() {}
}

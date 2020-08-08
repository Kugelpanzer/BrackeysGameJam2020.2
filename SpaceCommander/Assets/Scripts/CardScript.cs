using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardScript : MonoBehaviour
{
	public Color PlayedColor = new Color ( 0.3f, 0.3f, 0.3f );
	public Color AvailableColor = Color.white;
	public Color PreviewDiscardColor = Color.red;
	public Color DiscardedColor1 = Color.red;
	public Color DiscardedColor2 = new Color ( 1f, 0f, 0f, 0f );
	private int _discardStep = 1;

	public Card cardData;
	public bool isPlayed = false;
	public bool isDiscarded = false;
	public bool isPreviewDiscard = false;
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

		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		if ( isDiscarded )
		{
			if ( _discardStep == 1 )
			{
				if ( SameColors ( renderer.color, DiscardedColor1 ) ) _discardStep++;
				else renderer.color = Color.Lerp ( renderer.color, DiscardedColor1, Time.deltaTime * 0.6f );
			}
			else
			{
				if ( SameColors ( renderer.color, DiscardedColor2 ) ) Destroy ( gameObject );
				else renderer.color = Color.Lerp ( renderer.color, DiscardedColor2, Time.deltaTime );
			}
		}
		else if ( isPreviewDiscard ) renderer.color = Color.Lerp ( renderer.color, PreviewDiscardColor, Time.deltaTime * 2f );
		else if ( isPlayed ) renderer.color = Color.Lerp ( renderer.color, PlayedColor, Time.deltaTime * 2f );
		else renderer.color = Color.Lerp ( renderer.color, AvailableColor, Time.deltaTime * 2f );
	}

	private static bool SameColors (Color color1, Color color2)
	{
		if ( Mathf.Abs ( color1.r - color2.r ) > 0.03f ) return false;
		if ( Mathf.Abs ( color1.g - color2.g ) > 0.03f ) return false;
		if ( Mathf.Abs ( color1.b - color2.b ) > 0.03f ) return false;
		if ( Mathf.Abs ( color1.a - color2.a ) > 0.01f ) return false;
		return true;
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

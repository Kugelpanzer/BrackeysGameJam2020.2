using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class BaseFighter : BaseDamagable
{
    public int damage;
    public int location;
	public int Longevity = 2;
	public TextMeshPro infoText;
	public virtual void Attack ()
	{
		BaseEnemy target = GetRandomEnemy ();
		if ( target != null )
		{
			target.TakeDamage ( damage );
		}
	}

	public void OnEndOfTurn ()
	{
		Longevity--;
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       infoText.text = "A:" + damage + " H:" + health +" S:"+shield;
	}

	private List<BaseEnemy> GetRandomEnemies ( int amount = 1 )
	{
		return EnemyController.instance.RandomEnemies ( amount );
	}

	private BaseEnemy GetRandomEnemy ()
	{
		List<BaseEnemy> fighters = GetRandomEnemies ( 1 );
		if ( fighters.Count == 0 ) return null;
		return fighters [0];
	}
}

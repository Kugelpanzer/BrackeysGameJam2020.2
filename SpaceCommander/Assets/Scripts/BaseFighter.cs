using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFighter : BaseDamagable
{
    public int damage;
    public int location;
	public int Longevity = 2;

    public virtual void Attack()
    {
        BaseEnemy target = GetRandomEnemies()[0];
        if (target != null)
        {
            target.TakeDamage(damage);
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
        
    }

	private List<BaseEnemy> GetRandomEnemies ( int amount = 1 )
	{
		return EnemyController.instance.RandomEnemies ( amount );
	}
}

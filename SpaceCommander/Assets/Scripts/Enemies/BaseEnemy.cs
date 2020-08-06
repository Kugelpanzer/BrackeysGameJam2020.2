using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseDamagable
{
    public int damage;

    public void Attack()
    {
		BaseFighter target = GetRandomFighters () [0];
		if ( target != null ) target.TakeDamage ( damage );
	}

	public void Upgrade ()
	{
		damage++;
	}

	private List<BaseFighter> GetRandomFighters ( int amount = 1 )
	{
		return PlayerController.instance.RandomFighters ( amount );
	}

	// Start is called before the first frame update
	void Start ()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

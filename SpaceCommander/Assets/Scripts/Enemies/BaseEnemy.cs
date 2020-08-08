﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BaseEnemy : BaseDamagable
{
    public int damage;
	public TextMeshPro infoText;
    public void Attack()
    {
		BaseFighter target = GetRandomFighter ();
		if ( target != null )
			target.TakeDamage ( damage );
		else
			PlayerController.instance.player.TakeDamage ( damage );
	}

	public void Upgrade ()
	{

			damage += EnemyController.instance.upgrade;


	}
    public override void Death()
    {
        base.Death();
    }
    private List<BaseFighter> GetRandomFighters ( int amount = 1 )
	{
		return PlayerController.instance.RandomFighters ( amount );
	}

	private BaseFighter GetRandomFighter ()
	{
		List<BaseFighter> fighters = GetRandomFighters ( 1 );
		if ( fighters.Count == 0 ) return null;
		return fighters [0];
	}

	// Start is called before the first frame update
	void Start ()
    {
		//Upgrade();
	}

    // Update is called once per frame
    void Update()
    {
		infoText.text = "A:" + damage + " H:" + health;
    }
}

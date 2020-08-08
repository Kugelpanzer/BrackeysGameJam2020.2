using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : BaseDamagable
{
	public static PlayerScript instance;


    public override void Death()
    {
       
    }
    private void Awake ()
	{
		if ( instance == null ) instance = this;
		else Destroy ( gameObject );
	}
}

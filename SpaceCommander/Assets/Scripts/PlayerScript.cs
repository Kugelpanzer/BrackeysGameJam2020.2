using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : BaseDamagable
{
	public static PlayerScript instance;

	private void Awake ()
	{
		if ( instance == null ) instance = this;
		else Destroy ( gameObject );
	}
}

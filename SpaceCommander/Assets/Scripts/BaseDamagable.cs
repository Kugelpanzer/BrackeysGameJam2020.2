using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDamagable : MonoBehaviour
{

    public int health;
    public int shield;

    public virtual void TakeDamage(int amount)
    {
        if (shield > 0)
        {
            shield--;
        }
        else
        {
            health -= amount;
            if (health <= 0)
            {
                Death();
            }
        }
    }
    public virtual void Death()
    {
        
        DestroyObject();
		// mora i kontroler da ga ukloni iz liste, al to se poziva spolja
    }

    public void DestroyObject()
    {
       Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

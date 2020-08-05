using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseDamagable
{

    /*
    znaci basic neprijatelj od statova ima:
        -damage
        -health
    od sopobnosti ima:
        -attack :aktivira se na PLAY
        -move( kretanje na susedne lanove): aktivira se na WIND
        -upgrade( povecava sebi damege) :aktivira se na REWIND
     */

    public int damage;

    public virtual void PlayReact()
    {

    }

    public virtual void WindReact()
    {

    }
    public virtual void RewindReact()
    {

    }

    public virtual void ExecuteBehavior(ActionEnum action)
    {
        switch (action)
        {
            case ActionEnum.Play:
                PlayReact();
                break;
            case ActionEnum.Rewind:
                WindReact();
                break;
            case ActionEnum.Wind:
                RewindReact();
                break;
        }
    }

    public virtual void Attack(BaseDamagable target)
    {
        target.TakeDamage(damage);
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

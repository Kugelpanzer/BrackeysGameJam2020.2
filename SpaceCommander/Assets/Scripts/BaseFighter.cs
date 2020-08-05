using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFighter : BaseDamagable
{

    public int damage;
    public int location;

    public virtual void Attack()
    {
        BaseEnemy target = GetRandomEnemies()[0];
        if (target != null)
        {
            target.TakeDamage(damage);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private List<BaseEnemy> GetRandomEnemies(int amount=1)
    {
        List<BaseEnemy> allEnemies = new List<BaseEnemy>();
        List<BaseEnemy> targetList = new List<BaseEnemy>();
        for (int i = 0; i < EnemyController.instance.enemyList.Length; i++)
        {
            if (EnemyController.instance.enemyList[i] != null)
            {
                allEnemies.Add(EnemyController.instance.enemyList[i]);
            }
        }
        for (int i = 0; i < amount; i++)
        {
            if (allEnemies.Count != 0)
            {
                int rand = Random.Range(0, allEnemies.Count);
                targetList.Add(allEnemies[rand]);
                allEnemies.RemoveAt(rand);
            }

        }

        return targetList ;
    }
}

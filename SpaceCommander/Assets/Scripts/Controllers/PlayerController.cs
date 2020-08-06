using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    public List<GameObject> fighterPositions = new List<GameObject>();// positions where fighters can spawn on screen
    private List<Card> allCards = new List<Card>();

    public GameObject fighterPrefab;

    public PlayerScript player;
    public BaseFighter[] fighters;

    public void SpawnFighter()
    {
        int index = FreeFighterSpace();
        if (index != -1)
        {
            GameObject currentFighter = Instantiate(fighterPrefab);

            fighters[index] = currentFighter.GetComponent<BaseFighter>();
            currentFighter.GetComponent<BaseFighter>().location = index;
            currentFighter.transform.position = fighterPositions[index].transform.position;
        }
        //fighters.Add(fighterPrefab.GetComponent<BaseFighter>());

    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        fighters = new BaseFighter[fighterPositions.Count];
    }


    public List<BaseFighter> AllFighters()
    {
        List<BaseFighter> allFighters=new List<BaseFighter>();
        for(int i= 0; i< fighters.Length; i++)
        {
            if (fighters[i] != null)
            {
                allFighters.Add(fighters[i]);
            }
        }
        return allFighters;
        
    }

    public int FreeFighterSpace()
    {
        int value = -1;
        for (int i = 0; i < fighters.Length; i++)
        {
            if (fighters[i] == null)
                value = i;

        }
        return value;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

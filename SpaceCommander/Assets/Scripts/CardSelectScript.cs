using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectScript : MonoBehaviour
{
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) )
        {
            CardScript cs = GetComponent<CardScript>();
            int index =PlayerController.instance.deck.IndexOf(cs);
            PlayerController.instance.Rewind(index);
            //PlayerController.instance.AddTarget(GetComponent<BaseEnemy>());
        }
    }
}

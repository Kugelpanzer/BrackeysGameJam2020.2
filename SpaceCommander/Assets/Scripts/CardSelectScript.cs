using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
public class CardSelectScript : MonoBehaviour
{
    Text descritpionText;

    private void Start()
    {
        GameObject go = GameObject.Find("CardEffectText");
        descritpionText = go.GetComponent<Text>();
    }

    private void OnMouseOver()
    {
        CardScript cs = GetComponent<CardScript>();
        descritpionText.text = cs.description;
        if (Input.GetMouseButtonDown(0) )
        {
            
            int index =PlayerController.instance.deck.IndexOf(cs);
            PlayerController.instance.Rewind(index);
            //PlayerController.instance.AddTarget(GetComponent<BaseEnemy>());
        }
    }
    private void OnMouseExit()
    {
        descritpionText.text = "";
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CardSelectScript : MonoBehaviour
{
    // descritpionText;
    TextMeshProUGUI descritpionText;
    private void Start()
    {
        GameObject go = GameObject.Find("CardText");
        descritpionText = go.GetComponent<TextMeshProUGUI>();
    }

    private void OnMouseOver()
    {
        CardScript cs = GetComponent<CardScript>();
        descritpionText.text = cs.description;
        if (Input.GetMouseButtonDown(0) )
        {
            int index =PlayerController.instance.deck.IndexOf(cs);
			PlayerController.instance.Rewind ( index );
        }
    }
    private void OnMouseExit()
    {
        descritpionText.text = "";
    }
}

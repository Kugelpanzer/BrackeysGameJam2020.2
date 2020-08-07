using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectScript : MonoBehaviour
{
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) )
        {
            PlayerController.instance.AddTarget(GetComponent<BaseEnemy>());
        }
    }
}

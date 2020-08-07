using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetingScript : MonoBehaviour
{

    GameObject targetPrefab;


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            Debug.Log("Klik");
        if (Input.GetMouseButtonDown(0) && PlayerController.instance.shouldTarget)
        {
            PlayerController.instance.AddTarget(GetComponent<BaseEnemy>());

        }
    }

}

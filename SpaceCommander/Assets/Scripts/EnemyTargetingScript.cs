using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetingScript : MonoBehaviour
{

    public GameObject targetPrefab;


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            Debug.Log("Klik");
        if (Input.GetMouseButtonDown(0) && PlayerController.instance.shouldTarget)
        {

            targetPrefab.SetActive(PlayerController.instance.CheckTarget(GetComponent<BaseEnemy>()));


        }
    }

}

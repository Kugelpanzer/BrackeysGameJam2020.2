using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class WheelScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool mouseOn;
    public bool hoverOn;
    public bool wheelSelected;
    void Update()
    {

        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);


        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);


        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        if (Input.GetMouseButton(0))
        {
            mouseOn = true;
        }
        else
        {
            mouseOn = false;
        }

        if (mouseOn && hoverOn)
            wheelSelected = true;
        if (!mouseOn && wheelSelected)
            wheelSelected = false;

        if(wheelSelected)
            transform.rotation = Quaternion.Euler(new Vector3( 0f, 0f,angle));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverOn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverOn = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CylinderManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    bool buttonPressed;
    public GameObject Cylinder;
    [SerializeField] private float ReturnSpeed;
    public string Direction;


    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }

    void Update()
    {
        if (buttonPressed)
        {

            if (Direction == "Left")
            {
                Cylinder.transform.Rotate(0, -ReturnSpeed * Time.deltaTime, 0, Space.Self);
            }
            else
            {
                Cylinder.transform.Rotate(0, ReturnSpeed * Time.deltaTime, 0, Space.Self);
            }

        }
    }



}

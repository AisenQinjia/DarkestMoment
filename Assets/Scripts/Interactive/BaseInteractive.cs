using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseInteractive : MonoBehaviour, IPointerClickHandler
{

    public virtual void InteractiveLogic(Transform player)
    {

    }


    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {

    }
}

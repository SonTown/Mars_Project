using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfoButton : MonoBehaviour,IPointerUpHandler
{
    public void OnPointerUp(PointerEventData eventData)
    {
        GameManager.Instance.GetCharacterManager().GetCharacterController().InfoActivate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltip;

    // Start is called before the first frame update
    void Start()
    {
        tooltip.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        tooltip.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        tooltip.SetActive(false);
    }
}

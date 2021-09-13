using UnityEngine;
using UnityEngine.EventSystems;

public class TapArea : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown (PointerEventData eventData)
    {
        GameManager.Instance.CollectByTap (eventData.position, transform);
    }

    void start() 
    {

    }

    void update()
    {
    
    }

}
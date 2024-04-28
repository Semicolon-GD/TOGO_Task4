using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour , IDragHandler,IPointerDownHandler
{
    private float _horizontal;
    private bool _firstClick = true;
    
    public delegate void Action();
    public delegate void Action2(float horizontal);
    public static event Action OnFirstClick;
    public static event Action2 Dragging;
    
    public void OnDrag(PointerEventData eventData)
    {
        _horizontal= eventData.delta.x;
        Dragging?.Invoke(_horizontal);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_firstClick)
        {
            OnFirstClick?.Invoke();
            _firstClick = false;
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform _background;
    [SerializeField] private RectTransform _stick;

    public Vector2 InputVector { get; private set; } = Vector2.zero;

    public void OnPointerDown(PointerEventData eventData) => OnDrag(eventData);

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_stick != null)
            _stick.anchoredPosition = Vector2.zero;

        InputVector = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _background,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
        );

        localPoint = Vector2.ClampMagnitude(localPoint, 100f);
        InputVector = localPoint / 100f;
        _stick.anchoredPosition = localPoint;
    }
}

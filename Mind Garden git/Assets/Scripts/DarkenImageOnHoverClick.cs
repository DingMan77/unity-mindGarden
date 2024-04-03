using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DarkenImageOnHoverClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image image;
    private Color originalColor;

    [SerializeField] private Color hoverColor = Color.white;
    [SerializeField] private Color clickColor = Color.red;

    void Start()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = originalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        image.color = clickColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        image.color = hoverColor;
    }
}

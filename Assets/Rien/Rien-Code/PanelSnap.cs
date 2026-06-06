using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PanelSnap : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public RectTransform content;
    public RectTransform[] panels;

    public float snapSpeed = 10f;

    private int targetPanel;
    private bool isDragging;

    void Update()
    {
        if (!isDragging)
        {
            Vector2 targetPos = new Vector2(
                -panels[targetPanel].anchoredPosition.x,
                content.anchoredPosition.y
            );

            content.anchoredPosition = Vector2.Lerp(
                content.anchoredPosition,
                targetPos,
                Time.deltaTime * snapSpeed
            );
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < panels.Length; i++)
        {
            float distance = Mathf.Abs(
                content.anchoredPosition.x +
                panels[i].anchoredPosition.x
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetPanel = i;
            }
        }
    }
}
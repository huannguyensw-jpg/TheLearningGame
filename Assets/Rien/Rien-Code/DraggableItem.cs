using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Item Settings")]
    public bool isCorrectItem; // Tích chọn nếu vật phẩm này đúng yêu cầu câu hỏi

    private SortingManager manager;
    private Vector3 originalPosition;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private bool isShaking = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        // Tự động thêm CanvasGroup nếu chưa có để xử lý xuyên thấu raycast khi kéo
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
        
        manager = FindObjectOfType<SortingManager>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isShaking) return;
        canvasGroup.alpha = 0.6f; // Làm mờ nhẹ khi đang kéo cho có cảm giác mượt
        canvasGroup.blocksRaycasts = false; // Bỏ chặn raycast để hệ thống nhận diện được giỏ phía dưới
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isShaking) return;
        // Di chuyển vật phẩm theo tọa độ chuột/tay cảm ứng
        rectTransform.anchoredPosition += eventData.delta / manager.mainCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Kiểm tra xem vị trí thả có nằm trong Giỏ (Basket) không
        if (RectTransformUtility.RectangleContainsScreenPoint(manager.basketRect, Input.mousePosition, null))
        {
            manager.CheckDroppedItem(this);
        }
        else
        {
            // Thả ra ngoài giỏ -> Tự động bay về vị trí cũ
            rectTransform.anchoredPosition = originalPosition;
        }
    }

    // Hàm đưa vật phẩm về vị trí cũ và kích hoạt hiệu ứng rung
    public void ReturnToOldPositionWithPenalty()
    {
        rectTransform.anchoredPosition = originalPosition;
        if (!isShaking)
        {
            StartCoroutine(ShakeCoroutine());
        }
    }

    private IEnumerator ShakeCoroutine()
    {
        isShaking = true;
        Vector3 pos = rectTransform.anchoredPosition;
        float duration = 0.3f;
        float elapsed = 0f;
        float magnitude = 15f; // Độ mạnh của hiệu ứng rung

        while (elapsed < duration)
        {
            float x = pos.x + Random.Range(-1f, 1f) * magnitude;
            rectTransform.anchoredPosition = new Vector2(x, pos.y);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = pos;
        isShaking = false;
    }
}
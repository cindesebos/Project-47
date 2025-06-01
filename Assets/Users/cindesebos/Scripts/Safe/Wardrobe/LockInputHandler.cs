using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Safe.Wardrobe
{
    public class LockInputHandler : MonoBehaviour, IPointerClickHandler
    {
        public Camera renderCamera;
        public RawImage targetRawImage;
        public LayerMask layer;

        public void OnPointerClick(PointerEventData eventData)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                targetRawImage.rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out localPoint
            );

            Rect rect = targetRawImage.rectTransform.rect;
            Vector2 uv = new Vector2(
                (localPoint.x - rect.x) / rect.width,
                (localPoint.y - rect.y) / rect.height
            );

            Vector2 pixelPoint = new Vector2(
                uv.x * renderCamera.pixelWidth,
                uv.y * renderCamera.pixelHeight
            );

            Ray ray = renderCamera.ScreenPointToRay(pixelPoint);

            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 2f);
            Debug.Log("Has ray");

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layer))
            {
                Debug.Log($"Hit: {hit.collider.name}");

                LockPinRotator pin = hit.collider.GetComponent<LockPinRotator>();
                if (pin != null) pin.Rotate();
            }
        }
    }
}
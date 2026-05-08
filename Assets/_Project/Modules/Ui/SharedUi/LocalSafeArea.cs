using UnityEngine;

namespace GameKit.SharedUi
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public sealed class LocalSafeArea : MonoBehaviour
    {
        private const float k_RectComparisonEpsilon = 0.5f;

        [SerializeField] private bool conformTop = true;
        [SerializeField] private bool conformBottom = true;
        [SerializeField] private bool conformLeft;
        [SerializeField] private bool conformRight;

        private readonly Vector3[] m_worldCorners = new Vector3[4];

        private Camera m_canvasCamera;
        private RectTransform m_rectTransform;
        private RectTransform m_parentRectTransform;
        private Rect m_lastSafeArea = new (0f, 0f, 0f, 0f);
        private Rect m_lastParentScreenRect = new (0f, 0f, 0f, 0f);
        private Vector2Int m_lastScreenSize = new (0, 0);

        private void Awake()
        {
            m_rectTransform = GetComponent<RectTransform>();
            m_canvasCamera = GetComponentInParent<Canvas>().worldCamera;
        }

        private void OnEnable()
        {
            CacheParentRectTransform();
            Refresh();
        }

        private void Update()
        {
            if (!NeedsRefresh())
            {
                return;
            }

            Refresh();
        }

        private void CacheParentRectTransform()
        {
            m_parentRectTransform = m_rectTransform.parent as RectTransform;
        }

        private void Refresh()
        {
            if (!m_parentRectTransform)
            {
                CacheParentRectTransform();
                if (!m_parentRectTransform)
                {
                    Debug.LogError($"Cannot apply local safe area on {name}: parent RectTransform is missing.", this);
                    enabled = false;
                    return;
                }
            }

            var safeArea = Screen.safeArea;
            var parentScreenRect = GetScreenRect(m_parentRectTransform);

            if (!HasStateChanged(safeArea, parentScreenRect))
            {
                return;
            }

            m_lastSafeArea = safeArea;
            m_lastParentScreenRect = parentScreenRect;
            m_lastScreenSize = new Vector2Int(Screen.width, Screen.height);

            ApplySafeArea(safeArea, parentScreenRect);
            m_parentRectTransform.hasChanged = false;
        }

        private bool NeedsRefresh()
        {
            if (!m_parentRectTransform)
            {
                return true;
            }

            return Screen.width != m_lastScreenSize.x
                || Screen.height != m_lastScreenSize.y
                || !Approximately(m_lastSafeArea, Screen.safeArea)
                || m_parentRectTransform.hasChanged;
        }

        private bool HasStateChanged(Rect safeArea, Rect parentScreenRect)
        {
            return !Approximately(m_lastSafeArea, safeArea)
                || !Approximately(m_lastParentScreenRect, parentScreenRect)
                || Screen.width != m_lastScreenSize.x
                || Screen.height != m_lastScreenSize.y;
        }

        private void ApplySafeArea(Rect safeArea, Rect parentScreenRect)
        {
            if (parentScreenRect.width <= 0f || parentScreenRect.height <= 0f)
            {
                return;
            }

            var localSafeArea = Intersect(parentScreenRect, safeArea);

            if (!conformLeft)
            {
                localSafeArea.xMin = parentScreenRect.xMin;
            }

            if (!conformRight)
            {
                localSafeArea.xMax = parentScreenRect.xMax;
            }

            if (!conformBottom)
            {
                localSafeArea.yMin = parentScreenRect.yMin;
            }

            if (!conformTop)
            {
                localSafeArea.yMax = parentScreenRect.yMax;
            }

            localSafeArea = ClampToParent(localSafeArea, parentScreenRect);

            var anchorMin = new Vector2(
                Mathf.InverseLerp(parentScreenRect.xMin, parentScreenRect.xMax, localSafeArea.xMin),
                Mathf.InverseLerp(parentScreenRect.yMin, parentScreenRect.yMax, localSafeArea.yMin));
            var anchorMax = new Vector2(
                Mathf.InverseLerp(parentScreenRect.xMin, parentScreenRect.xMax, localSafeArea.xMax),
                Mathf.InverseLerp(parentScreenRect.yMin, parentScreenRect.yMax, localSafeArea.yMax));

            if (anchorMin.x < 0f || anchorMin.y < 0f || anchorMax.x < 0f || anchorMax.y < 0f)
            {
                return;
            }

            m_rectTransform.anchorMin = anchorMin;
            m_rectTransform.anchorMax = anchorMax;
            m_rectTransform.offsetMin = Vector2.zero;
            m_rectTransform.offsetMax = Vector2.zero;
        }

        private static Rect ClampToParent(Rect rect, Rect parentRect)
        {
            rect.xMin = Mathf.Clamp(rect.xMin, parentRect.xMin, parentRect.xMax);
            rect.xMax = Mathf.Clamp(rect.xMax, parentRect.xMin, parentRect.xMax);
            rect.yMin = Mathf.Clamp(rect.yMin, parentRect.yMin, parentRect.yMax);
            rect.yMax = Mathf.Clamp(rect.yMax, parentRect.yMin, parentRect.yMax);
            return rect;
        }

        private Rect GetScreenRect(RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(m_worldCorners);
            var min = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
            var max = new Vector2(float.NegativeInfinity, float.NegativeInfinity);

            foreach (var worldCorner in m_worldCorners)
            {
                var screenPoint = RectTransformUtility.WorldToScreenPoint(m_canvasCamera, worldCorner);
                min = Vector2.Min(min, screenPoint);
                max = Vector2.Max(max, screenPoint);
            }

            return Rect.MinMaxRect(min.x, min.y, max.x, max.y);
        }

        private static Rect Intersect(Rect a, Rect b)
        {
            var xMin = Mathf.Max(a.xMin, b.xMin);
            var yMin = Mathf.Max(a.yMin, b.yMin);
            var xMax = Mathf.Min(a.xMax, b.xMax);
            var yMax = Mathf.Min(a.yMax, b.yMax);

            if (xMax < xMin || yMax < yMin)
            {
                return Rect.MinMaxRect(xMin, yMin, xMin, yMin);
            }

            return Rect.MinMaxRect(xMin, yMin, xMax, yMax);
        }

        private static bool Approximately(Rect a, Rect b)
        {
            return Mathf.Abs(a.x - b.x) < k_RectComparisonEpsilon
                && Mathf.Abs(a.y - b.y) < k_RectComparisonEpsilon
                && Mathf.Abs(a.width - b.width) < k_RectComparisonEpsilon
                && Mathf.Abs(a.height - b.height) < k_RectComparisonEpsilon;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace GameKit.SharedUi
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(LayoutElement))]
    public sealed class SafeAreaVerticalInset : MonoBehaviour
    {
        private const float k_RectComparisonEpsilon = 0.5f;
        private const float k_HeightComparisonEpsilon = 0.01f;

        // ReSharper disable once UnusedMember.Local
        private enum InsetEdge
        {
            Top,
            Bottom
        }

        [SerializeField] private InsetEdge edge = InsetEdge.Top;

        private readonly Vector3[] m_worldCorners = new Vector3[4];

        private Camera m_canvasCamera;
        private RectTransform m_rectTransform;
        private RectTransform m_parentRectTransform;
        private LayoutElement m_layoutElement;
        private Rect m_lastSafeArea = new (0f, 0f, 0f, 0f);
        private Rect m_lastParentScreenRect = new (0f, 0f, 0f, 0f);
        private Vector2Int m_lastScreenSize = new (0, 0);
        private float m_lastAppliedInset;
        private bool m_isInsetApplied;
        private float m_originalPreferredHeight;
        private float m_basePreferredHeight;

        private void Awake()
        {
            m_rectTransform = GetComponent<RectTransform>();
            m_layoutElement = GetComponent<LayoutElement>();
            m_canvasCamera = GetComponentInParent<Canvas>().worldCamera;
        }

        private void OnEnable()
        {
            CacheParentRectTransform();
            CacheBaseHeights();
            Refresh();
        }

        private void OnDisable()
        {
            m_layoutElement.preferredHeight = m_originalPreferredHeight;
            m_isInsetApplied = false;
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

        private void CacheBaseHeights()
        {
            m_originalPreferredHeight = m_layoutElement.preferredHeight;
            m_basePreferredHeight = m_originalPreferredHeight >= 0f ? m_originalPreferredHeight : m_rectTransform.rect.height;
        }

        private void Refresh()
        {
            if (!m_parentRectTransform)
            {
                CacheParentRectTransform();
                if (!m_parentRectTransform)
                {
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

            ApplyInset(safeArea, parentScreenRect);
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

        private void ApplyInset(Rect safeArea, Rect parentScreenRect)
        {
            if (parentScreenRect.width <= 0f || parentScreenRect.height <= 0f || m_parentRectTransform.rect.height <= 0f)
            {
                return;
            }

            var localSafeArea = Intersect(parentScreenRect, safeArea);
            var screenInset = edge == InsetEdge.Top
                ? Mathf.Max(0f, parentScreenRect.yMax - localSafeArea.yMax)
                : Mathf.Max(0f, localSafeArea.yMin - parentScreenRect.yMin);

            var localInset = screenInset * (m_parentRectTransform.rect.height / parentScreenRect.height);

            if (m_isInsetApplied && Mathf.Abs(localInset - m_lastAppliedInset) < k_HeightComparisonEpsilon)
            {
                return;
            }

            m_lastAppliedInset = localInset;
            m_isInsetApplied = true;

            m_layoutElement.preferredHeight = m_basePreferredHeight + localInset;

            LayoutRebuilder.MarkLayoutForRebuild(m_rectTransform);
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

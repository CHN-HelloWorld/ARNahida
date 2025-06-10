using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace UnityEngine.XR.ARFoundation.Samples
{
    /// <summary>
    /// 监听触摸事件，并从屏幕触摸点进行AR射线投射。
    /// AR射线投射只会命中检测到的可追踪对象，如特征点和平面。
    ///
    /// 如果射线投射命中一个平面，且场景中没有标签为 "NXD" 的物体，则实例化 <see cref="placedPrefab"/>；
    /// 否则，将场景中标签为 "NXD" 的游戏对象移动到命中位置。
    /// </summary>
    [RequireComponent(typeof(ARRaycastManager))]
    public class PlaceOnPlane : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("在触摸位置的平面上实例化这个预制体。")]
        GameObject m_PlacedPrefab;

        public Pattern pattern;

        /// <summary>
        /// 触摸时要实例化的预制体。
        /// </summary>
        public GameObject placedPrefab
        {
            get { return m_PlacedPrefab; }
            set { m_PlacedPrefab = value; }
        }

        /// <summary>
        /// 当前操作的游戏对象，无论是新实例化的还是已存在的。
        /// </summary>
        public GameObject spawnedObject { get; private set; }

        /// <summary>
        /// 初始化ARRaycastManager。
        /// </summary>
        void Awake()
        {
            m_RaycastManager = GetComponent<ARRaycastManager>();
        }

        /// <summary>
        /// 尝试获取触摸位置。
        /// </summary>
        /// <param name="touchPosition">如果有触摸事件，输出触摸位置。</param>
        /// <returns>如果有触摸事件，返回true；否则返回false。</returns>
        bool TryGetTouchPosition(out Vector2 touchPosition)
        {
            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
                return true;
            }

            touchPosition = default;
            return false;
        }

        /// <summary>
        /// 每帧更新，检查触摸事件并进行AR射线投射。
        /// </summary>
        void Update()
        {
            if (pattern.getIsManualMove() || pattern.getIsFollowing())
                return;
            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;

            // 防止在UI上点击时触发AR射线投射
            if (IsPointerOverUI(touchPosition))
                return;

            if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                // 射线投射命中按距离排序，第一个是最接近的命中
                var hitPose = s_Hits[0].pose;

                // 查找场景中标签为 "NXD" 的物体
                GameObject[] nxdObjects = GameObject.FindGameObjectsWithTag("NXD");

                if (nxdObjects.Length == 0)
                {
                    // 场景中没有 "NXD" 物体，实例化新的预制体
                    spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                    spawnedObject.tag = "NXD"; // 确保新实例化的物体有 "NXD" 标签
                }
                else
                {
                    // 场景中有 "NXD" 物体，移动第一个找到的物体
                    spawnedObject = nxdObjects[0];
                    spawnedObject.transform.position = hitPose.position;
                }
            }
        }

        /// <summary>
        /// 检测触摸点是否在UI上。
        /// </summary>
        /// <param name="touchPosition">触摸位置。</param>
        /// <returns>如果触摸点在UI上，返回true；否则返回false。</returns>
        bool IsPointerOverUI(Vector2 touchPosition)
        {
            if (EventSystem.current == null)
                return false;

            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = touchPosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }

        /// <summary>
        /// 存储AR射线投射命中结果的列表。
        /// </summary>
        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        /// <summary>
        /// ARRaycastManager组件的引用。
        /// </summary>
        ARRaycastManager m_RaycastManager;
    }
}
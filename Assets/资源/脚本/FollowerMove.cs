using UnityEngine;
using System.Collections.Generic;

// 跟随移动脚本
public class FollowerMove : MonoBehaviour
{
    public Pattern Pattern;
    public InputAttribute InputAttribute;

    public Transform cameraTransform;   // 摄像机的 Transform，用于记录移动路径
    public PlayerMove playerMove;       // PlayerMove 脚本的引用，用于控制角色移动
    public float recordInterval = 0.1f; // 记录路径点的时间间隔
    public float positionThreshold = 0.1f; // 位置变化阈值，只有当摄像机移动超过此阈值时才记录新路径点
    public float smoothTime = 0.2f;     // 平滑时间，用于平滑调整 vertical 和 compensationFactor
    public float radiusThreshold = 0.5f; // 检测路径点的半径阈值，当角色进入此半径内时，移除该路径点
    public float ySmoothTime = 0.2f;    // Y 轴平滑移动的时间

    public List<Vector3> pathPoints = new List<Vector3>(); // 路径点列表
    private float timer = 0f;           // 计时器，用于控制记录路径点的频率
    private Vector3 lastPosition;       // 上一次记录的位置

    private float currentVertical = 0f;         // 当前平滑的 vertical 值
    private float currentCompensationFactor = 0f; // 当前平滑的 compensationFactor 值
    private float verticalVelocity = 0f;        // vertical 平滑速度
    private float compensationVelocity = 0f;    // compensationFactor 平滑速度

    private float initialYDifference;   // 初始 Y 轴差值
    private float currentYVelocity = 0f; // Y 轴平滑速度

    void Start()
    {
        Pattern = GameObject.Find("公共").GetComponent<Pattern>();
        InputAttribute = GameObject.Find("公共").GetComponent<InputAttribute>();
        cameraTransform = Camera.main.transform;

        lastPosition = cameraTransform.position;
        // 计算初始 Y 轴差值
        initialYDifference = transform.position.y - cameraTransform.position.y;
    }

    void Update()
    {
        if (Pattern.getIsFollowing())
        {
            RecordPathPoints();
            NavigatePathPoints();
            FollowYPosition();
        }
        else
        {
            // 停止跟随时，清空路径点并停止移动
            pathPoints.Clear();
            InputAttribute.setVertical(0f);
            InputAttribute.setHorizontal(0f);
            InputAttribute.setCompensationFactor(0f);
        }
    }

    /// <summary>
    /// 记录摄像机路径点
    /// </summary>
    private void RecordPathPoints()
    {
        if (!Pattern.getIsRecordPath())
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer < recordInterval) return;

        timer = 0f;
        Vector3 currentPosition = cameraTransform.position;
        if (Vector3.Distance(currentPosition, lastPosition) > positionThreshold)
        {
            pathPoints.Add(currentPosition);
            lastPosition = currentPosition;
        }
    }

    /// <summary>
    /// 导航到路径点
    /// </summary>
    private void NavigatePathPoints()
    {
        if (pathPoints.Count == 0) return;

        Vector3 nextPoint = pathPoints[0];
        Vector3 direction = (nextPoint - transform.position).normalized;
        direction.y = 0;

        float distance = Vector3.Distance(
            new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(nextPoint.x, 0, nextPoint.z)
        );

        UpdateInputValues(direction, distance);

        if (distance < radiusThreshold)
        {
            pathPoints.RemoveAt(0);
        }
    }

    /// <summary>
    /// 更新输入值和角色控制参数
    /// </summary>
    private void UpdateInputValues(Vector3 direction, float distance)
    {
        float remainingPath = CalculateRemainingPath();
        (float targetVertical, float targetCompensationFactor) = GetTargetValues(remainingPath);

        // 平滑过渡
        currentVertical = Mathf.SmoothDamp(currentVertical, targetVertical, ref verticalVelocity, smoothTime);
        currentCompensationFactor = Mathf.SmoothDamp(currentCompensationFactor, targetCompensationFactor, ref compensationVelocity, smoothTime);

        // 设置输入值
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        InputAttribute.setHorizontal(0f);
        InputAttribute.setVertical(currentVertical);
        InputAttribute.setRotationTarget(targetRotation);
        InputAttribute.setCompensationFactor(currentCompensationFactor);
    }

    /// <summary>
    /// 根据剩余路径获取目标 vertical 和 compensationFactor
    /// </summary>
    private (float vertical, float compensationFactor) GetTargetValues(float remainingPath)
    {
        if (remainingPath < 1f)
            return (0f, 0f);
        if (remainingPath < 2f)
            return (1f, 0.5f);
        if (remainingPath < 6f)
            return (1f, 0.7f);
        return (1f, 2f);
    }

    /// <summary>
    /// 计算剩余路径长度（XZ 平面）
    /// </summary>
    private float CalculateRemainingPath()
    {
        float remaining = 0f;
        Vector3 currentPos = new Vector3(transform.position.x, 0, transform.position.z);
        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            Vector3 pointA = new Vector3(pathPoints[i].x, 0, pathPoints[i].z);
            Vector3 pointB = new Vector3(pathPoints[i + 1].x, 0, pathPoints[i + 1].z);
            remaining += Vector3.Distance(pointA, pointB);
        }
        if (pathPoints.Count > 0)
        {
            Vector3 firstPoint = new Vector3(pathPoints[0].x, 0, pathPoints[0].z);
            remaining += Vector3.Distance(currentPos, firstPoint);
        }
        return remaining;
    }

    /// <summary>
    /// 跟随 Y 轴位置
    /// </summary>
    private void FollowYPosition()
    {
        // 计算目标 Y 位置，保持与摄像机的初始 Y 轴距离
        float targetY = cameraTransform.position.y + initialYDifference;
        // 使用 SmoothDamp 平滑调整 Y 位置
        float newY = Mathf.SmoothDamp(transform.position.y, targetY, ref currentYVelocity, ySmoothTime);
        // 更新角色的位置，仅修改 Y 轴
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    /// <summary>
    /// 设置是否记录路径点
    /// </summary>
    public void SetRecordPath(bool record)
    {
        if (!record)
        {
            timer = 0f; // 重置计时器
        }
    }

    /// <summary>
    /// 更新路径点位置
    /// </summary>
    public void UpdatePathPoints(Vector3 delta)
    {
        for (int i = 0; i < pathPoints.Count; i++)
        {
            pathPoints[i] += delta;
        }
        lastPosition += delta;
    }
}
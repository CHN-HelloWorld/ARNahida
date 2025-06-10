using UnityEngine;

//朝向或取消朝向
public class LookAt : ED
{
    public Pattern Pattern;

    [SerializeField] private GameObject targetObject; // 要朝向摄像机的游戏对象
    [SerializeField] private float rotationDuration = 0.5f; // 插值旋转持续时间（秒）
    private Camera mainCamera;
    private Quaternion targetRotation;
    private float rotationTime;

    void Start()
    {
        SwitchStatus();
        // 获取主摄像机
        mainCamera = Camera.main;
    }

    public void Run()
    {
        //处于手动移动模式期间禁止朝向摄像机
        if (!Pattern.getIsManualMove())
        {
            SwitchStatus();
            Pattern.setIsLookingAt(!Pattern.getIsLookingAt());
        }

    }
    void Update()
    {
        if (targetObject == null)
        {
            targetObject = GameObject.FindGameObjectWithTag("NXD");
        }
        else
        {
            //处于手动模式并且启用朝向摄像机时，禁用朝向摄像机
            if (Pattern.getIsManualMove() && Pattern.getIsLookingAt())
            {
                SwitchStatus();
                Pattern.setIsLookingAt(!Pattern.getIsLookingAt());

            }
            else
            {
                if (Pattern.getIsLookingAt())
                {
                    if (targetObject != null && mainCamera != null)
                    {
                        // 计算目标对象朝向摄像机的方向
                        Vector3 directionToCamera = mainCamera.transform.position - targetObject.transform.position;
                        directionToCamera.y = 0; // 仅考虑XZ平面，忽略Y轴高度差

                        if (directionToCamera != Vector3.zero)
                        {
                            // 计算目标旋转（仅Y轴）
                            targetRotation = Quaternion.LookRotation(directionToCamera);
                            rotationTime = 0f;
                        }
                    }

                    // 插值旋转
                    rotationTime += Time.deltaTime;
                    float t = Mathf.Clamp01(rotationTime / rotationDuration);
                    Quaternion currentRotation = targetObject.transform.rotation;
                    // 仅插值Y轴旋转
                    float targetY = targetRotation.eulerAngles.y;
                    float currentY = currentRotation.eulerAngles.y;
                    float newY = Mathf.LerpAngle(currentY, targetY, t);
                    targetObject.transform.rotation = Quaternion.Euler(currentRotation.eulerAngles.x, newY, currentRotation.eulerAngles.z);
                }
            }
        }
    }
}
using UnityEngine;

// 横竖屏自适应
public class UISwitch : MonoBehaviour
{
    public UIDH uiDH;
    public GameObject landscapeCanvas; // 横屏画布
    public GameObject portraitCanvas; // 竖屏画布

    void Start()
    {
        // 如果支持陀螺仪，则启用
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
        CheckOrientation();
    }

    void Update()
    {
        // 每帧检查屏幕方向
        CheckOrientation();
    }

    // 切换画布状态
    void CheckOrientation()
    {
        bool isLandscape;

        // 如果支持陀螺仪且已启用，则使用陀螺仪
        if (SystemInfo.supportsGyroscope && Input.gyro.enabled)
        {
            Vector3 gravity = Input.gyro.gravity;
            // 如果Y轴重力占主导，则为竖屏；否则为横屏
            isLandscape = Mathf.Abs(gravity.y) <= Mathf.Abs(gravity.x);
        }
        else
        {
            // 如果陀螺仪不可用，则根据屏幕宽高比判断
            isLandscape = Screen.width > Screen.height;
        }

        // 根据屏幕方向和uiDH.show激活画布
        if (uiDH.show)
        {
            landscapeCanvas.SetActive(isLandscape);
            portraitCanvas.SetActive(!isLandscape);
        }
        else
        {
            landscapeCanvas.SetActive(false);
            portraitCanvas.SetActive(false);
        }
    }
}
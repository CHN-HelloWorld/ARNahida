using UnityEngine;


/// <summary>
/// 输入控制
/// </summary>
public class InputAttribute : MonoBehaviour
{
    public Pattern Pattern;

    private ETCJoystick joystick;        // EasyTouch5 的摇杆组件引用

    //手动移动速度倍率，默认1
    public float speed = 1f;

    //补偿因子
    public float compensationFactor = 1f;

    //输出幅度
    public float inputMagnitude;

    //水平轴输入
    public float horizontal = 0f;

    //垂直轴输入
    public float vertical = 0f;

    //旋转目标
    private Quaternion RotationTarget;

    /// <summary>
    /// 寻找摇杆组件
    /// </summary>
    private void FindJoystickDelayed()
    {
        GameObject uiRoot = GameObject.Find("UI");
        Transform horizontalScreen = uiRoot.transform.Find("横屏");
        Transform control = horizontalScreen.Find("操纵");
        Transform joystickTransform = control.Find("摇杆-移动");
        joystick = joystickTransform.GetComponent<ETCJoystick>();
    }

    private void Start()
    {
        FindJoystickDelayed();
    }

    /// <summary>
    /// 获取手动移动速度倍率
    /// </summary>
    public float getSpeed()
    {
        return speed;
    }

    /// <summary>
    /// 设置手动移动速度倍率
    /// </summary>
    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    /// <summary>
    /// 获取水平轴输入
    /// </summary>
    public float getHorizontal()
    {
        //跟随模式下返回模拟输入，移动模式下返回实际输入
        if (Pattern.getIsFollowing())
        {
            return horizontal;
        }
        else
        {
            // return joystick.axisX.axisValue; 前后反，左右没反应
            return -joystick.axisY.axisValue;
        }
    }

    /// <summary>
    /// 设置水平轴输入
    /// </summary>
    /// <param name="horizontal"></param>
    public void setHorizontal(float horizontal)
    {
        this.horizontal = horizontal;
    }

    /// <summary>
    /// 获取垂直轴输入
    /// </summary>
    public float getVertical()
    {
        //跟随模式下返回模拟输入，移动模式下返回实际输入
        if (Pattern.getIsFollowing())
        {
            return vertical;
        }
        else
        {
            // return joystick.axisY.axisValue;前后反，左右没反应
            return -joystick.axisX.axisValue;
        }
    }

    /// <summary>
    /// 设置垂直轴输入
    /// </summary>
    /// <param name="vertical"></param>
    public void setVertical(float vertical)
    {
        this.vertical = vertical;
    }

    ///  <summary>
    /// 获取补偿因子
    /// </summary>
    /// <returns></returns>
    public float getCompensationFactor()
    {
        return Pattern.getIsFollowing() ? compensationFactor : this.speed;
    }

    ///  <summary>
    /// 设置补偿因子
    /// </summary>
    public void setCompensationFactor(float compensationFactor)
    {
        this.compensationFactor = compensationFactor;
    }

    ///  <summary>
    /// 获取输出幅度
    /// </summary>
    public float getInputMagnitude()
    {
        return Mathf.Clamp01(Mathf.Sqrt(getHorizontal() * getHorizontal() + getVertical() * getVertical()));
    }


    ///<summary>
    /// 设置输出幅度
    /// </summary>    
    public void setInputMagnitude(float inputMagnitude)
    {
        this.inputMagnitude = inputMagnitude;
    }

    /// <summary>
    /// 获取旋转目标
    /// </summary>
    public Quaternion getRotationTarget()
    {
        return RotationTarget;
    }

    /// <summary>
    /// 设置旋转目标
    /// </summary>
    public void setRotationTarget(Quaternion RotationTarget)
    {
        this.RotationTarget = RotationTarget;
    }
}

using UnityEngine;

/// <summary> 
/// 模式控制类，用于控制各种模式开关 
/// </summary> 
public class Pattern : MonoBehaviour
{
    // 是否启用动画移动，控制物体是否通过动画进行位移 
    public bool isAnimMove = true;

    // 是否跟随目标，决定物体是否会追踪某个目标对象 
    public bool isFollowing = false;

    // 是否处于手动移动模式，具有最高优先级，手动移动模式禁止跟随模式、朝向摄像机
    public bool isManualMove = false;

    // 是否锁定Y轴位置，控制物体在Y轴上的移动限制，跟随模式不锁定，手动操纵锁定
    public bool isLockYPosition = false;

    // 是否朝向摄像机，决定物体是否始终面向摄像机 
    public bool isLookingAt = false;

    // 是否使用默认手动移动速度倍率，控制手动移动时的速度是否为默认值 
    public bool isDefaultSpeed = true;

    //是否相对移动
    public bool isRelativeMove = false;

    //是否启用识别
    public bool isRecognition = true;

    //是否记录路径点
    // public bool isRecordPath = false;

    /// <summary> 
    /// 获取是否启用动画移动的状态 
    /// </summary> 
    /// <returns>返回一个布尔值，表示是否启用动画移动</returns> 
    public bool getIsAnimMove()
    {
        return isAnimMove;
    }

    /// <summary> 
    /// 设置是否启用动画移动的状态 
    /// </summary> 
    /// <param name="isAnimMove">一个布尔值，用于设置是否启用动画移动</param> 
    public void setIsAnimMove(bool isAnimMove)
    {
        this.isAnimMove = isAnimMove;
    }

    /// <summary> 
    /// 获取是否跟随目标的状态 
    /// </summary> 
    /// <returns>返回一个布尔值，表示是否跟随目标</returns> 
    public bool getIsFollowing()
    {
        return isFollowing;
    }

    /// <summary> 
    /// 设置是否跟随目标的状态 
    /// </summary> 
    /// <param name="isFollowing">一个布尔值，用于设置是否跟随目标</param> 
    public void setIsFollowing(bool isFollowing)
    {
        this.isFollowing = isFollowing;
    }

    /// <summary> 
    /// 获取是否锁定Y轴位置的状态 
    /// </summary> 
    /// <returns>返回一个布尔值，表示是否锁定Y轴位置</returns> 
    public bool getIsLockYPosition()
    {
        return isLockYPosition;
    }

    /// <summary> 
    /// 设置是否锁定Y轴位置的状态 
    /// </summary> 
    /// <param name="isLockYPosition">一个布尔值，用于设置是否锁定Y轴位置</param> 
    public void setIsLockYPosition(bool isLockYPosition)
    {
        this.isLockYPosition = isLockYPosition;
    }

    /// <summary> 
    /// 获取是否朝向摄像机的状态 
    /// </summary> 
    /// <returns>返回一个布尔值，表示是否朝向摄像机</returns> 
    public bool getIsLookingAt()
    {
        return isLookingAt;
    }

    /// <summary> 
    /// 设置是否朝向摄像机的状态 
    /// </summary> 
    /// <param name="isLookingAt">一个布尔值，用于设置是否朝向摄像机</param> 
    public void setIsLookingAt(bool isLookingAt)
    {
        this.isLookingAt = isLookingAt;
    }

    /// <summary> 
    /// 获取是否使用默认手动移动速度倍率的状态 
    /// </summary> 
    /// <returns>返回一个布尔值，表示是否使用默认手动移动速度倍率</returns> 
    public bool getIsDefaultSpeed()
    {
        return isDefaultSpeed;
    }

    /// <summary> 
    /// 设置是否使用默认手动移动速度倍率的状态 
    /// </summary> 
    /// <param name="isDefaultSpeed">一个布尔值，用于设置是否使用默认手动移动速度倍率</param> 
    public void setIsDefaultSpeed(bool isDefaultSpeed)
    {
        this.isDefaultSpeed = isDefaultSpeed;
    }

    /// <summary> 
    /// 获取是否处于手动移动模式的状态 
    /// </summary> 
    /// <returns>返回一个布尔值，表示是否处于手动移动模式</returns> 
    public bool getIsManualMove()
    {
        return isManualMove;
    }

    /// <summary> 
    /// 设置是否处于手动移动模式 
    /// </summary> 
    /// <param name="isManualMove">一个布尔值，用于设置是否处于手动移动模式</param> 
    public void setIsManualMove(bool isManualMove)
    {
        this.isManualMove = isManualMove;
    }

    ///<summary>
    /// 获取是否相对移动的状态
    ///</summary>
    public bool getIsRelativeMove()
    {
        return isRelativeMove;
    }

    ///<summary>
    /// 设置是否相对移动的状态
    ///</summary>
    public void setIsRelativeMove(bool isRelativeMove)
    {
        this.isRelativeMove = isRelativeMove;
    }

    ///<summary>
    /// 获取是否记录路径点的状态，与isRelativeMove互斥
    ///</summary>
    public bool getIsRecordPath()
    {
        return !isRelativeMove;
    }

    ///<summary>
    /// 设置是否启用识别
    ///</summary>
    public void setIsRecognition(bool isRecognition)
    {
        this.isRecognition = isRecognition;
    }

    ///<summary>
    /// 获取是否启用识别
    ///</summary>
    public bool getIsRecognition()
    {
        return isRecognition;
    }
}


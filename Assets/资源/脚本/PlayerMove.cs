using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public InputAttribute InputAttribute;
    public Pattern Pattern;

    public Animator animator;           // 动画师组件的引用，用于控制角色的动画
    public float baseMoveSpeedBlendTree1 = 0.8f; // 混合树 1（Z）的基础移动速度
    public float baseMoveSpeedBlendTree2 = 0.4f; // 混合树 2（Z-HGSZ）的基础移动速度
    public float rotationSpeed = 10f;   // 角色旋转插值的速度，用于平滑旋转
    public float idleDelay = 0.5f;      // 进入 Idle 状态的延迟时间（秒）
    public float speedSmoothTime = 0.5f; // Speed 参数的平滑时间

    private int currentBlendTree = -1;  // 当前活动混合树的索引（-1 表示无）
    private string[] triggers = { "Z", "Z-HGSZ", "Z-BT" }; // 动画混合树触发器
    private bool isMoving = false;      // 跟踪角色当前是否在移动
    private CharacterController characterController; // 用于没有根运动的移动
    private float idleTimer = 0f;       // 计时器，用于记录输入轴值为 0 的持续时间
    private float currentSpeed = 0f;    // 当前平滑的 Speed 值
    private float speedVelocity = 0f;   // Speed 平滑的速度
    private float lockedYPosition;      // 混合树执行期间锁定的 Y 轴位置

    // 将 animator 传递给 AnimationPlay 脚本
    private void TransmitAnimator()
    {
        GameObject targetObj = GameObject.FindWithTag("Script");
        if (targetObj != null)
        {
            AnimPlay animationPlay = targetObj.GetComponent<AnimPlay>();
            if (animationPlay != null)
            {
                animationPlay.SetAnimator(animator);
            }
        }
    }

    void Start()
    {
        TransmitAnimator();
        characterController = GetComponent<CharacterController>();
        Pattern = GameObject.Find("公共").GetComponent<Pattern>();
        InputAttribute = GameObject.Find("公共").GetComponent<InputAttribute>();
    }

    void Update()
    {
        HandleInput();
        CalculateSpeed();
        HandleMovement();
        HandleRotation();
        ControlAnimation();
        LockYPosition();
    }

    /// <summary>
    /// 处理输入和设置目标旋转
    /// </summary>
    private void HandleInput()
    {
        if (Pattern.getIsFollowing()) return;

        float inputMagnitude = InputAttribute.getInputMagnitude();
        if (inputMagnitude > 0)
        {
            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.up;
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();
            Vector3 targetDirection = camForward * InputAttribute.getVertical() + camRight * InputAttribute.getHorizontal();
            targetDirection.Normalize();
            InputAttribute.setRotationTarget(Quaternion.LookRotation(targetDirection));
        }
    }

    /// <summary>
    /// 计算平滑速度
    /// </summary>
    private void CalculateSpeed()
    {
        float inputMagnitude = InputAttribute.getInputMagnitude();
        float compensationFactor = InputAttribute.getCompensationFactor();
        float targetSpeed = inputMagnitude * compensationFactor;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedVelocity, speedSmoothTime);
    }

    /// <summary>
    /// 处理角色移动
    /// </summary>
    private void HandleMovement()
    {
        if (Pattern.getIsAnimMove())
        {
            HandleAnimationMovement();
        }
        else
        {
            HandleDirectMovement();
        }
    }

    /// <summary>
    /// 处理动画驱动的移动
    /// </summary>
    private void HandleAnimationMovement()
    {
        float inputMagnitude = InputAttribute.getInputMagnitude();

        if (inputMagnitude > 0)
        {
            if (!isMoving)
            {
                currentBlendTree = Random.Range(0, 3);
                animator.SetTrigger(triggers[currentBlendTree]);
                isMoving = true;
                lockedYPosition = transform.position.y;
                if (!Pattern.getIsFollowing())
                {
                    Pattern.setIsLockYPosition(true);
                }
            }
            idleTimer = 0f;
        }
        else
        {
            if (isMoving)
            {
                idleTimer += Time.deltaTime;
                if (idleTimer >= idleDelay)
                {
                    animator.SetTrigger("Idle");
                    isMoving = false;
                    currentBlendTree = -1;
                    idleTimer = 0f;
                    if (Pattern.getIsLockYPosition())
                    {
                        Pattern.setIsLockYPosition(false);
                    }
                }
            }
        }

        animator.SetFloat("Speed", currentSpeed);

        if (isMoving && (currentBlendTree == 0 || currentBlendTree == 1))
        {
            float baseMoveSpeed = currentBlendTree == 0 ? baseMoveSpeedBlendTree1 : baseMoveSpeedBlendTree2;
            float moveSpeed = baseMoveSpeed * currentSpeed;
            Vector3 moveDirection = transform.forward * moveSpeed * Time.deltaTime;
            moveDirection.y = 0;
            characterController.Move(moveDirection);
        }
    }

    /// <summary>
    /// 处理直接移动
    /// </summary>
    private void HandleDirectMovement()
    {
        if (currentSpeed > 0)
        {
            Vector3 moveDirection = transform.forward * currentSpeed * Time.deltaTime * InputAttribute.getSpeed();
            moveDirection.y = 0;
            characterController.Move(moveDirection);
        }
    }

    /// <summary>
    /// 处理角色旋转
    /// </summary>
    private void HandleRotation()
    {
        float inputMagnitude = InputAttribute.getInputMagnitude();
        if (inputMagnitude > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, InputAttribute.getRotationTarget(), Time.deltaTime * rotationSpeed);
        }
    }

    /// <summary>
    /// 控制动画播放速度
    /// </summary>
    private void ControlAnimation()
    {
        float animSpeed = Pattern.getIsAnimMove() ? (currentSpeed >= 0.7f ? 0.5f : 1f) : 1f;
        animator.speed = animSpeed;
    }

    /// <summary>
    /// 锁定 Y 轴位置
    /// </summary>
    private void LockYPosition()
    {
        if (Pattern.getIsLockYPosition())
        {
            Vector3 currentPosition = transform.position;
            currentPosition.y = lockedYPosition;
            transform.position = currentPosition;
        }
    }
}
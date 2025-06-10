using TMPro;
using UnityEngine;

//移动模式
public class MobilityPattern : ED
{
    public Pattern Pattern;
    public FollowerMove followerMove;

    public TMP_Text text1;//横屏文字
    public TMP_Text text2;//竖屏文字

    public Transform playerTransform; // 角色Transform
    public Transform cameraTransform; // 摄像机Transform
    private Vector3 lastCameraPosition; // 上次记录的摄像机位置

    void Start()
    {
        text1.text = "绝对移动";
        text2.text = "绝对移动";
        SwitchStatus();
    }

    public void Run()
    {
        Pattern.setIsRelativeMove(!Pattern.getIsRelativeMove());

        SwitchStatus();

        if (Pattern.getIsRelativeMove())
        {
            text1.text = "相对移动";
            text2.text = "相对移动";
        }
        else
        {
            text1.text = "绝对移动";
            text2.text = "绝对移动";
        }
    }

    void Update()
    {
        if (playerTransform == null)
        {
            try
            {
                followerMove = GameObject.FindGameObjectWithTag("NXD").GetComponent<FollowerMove>();
                playerTransform = GameObject.FindGameObjectWithTag("NXD").transform;
            }
            catch
            {
                return;
            }
        }
        else
        {
            // 在相对模式下，实时更新角色和路径点位置以跟随摄像机
            if (Pattern.getIsRelativeMove())
            {
                Vector3 cameraDelta = cameraTransform.position - lastCameraPosition;
                UpdateRelativePositions(cameraDelta);
            }

            // 更新上次摄像机位置
            lastCameraPosition = cameraTransform.position;
        }
    }

    // 更新路径点和角色位置以跟随摄像机移动
    private void UpdateRelativePositions(Vector3 cameraDelta)
    {
        // 更新角色位置
        playerTransform.position += cameraDelta;

        // 更新所有路径点
        followerMove.UpdatePathPoints(cameraDelta);
    }
}

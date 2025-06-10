using UnityEngine;

//跟随或取消跟随
public class Follow : ED
{
    public Pattern Pattern;
    // public FollowerMove followerMove;
    void Start()
    {
        SwitchStatus();
    }

    public void Run()
    {
        //手动移动模式期间禁止跟随模式
        if (!Pattern.getIsManualMove())
        {
            SwitchStatus();
            Pattern.setIsFollowing(!Pattern.getIsFollowing());
        }
    }

    void Update()
    {
        // if (followerMove == null)
        // {
        //     followerMove = GameObject.FindGameObjectWithTag("NXD").GetComponent<FollowerMove>();
        // }

        //处于手动模式并且跟随模式启用时，禁用跟随模式
        if (Pattern.getIsManualMove() && Pattern.getIsFollowing())
        {
            SwitchStatus();
            Pattern.setIsFollowing(!Pattern.getIsFollowing());
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

//控制角色缩放
public class Scale : MonoBehaviour
{
    //滑动条
    public Slider slider;
    public GameObject player;
    void Start()
    {

    }

    void Update()
    {
        //如果player为空每帧寻找标签为NXD的物体
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("NXD");
        }
        else
        {
            if (slider.value != 0)
            {
                player.transform.localScale = new Vector3(slider.value, slider.value, slider.value);
            }
        }
    }
}

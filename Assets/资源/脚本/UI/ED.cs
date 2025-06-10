using UnityEngine;
using UnityEngine.UI;

//按钮反馈
public class ED : MonoBehaviour
{
    public Image HButtonImage;               // 横屏按钮的Image组件 
    public Image VButtonImage;               // 竖屏按钮的Image组件 

    /// <summary>
    /// 是否启用当前按钮的功能
    /// </summary>
    public bool isEnabled = true;


    /// <summary>
    /// 更换按钮当前状态的对应显示，默认为启用状态，首次调用一次为失效状态
    /// </summary>
    public void SwitchStatus()
    {
        isEnabled = !isEnabled;

        // 修改按钮透明度
        Color tempColor = HButtonImage.color;
        tempColor.a = isEnabled ? 1f : 100f / 255f; // 255对应1，100对应约0.39 
        HButtonImage.color = tempColor;
        VButtonImage.color = tempColor;
    }
}
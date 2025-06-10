using TMPro;
using UnityEngine;
using UnityEngine.UI;

//启用/禁用动画移动
public class AnimMove : MonoBehaviour
{
    public Pattern Pattern;

    public TMP_Text text;
    public Image Image;

    void Start()
    {
        text.text = "禁用动画移动";
    }

    public void Run()
    {
        if (Pattern.getIsAnimMove())
        {
            text.text = "启用动画移动";
        }
        else
        {
            text.text = "禁用动画移动";
        }

        Pattern.setIsAnimMove(!Pattern.getIsAnimMove());

        // 修改按钮透明度
        Color tempColor = Image.color;
        tempColor.a = Pattern.getIsAnimMove() ? 1f : 100f / 255f; // 255对应1，100对应约0.39 
        Image.color = tempColor;
    }
}

using UnityEngine;
using UnityEngine.UI;

//移动
public class Move : MonoBehaviour
{
    public Pattern Pattern;

    public GameObject controlUI;             //操纵界面
    public Image HButtonImage;               // 横屏按钮的Image组件

    void Start()
    {
        // 修改按钮透明度
        Color tempColor = HButtonImage.color;
        tempColor.a = Pattern.getIsManualMove() ? 1f : 100f / 255f; // 255对应1，100对应约0.39 
        HButtonImage.color = tempColor;
        controlUI.SetActive(Pattern.getIsManualMove());
    }


    public void Run()
    {
        Pattern.setIsManualMove(!Pattern.getIsManualMove());

        // 修改按钮透明度
        Color tempColor = HButtonImage.color;
        tempColor.a = Pattern.getIsManualMove() ? 1f : 100f / 255f; // 255对应1，100对应约0.39 
        HButtonImage.color = tempColor;
        controlUI.SetActive(Pattern.getIsManualMove());
    }
}

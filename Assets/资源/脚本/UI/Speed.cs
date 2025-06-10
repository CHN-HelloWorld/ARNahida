using TMPro;
using UnityEngine;

//移动速度
public class Speed : MonoBehaviour
{
    public Pattern Pattern;
    public InputAttribute InputAttribute;
    public TMP_Text text;

    void Start()
    {
        text.text = "速度X1";
    }

    public void Run()
    {
        if (Pattern.getIsDefaultSpeed())
        {
            InputAttribute.setSpeed(2f);
            text.text = "速度X2";
        }
        else
        {
            InputAttribute.setSpeed(1f);
            text.text = "速度X1";
        }
        Pattern.setIsDefaultSpeed(!Pattern.getIsDefaultSpeed());
    }
}

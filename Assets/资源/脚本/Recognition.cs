using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class Recognition : MonoBehaviour
{
    public Pattern pattern;
    public Image HButtonImage;               // 横屏按钮的Image组件 
    public ARPlaneManager planeManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Run()
    {
        pattern.setIsRecognition(!pattern.getIsRecognition());
        planeManager.enabled = pattern.getIsRecognition();
        // 修改按钮透明度
        Color tempColor = HButtonImage.color;
        tempColor.a = pattern.getIsRecognition() ? 1f : 100f / 255f; // 255对应1，100对应约0.39 
        HButtonImage.color = tempColor;

    }

    // Update is called once per frame
    void Update()
    {

    }
}

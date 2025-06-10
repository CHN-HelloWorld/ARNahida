using UnityEngine;

//网格显示或隐藏
public class GridDH : ED
{
    void Start()
    {
        Run();
    }

    public void Run()
    {
        SwitchStatus();

        if (isEnabled)
        {
            int currentMask = Camera.main.cullingMask;
            int layer10Mask = 1 << 10;
            Camera.main.cullingMask = currentMask | layer10Mask;
        }
        else
        {
            Camera.main.cullingMask &= ~(1 << 10);
        }
    }
}
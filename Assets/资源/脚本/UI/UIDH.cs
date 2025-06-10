using UnityEngine;
using UnityEngine.EventSystems;

//控制UI的显示和隐藏
public class UIDH : MonoBehaviour
{
    private float touchStartTime; // 记录触摸开始时间
    private const float LONG_PRESS_DURATION = 1f; // 长按时间阈值（1秒）
    public bool show = true;

    public void Run()
    {
        show = false;
    }

    void Update()
    {
        if (!show)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    // 触摸开始，记录时间
                    touchStartTime = Time.time;
                }
                else if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    // 触摸持续中，检查是否达到长按时间
                    if (Time.time - touchStartTime >= LONG_PRESS_DURATION)
                    {
                        // 确保触摸没有在 UI 元素上
                        if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                        {
                            show = true;
                        }
                    }
                }
            }
        }
    }
}
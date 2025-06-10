using UnityEngine;
using System.Collections;

//动画窗口
public class AnimPlay : ED
{
    public UIDH uiDH;
    private Animator animator;//角色的动画控制器
    public GameObject HAnimationWindow;//横屏动画窗口
    public GameObject VAnimationWindow;//竖屏动画窗口

    // 设置动画控制器，由其他脚本调用自动赋值
    public void SetAnimator(Animator animator)
    {
        this.animator = animator;
    }

    void Start()
    {
        SwitchStatus();
    }

    // 辅助方法：执行前置操作并等待3秒后触发动画
    private IEnumerator TriggerAnimationAfterDelay(string triggerName)
    {
        HAnimationWindow.SetActive(false);//关闭横屏动画窗口
        VAnimationWindow.SetActive(false);//关闭竖屏动画窗口
        SwitchStatus();
        uiDH.Run();//隐藏全部UI

        // 等待3秒
        yield return new WaitForSeconds(3f);

        // 触发动画
        animator.SetTrigger(triggerName);
    }

    public void Run()
    {
        SwitchStatus();
        HAnimationWindow.SetActive(isEnabled);
        VAnimationWindow.SetActive(isEnabled);
    }

    public void KX_KX()
    {
        StartCoroutine(TriggerAnimationAfterDelay("KX-KX"));
    }

    public void KX_HX()
    {
        StartCoroutine(TriggerAnimationAfterDelay("KX-HX"));
    }

    public void KX_HGSZ()
    {
        StartCoroutine(TriggerAnimationAfterDelay("KX-HGSZ"));
    }

    public void KX_KS()
    {
        StartCoroutine(TriggerAnimationAfterDelay("KX-KS"));
    }

    public void KX_KST()
    {
        StartCoroutine(TriggerAnimationAfterDelay("KX-KST"));
    }

    public void KX_KYF()
    {
        StartCoroutine(TriggerAnimationAfterDelay("KX-KYF"));
    }

    public void KX_HQ()
    {
        StartCoroutine(TriggerAnimationAfterDelay("KX-HQ"));
    }

    public void KX_CH()
    {
        StartCoroutine(TriggerAnimationAfterDelay("KX-CH"));
    }

    public void KX_RJ()
    {
        StartCoroutine(TriggerAnimationAfterDelay("KX-RJ"));
    }

    public void Z()
    {
        StartCoroutine(TriggerAnimationAfterDelay("Z"));
    }

    public void Z_HGSZ()
    {
        StartCoroutine(TriggerAnimationAfterDelay("Z-HGSZ"));
    }

    public void Z_BT()
    {
        StartCoroutine(TriggerAnimationAfterDelay("Z-BT"));
    }

    public void Idle()
    {
        StartCoroutine(TriggerAnimationAfterDelay("Idle"));
    }

    public void DZH()
    {
        StartCoroutine(TriggerAnimationAfterDelay("DZH"));
    }

    public void FM1()
    {
        StartCoroutine(TriggerAnimationAfterDelay("FM1"));
    }

    public void FM2()
    {
        StartCoroutine(TriggerAnimationAfterDelay("FM2"));
    }

    public void ZS()
    {
        StartCoroutine(TriggerAnimationAfterDelay("ZS"));
    }

    public void DT()
    {
        StartCoroutine(TriggerAnimationAfterDelay("DT"));
    }

    public void QX_XF()
    {
        StartCoroutine(TriggerAnimationAfterDelay("QX-XF"));
    }

    public void QX_SW()
    {
        StartCoroutine(TriggerAnimationAfterDelay("QX-SW"));
    }

    public void QX_SB()
    {
        StartCoroutine(TriggerAnimationAfterDelay("QX-SB"));
    }

    public void QX_BA()
    {
        StartCoroutine(TriggerAnimationAfterDelay("QX-BA"));
    }

    public void z_Z1()
    {
        StartCoroutine(TriggerAnimationAfterDelay("z-Z1"));
    }

    public void z_Z2()
    {
        StartCoroutine(TriggerAnimationAfterDelay("z-Z2"));
    }

    public void z_Z3()
    {
        StartCoroutine(TriggerAnimationAfterDelay("z-Z3"));
    }

    public void z_Z()
    {
        StartCoroutine(TriggerAnimationAfterDelay("z-Z"));
    }

    public void Z_z1()
    {
        StartCoroutine(TriggerAnimationAfterDelay("Z-z1"));
    }

    public void Z_z2()
    {
        StartCoroutine(TriggerAnimationAfterDelay("Z-z2"));
    }

    public void Z_z3()
    {
        StartCoroutine(TriggerAnimationAfterDelay("Z-z3"));
    }

    public void Z_T()
    {
        StartCoroutine(TriggerAnimationAfterDelay("Z-T"));
    }

    public void T_Z()
    {
        StartCoroutine(TriggerAnimationAfterDelay("T-Z"));
    }

    public void Z_GZ()
    {
        StartCoroutine(TriggerAnimationAfterDelay("Z-GZ"));
    }
}
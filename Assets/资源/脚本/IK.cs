using UnityEngine;

public class IK : MonoBehaviour
{
    public Animator anim;      //动画
    public float maxDistance = 5f; //最大距离
    public float transitionSpeed = 2f; //平滑过渡速度

    private float currentWeight = 0f; //当前IK权重
    private Transform mainCamera; //主摄像机

    void Start()
    {
        mainCamera = Camera.main.transform; //获取主摄像机
    }

    private void OnAnimatorIK(int layerIndex)
    {
        float distance = Vector3.Distance(transform.position, mainCamera.position);
        float targetWeight = (mainCamera != null && distance <= maxDistance) ? 1f : 0f;

        // 平滑过渡IK权重
        currentWeight = Mathf.Lerp(currentWeight, targetWeight, Time.deltaTime * transitionSpeed);

        anim.SetLookAtWeight(currentWeight);
        if (currentWeight > 0.01f && mainCamera != null)
        {
            anim.SetLookAtPosition(mainCamera.position); //头部看向主摄像机
        }
    }

    void Update()
    {
    }
}
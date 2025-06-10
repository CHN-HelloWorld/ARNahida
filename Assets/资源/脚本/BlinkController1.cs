using System.Collections;
using UnityEngine;

public class BlinkController1 : MonoBehaviour
{
    // 左右眼的 SkinnedMeshRenderer 
    public SkinnedMeshRenderer SkinnedMeshRenderer;

    // 左右眼眨眼表情在 BlendShapes 中的索引 
    public int leftEyeBlinkBlendIndex;
    public int rightEyeBlinkBlendIndex;

    // 左右眼眨眼表情的初始权重值 
    public float leftEyeBlinkWeight = 0.0f;
    public float rightEyeBlinkWeight = 0.0f;

    // 眨眼动画持续时间 
    public float blinkDuration = 0.3f;

    // 眨眼间隔时间 
    public float blinkInterval = 5.0f;

    // 计时器，用于控制眨眼间隔 
    private float blinkTimer = 0.0f;

    void Start()
    {
        // 设置左右眼眨眼表情的初始权重值 
        SkinnedMeshRenderer.SetBlendShapeWeight(leftEyeBlinkBlendIndex, leftEyeBlinkWeight);
        SkinnedMeshRenderer.SetBlendShapeWeight(rightEyeBlinkBlendIndex, rightEyeBlinkWeight);
    }

    void Update()
    {
        blinkTimer += Time.deltaTime;

        // 如果计时器超过了眨眼间隔时间，就触发眨眼动画 
        if (blinkTimer >= blinkInterval)
        {
            StartCoroutine(BlinkCoroutine());
            blinkTimer = 0.0f; // 重置计时器 
        }
    }

    IEnumerator BlinkCoroutine()
    {
        // 将左右眼眨眼表情的权重值逐渐变为100，然后再逐渐恢复为0，实现眨眼动画 
        for (float t = 0.0f; t < blinkDuration; t += Time.deltaTime)
        {
            float leftEyeWeight = Mathf.Lerp(leftEyeBlinkWeight, 100.0f, t / blinkDuration);
            float rightEyeWeight = Mathf.Lerp(rightEyeBlinkWeight, 100.0f, t / blinkDuration);

            SkinnedMeshRenderer.SetBlendShapeWeight(leftEyeBlinkBlendIndex, leftEyeWeight);
            SkinnedMeshRenderer.SetBlendShapeWeight(rightEyeBlinkBlendIndex, rightEyeWeight);

            yield return null;
        }

        for (float t = 0.0f; t < blinkDuration; t += Time.deltaTime)
        {
            float leftEyeWeight = Mathf.Lerp(100.0f, leftEyeBlinkWeight, t / blinkDuration);
            float rightEyeWeight = Mathf.Lerp(100.0f, rightEyeBlinkWeight, t / blinkDuration);

            SkinnedMeshRenderer.SetBlendShapeWeight(leftEyeBlinkBlendIndex, leftEyeWeight);
            SkinnedMeshRenderer.SetBlendShapeWeight(rightEyeBlinkBlendIndex, rightEyeWeight);

            yield return null;
        }

        // 将左右眼眨眼表情的权重值恢复为初始值 
        SkinnedMeshRenderer.SetBlendShapeWeight(leftEyeBlinkBlendIndex, leftEyeBlinkWeight);
        SkinnedMeshRenderer.SetBlendShapeWeight(rightEyeBlinkBlendIndex, rightEyeBlinkWeight);
    }
}
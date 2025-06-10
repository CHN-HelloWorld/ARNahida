using UnityEngine;

public class InstantiateOnButton : MonoBehaviour
{
    [SerializeField]
    [Tooltip("要实例化的预制体")]
    private GameObject prefab;

    public void Run()
    {
        if (GameObject.FindWithTag("NXD") == null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            Instantiate(prefab, cameraPos, Quaternion.identity);
        }
    }
}
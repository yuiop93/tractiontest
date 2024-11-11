using UnityEngine;

[ExecuteInEditMode] // 确保在编辑模式下也执行
public class BoxColliderRotator : MonoBehaviour
{
    public Vector3 colliderRotation;
    private GameObject colliderHolder;
    private BoxCollider boxCollider;

    void Start()
    {
        InitializeColliderHolder();
        UpdateColliderTransform();
    }

    void Update()
    {
        // 仅在编辑模式下执行，避免在播放模式下每帧执行
        if (!Application.isPlaying)
        {
            UpdateColliderTransform();
        }
    }

    private void InitializeColliderHolder()
    {
        // 查找或创建子对象作为Collider Holder
        colliderHolder = transform.Find("BoxColliderHolder")?.gameObject;

        if (colliderHolder == null)
        {
            colliderHolder = new GameObject("BoxColliderHolder");
            colliderHolder.transform.parent = transform;
            colliderHolder.transform.localPosition = Vector3.zero;
            colliderHolder.transform.localRotation = Quaternion.identity;
            colliderHolder.transform.localScale = Vector3.one;

            boxCollider = colliderHolder.AddComponent<BoxCollider>();
        }
        else
        {
            boxCollider = colliderHolder.GetComponent<BoxCollider>();
        }
    }

    private void UpdateColliderTransform()
    {
        if (colliderHolder != null)
        {
            colliderHolder.transform.localRotation = Quaternion.Euler(colliderRotation);
        }
    }
}

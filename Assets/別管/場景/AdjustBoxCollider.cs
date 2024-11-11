using UnityEngine;

public class AdjustBoxCollider : MonoBehaviour
{
    void Start()
    {
        // 获取对象上的Box Collider组件
        BoxCollider boxCollider = GetComponentInChildren<BoxCollider>();

        if (boxCollider != null)
        {
            // 设置Box Collider的位置（相对于子对象）
            boxCollider.center = new Vector3(0, 0, 0);

            // 获取Box Collider的Transform组件
            Transform boxColliderTransform = boxCollider.transform;

            // 调整Box Collider的旋转
            boxColliderTransform.localRotation = Quaternion.Euler(45, 0, 0); // 旋转45度

            // 调整Box Collider的缩放
            boxColliderTransform.localScale = new Vector3(1, 1, 1);
        }
    }
}

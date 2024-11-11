using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    // 確保物體二有 Rigidbody 和 Collider
    // 當物體一（帶有 Box Collider）碰撞到物體二時，會呼叫這個函數
    void OnCollisionEnter(Collision collision)
    {
        // 檢查碰撞的物體是否是物體一
        Debug.Log("物體二被物體一碰撞了！");
    }
}
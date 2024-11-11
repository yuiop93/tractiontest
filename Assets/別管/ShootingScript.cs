using UnityEngine;
using UnityEngine.UI; // 引入UI命名空間

public class ShootingScript : MonoBehaviour
{
    public RectTransform crosshair; // 您的準心UI元素

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 滑鼠左鍵被按下
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 將準心的屏幕位置轉換為世界射線
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 發射射線，並檢測是否擊中目標
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Hit " + hit.collider.name); // 打印擊中的物體名稱
            // 這裡可以添加其他射擊擊中後的邏輯
        }
    }
}

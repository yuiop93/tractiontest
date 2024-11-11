using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowGun : MonoBehaviour
{
    public Transform gunMuzzle;  // 槍口的 Transform
    public Transform cameraTransform;  // 相機的 Transform

    void Update()
    {
        if (gunMuzzle != null && cameraTransform != null)
        {
            // 取得槍口的水平方向（Y 軸旋轉）
            Vector3 gunForward = gunMuzzle.forward;
            gunForward.y = 0;  // 僅保持水平方向

            if (gunForward != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(gunForward);
                cameraTransform.rotation = Quaternion.Euler(cameraTransform.rotation.eulerAngles.x, targetRotation.eulerAngles.y, cameraTransform.rotation.eulerAngles.z);
            }
        }
    }
}
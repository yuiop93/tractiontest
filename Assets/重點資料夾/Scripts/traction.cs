using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class traction : MonoBehaviour
{
    public GameObject ShootCamGamObj;
    public GameObject crossGameobj;
    public Transform gunMuzzle; // 槍口位置的參考
    public LineRenderer laserLineRenderer; // LineRenderer 用於雷射
    public float laserWidth = 0.1f; // 雷射的寬度
    public float laserMaxLength = 100f; // 雷射的最大長度

    public float attractionForce = 10f; // 吸引力，用於控制物體靠近槍口
    public float holdDistance = 3f; // 抓取物體時保持的距離

    private StarterAssetsInputs _sai;
    private Animator _anim;
    private Rigidbody grabbedObject; // 被抓取的物體
    private bool isObjectGrabbed = false;

    void Start()
    {
        _sai = GetComponent<StarterAssetsInputs>();
        _anim = GetComponent<Animator>();

        // 設定激光線條的寬度
        laserLineRenderer.startWidth = laserWidth;
        laserLineRenderer.endWidth = laserWidth;
    }

    void Update()
    {
        Vector3 aimWorldPos = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        // 瞄準射線檢測
        if (Physics.Raycast(ray, out RaycastHit rh, laserMaxLength))
        {
            aimWorldPos = rh.point;
        }
        else
        {
            aimWorldPos = ray.origin + ray.direction * laserMaxLength;
        }

        if (_sai.aim)
        {
            ShootCamGamObj.SetActive(true);
            crossGameobj.SetActive(true);
            _anim.SetLayerWeight(1, Mathf.Lerp(_anim.GetLayerWeight(1), 1f, Time.deltaTime * 10));

            // 計算目標方向，僅保持水平旋轉
            Vector3 aimDirection = (aimWorldPos - transform.position).normalized;
            aimDirection.y = 0; // 僅保持水平旋轉

            // 添加水平偏移角度，例如向右偏移幾度
            float horizontalOffsetAngle = 45f; // 正值表示向右偏移，調整這個值來微調
            Quaternion offsetRotation = Quaternion.Euler(0, horizontalOffsetAngle, 0);

            // 將偏移應用到目標旋轉
            Quaternion targetRotation = Quaternion.LookRotation(aimDirection) * offsetRotation;

            // 使用 Lerp 平滑過渡到偏移後的目標方向
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 50);


            // 抓取或控制物體
            if (Input.GetButton("Fire"))
            {
                laserLineRenderer.enabled = true;
                laserLineRenderer.SetPositions(new Vector3[] { gunMuzzle.position, aimWorldPos });
                if (isObjectGrabbed)
                {
                    MoveGrabbedObject();
                }
                else
                {
                    TryGrabObject(ray);
                }
            }
            else
            {
                laserLineRenderer.enabled = false;
                if (isObjectGrabbed)
                {
                    ReleaseObject();
                }
            }

            // 啟用激光線
            
        }
        else
        {
            ShootCamGamObj.SetActive(false);
            crossGameobj.SetActive(false);
            _anim.SetLayerWeight(1, Mathf.Lerp(_anim.GetLayerWeight(1), 0f, Time.deltaTime * 10));
            laserLineRenderer.enabled = false;

            if (isObjectGrabbed)
            {
                ReleaseObject();
            }
        }
    }

    // 嘗試抓取物體
    void TryGrabObject(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, laserMaxLength))
        {
            if (hit.rigidbody != null)
            {
                grabbedObject = hit.rigidbody;
                grabbedObject.useGravity = false;
                isObjectGrabbed = true;
            }
        }
    }

    // 移動抓取的物體到槍口前
    void MoveGrabbedObject()
    {
        if (grabbedObject != null)
        {
            // 計算目標位置
            Vector3 targetPosition = gunMuzzle.position + gunMuzzle.forward * holdDistance;
            Vector3 direction = (targetPosition - grabbedObject.position);

            // 鎖定物體的旋轉，防止碰撞導致旋轉
            grabbedObject.freezeRotation = true;

            // 設定物體的速度來移動到目標位置
            grabbedObject.velocity = direction * attractionForce;

            // 更新雷射線位置，將其終點設為被抓取物體的位置
            laserLineRenderer.SetPositions(new Vector3[] { gunMuzzle.position, grabbedObject.position });
        }
    }

    // 釋放抓取的物體
    void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            // 恢復物體的旋轉
            grabbedObject.freezeRotation = false;
            grabbedObject.useGravity = true;
            grabbedObject = null;
            isObjectGrabbed = false;
        }
    }
}

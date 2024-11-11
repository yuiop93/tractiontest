using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionActivator : MonoBehaviour
{
    public GameObject emissionObject; // 需要启用 Emission 的物体
    public GameObject animatorObject; // 包含 Animator 的物体
    public GameObject rigidbodyObject; // 包含 Rigidbody 的物体
    public int materialIndex = 1; // 需要操作的材质索引
    private Animator objectAnimator; // 用于存储获取到的 Animator
    private Rigidbody objectRigidbody; // 用于存储获取到的 Rigidbody
    private readonly string animatorParameter = "gwopen"; // Animator 中的布尔参数名

    private void Start()
    {
        // 获取 animatorObject 的 Animator 组件
        if (animatorObject != null)
        {
            objectAnimator = animatorObject.GetComponent<Animator>();
            if (objectAnimator == null)
            {
                Debug.LogWarning("Animator component not found on animatorObject.");
            }
        }
        else
        {
            Debug.LogWarning("animatorObject is not assigned.");
        }

        // 获取 rigidbodyObject 的 Rigidbody 组件
        if (rigidbodyObject != null)
        {
            objectRigidbody = rigidbodyObject.GetComponent<Rigidbody>();
            if (objectRigidbody == null)
            {
                Debug.LogWarning("Rigidbody component not found on rigidbodyObject.");
            }
        }
        else
        {
            Debug.LogWarning("rigidbodyObject is not assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Activator")) // 碰撞检测标签为 Activator 的物体
        {
            EnableEmission(emissionObject.GetComponent<Renderer>(), materialIndex);
            SetAnimatorParameter(objectAnimator, animatorParameter, true);
            EnableGravity(objectRigidbody, true);
        }
    }

    private void EnableEmission(Renderer renderer, int index)
    {
        if (renderer != null && index >= 0 && index < renderer.materials.Length)
        {
            Material mat = renderer.materials[index];
            mat.EnableKeyword("_EMISSION");
            //mat.SetColor("_EmissionColor", Color.white); // 你可以在这里设置发光的颜色
        }
    }

    private void SetAnimatorParameter(Animator animator, string parameter, bool value)
    {
        if (animator != null)
        {
            animator.SetBool(parameter, value);
        }
        else
        {
            Debug.LogWarning("Animator is not assigned or found.");
        }
    }

    private void EnableGravity(Rigidbody rb, bool enable)
    {
        if (rb != null)
        {
            rb.useGravity = enable;
        }
        else
        {
            Debug.LogWarning("Rigidbody is not assigned or found.");
        }
    }
}

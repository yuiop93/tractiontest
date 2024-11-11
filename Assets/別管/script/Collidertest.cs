using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidertest : MonoBehaviour
{
    public GameObject targetObject; // 用于開啟 Emission 的物件
    public SkinnedMeshRenderer blendShapeObject; // 用于增加 Blend Shape 值的物體
    public float blendShapeSpeed = 50f; // Blend Shape 增加的速度
    private float blendShapeValue = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("reflex"))
        {
            EnableEmission(targetObject.GetComponent<Renderer>());
            blendShapeValue = 0f; // 重置Blend Shape值
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("reflex"))
        {
            float increment = blendShapeSpeed * Time.deltaTime;
            blendShapeValue += increment; // 增加Blend Shape值
            blendShapeValue = Mathf.Clamp(blendShapeValue, 0f, 100f); // 限制Blend Shape值在0到100之间
            Debug.Log($"Blend shape value: {blendShapeValue}");
            BlendShapeManager.Instance.SetBlendShapeWeight(blendShapeObject, 0, blendShapeValue);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("reflex"))
        {
            DisableEmission(targetObject.GetComponent<Renderer>());
            ResetBlendShape(blendShapeObject, 0);
        }
    }

    private void EnableEmission(Renderer renderer)
    {
        if (renderer != null)
        {
            renderer.material.EnableKeyword("_EMISSION");
        }
    }

    private void DisableEmission(Renderer renderer)
    {
        if (renderer != null)
        {
            renderer.material.DisableKeyword("_EMISSION");
        }
    }

    private void ResetBlendShape(SkinnedMeshRenderer skinnedMeshRenderer, int index)
    {
        if (skinnedMeshRenderer != null)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(index, 0f);
        }
    }
}

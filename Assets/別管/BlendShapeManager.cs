using System.Collections.Generic;
using UnityEngine;

public class BlendShapeManager : MonoBehaviour
{
    private static BlendShapeManager _instance;

    public static BlendShapeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BlendShapeManager>();
                if (_instance == null)
                {
                    GameObject manager = new GameObject("BlendShapeManager");
                    _instance = manager.AddComponent<BlendShapeManager>();
                }
            }
            return _instance;
        }
    }

    private Dictionary<SkinnedMeshRenderer, float> blendShapeValues = new Dictionary<SkinnedMeshRenderer, float>();

    public void IncrementBlendShapeWeight(SkinnedMeshRenderer renderer, int index, float increment)
    {
        if (renderer != null)
        {
            if (!blendShapeValues.ContainsKey(renderer))
            {
                blendShapeValues[renderer] = 0f;
            }
            blendShapeValues[renderer] = Mathf.Clamp(blendShapeValues[renderer] + increment, 0, 100);
            renderer.SetBlendShapeWeight(index, blendShapeValues[renderer]);
        }
    }

    public void SetBlendShapeWeight(SkinnedMeshRenderer renderer, int index, float value)
    {
        if (renderer != null)
        {
            blendShapeValues[renderer] = Mathf.Clamp(value, 0, 100);
            renderer.SetBlendShapeWeight(index, blendShapeValues[renderer]);
        }
    }

    public float GetBlendShapeWeight(SkinnedMeshRenderer renderer)
    {
        if (renderer != null && blendShapeValues.ContainsKey(renderer))
        {
            return blendShapeValues[renderer];
        }
        return 0f;
    }
}

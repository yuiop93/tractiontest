using UnityEngine;

public class BlendShapeController : MonoBehaviour
{
    public SkinnedMeshRenderer blendShapeRenderer;
    private float[] blendShapeValues;

    void Start()
    {
        blendShapeValues = new float[blendShapeRenderer.sharedMesh.blendShapeCount];
    }

    public void UpdateBlendShapeValue(int index, float value)
    {
        if (index < 0 || index >= blendShapeValues.Length)
            return;

        blendShapeValues[index] = Mathf.Clamp(value, 0, 100);
        blendShapeRenderer.SetBlendShapeWeight(index, blendShapeValues[index]);
    }

    public void IncrementBlendShapeValue(int index, float increment)
    {
        UpdateBlendShapeValue(index, blendShapeValues[index] + increment);
    }

    public void ResetBlendShapeValue(int index)
    {
        UpdateBlendShapeValue(index, 0f);
    }
}

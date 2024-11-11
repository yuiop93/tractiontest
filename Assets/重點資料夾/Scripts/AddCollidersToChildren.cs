using UnityEngine;

public class AddCollidersToChildren : MonoBehaviour
{
    void Start()
    {
        AddColliders(transform);
    }

    void AddColliders(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.GetComponent<MeshFilter>())
            {
                if (child.gameObject.GetComponent<MeshCollider>() == null)
                {
                    child.gameObject.AddComponent<MeshCollider>();
                }
            }
            AddColliders(child);
        }
    }
}

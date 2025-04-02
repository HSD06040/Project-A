using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    private MeshFilter filter;
    private MeshRenderer renderer;

    private void Awake()
    {
        filter = GetComponent<MeshFilter>();
        renderer = GetComponent<MeshRenderer>();
    }

    public void SetupItemObejct(ItemData data)
    {
        itemData = data;

        filter.mesh = itemData.itemMesh;
        renderer.material = itemData.itemMaterial;
    }
}

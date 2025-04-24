using UnityEditor.Rendering;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ItemData itemData;
    private MeshFilter filter;
    private MeshRenderer itemRenderer;
    private MeshCollider mc;
    private Rigidbody rb;

    private void Awake()
    {
        filter       = GetComponent<MeshFilter>();
        itemRenderer = GetComponent<MeshRenderer>();

        mc = GetComponent<MeshCollider>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.AddForce(new Vector3(Random.Range(-2, 2), Random.Range(6, 10), Random.Range(-2, 2)), ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * .3f, ForceMode.Impulse);
    }

    public void SetupItemObejct(ItemData data)
    {
        itemData = data;

        filter.mesh = itemData.itemMesh;
        itemRenderer.material = itemData.itemMaterial;
        mc.sharedMesh = itemData.itemMesh;
    }

    public void PickupItem()
    {
        if (GameManager.Data.inventory.CanAdd())
        {
            GameManager.Data.inventory.AddItem(itemData);
            Destroy(gameObject);
        }
    }
}

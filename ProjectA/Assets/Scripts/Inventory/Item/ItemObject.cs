using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    private MeshFilter filter;
    private MeshRenderer renderer;
    private Rigidbody rb;

    private void Awake()
    {
        filter = GetComponent<MeshFilter>();
        renderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.AddForce(new Vector3(Random.Range(-3, 3), Random.Range(6, 10), Random.Range(-3, 3)), ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * 1f, ForceMode.Impulse);
    }

    public void SetupItemObejct(ItemData data)
    {
        itemData = data;

        filter.mesh = itemData.itemMesh;
        renderer.material = itemData.itemMaterial;
    }
}

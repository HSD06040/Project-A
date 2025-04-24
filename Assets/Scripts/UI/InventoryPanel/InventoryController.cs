using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private InventoryPanel panel;
    private Inventory inventory;

    private void Start()
    {
        panel = GetComponent<InventoryPanel>();
        inventory = GameManager.Data.inventory;
        inventory.OnChangedItem += panel.status.UpdateStatusUI;
        GameManager.Data.playerStat.ClearStat();
        inventory.UpdateSlotUI();
    }

    private void OnEnable()
    {
        if(inventory != null)
            inventory.OnChangedItem += panel.status.UpdateStatusUI;
    }

    private void OnDisable()
    {
        inventory.OnChangedItem -= panel.status.UpdateStatusUI;
    }


}

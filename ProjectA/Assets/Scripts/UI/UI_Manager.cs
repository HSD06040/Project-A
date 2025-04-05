using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public InventoryPanel inventoryPanel;

    private void Awake()
    {
        inventoryPanel = GameObject.Find("InventoryPanel").GetComponent<InventoryPanel>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            SwitchUI(inventoryPanel.gameObject);
    }

    public void SwitchUI(GameObject ui)
    {
        if(ui.activeSelf)
        {
            ui.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else
        {
            ui.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}

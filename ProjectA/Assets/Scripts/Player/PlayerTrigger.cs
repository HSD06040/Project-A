using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTrigger : MonoBehaviour
{
    private bool isInteractive;
    IInteractable interactiveObj;

    private void OnTriggerEnter(Collider hit)
    {
        if(hit.CompareTag("Interactive"))
        {
            isInteractive = true;
            interactiveObj = hit.GetComponent<IInteractable>();
            interactiveObj.Interactable();
        }
    }
    private void OnTriggerExit(Collider hit)
    {
        if (hit.CompareTag("Interactive") || hit.CompareTag("Untagged"))
        {
            isInteractive = false;

            if(interactiveObj != null)
                interactiveObj = null;
        }
    }

    private void Update()
    {
        if (isInteractive && Input.GetKeyDown(KeyCode.F))
            interactiveObj.Open();
    }
}

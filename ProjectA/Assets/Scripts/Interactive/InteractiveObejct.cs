using UnityEngine;

public class InteractiveObejct : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject interactiveKey;
    protected Animator keyAnim;
    protected Animator anim;
    protected bool isOpen = false;
    private int collectorCount = 0;

    public virtual void Interactive()
    {
        if (!interactiveKey.activeSelf)
        {
            if (keyAnim == null)
                keyAnim = interactiveKey.GetComponent<Animator>();

            interactiveKey.SetActive(true);
        }
        if (anim == null)
            anim = GetComponentInChildren<Animator>();
    }

    public virtual void Open()
    {
        anim.SetBool("Open", true);
    }

    protected virtual void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Collector") && interactiveKey.activeSelf && !isOpen)
        {
            collectorCount++;
            keyAnim.SetBool("In", true);
        }
    }

    protected virtual void OnTriggerExit(Collider hit)
    {
        if (hit.CompareTag("Collector") && !isOpen)
        {
            collectorCount = Mathf.Max(0, collectorCount - 1);

            if (collectorCount == 0)
            keyAnim.SetBool("In", false);
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class InteractiveObejct : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject interactiveKey;
    protected Animator keyAnim;
    protected Animator anim;
    protected bool isOpen = false;
    private SceneLoader loader;
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

        if (hit.CompareTag("Loader"))
        {
            loader = hit.GetComponent<SceneLoader>();
            if (loader != null)
            {
                loader.RegisterOnSaved(Saved);
                loader.RegisterOnLoaded(Loaded);
            }
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

        if (hit.CompareTag("Loader"))
        {
            if (loader != null)
            {
                loader.UnregisterOnSaved(Saved);
                loader.UnregisterOnLoaded(Loaded);
            }
        }
    }

    private void Saved()
    {
        if (loader != null && !string.IsNullOrEmpty(gameObject.name))
        {
            loader.interactiveDic[gameObject.name] = isOpen;
        }
    }

    private void Loaded()
    {
        if (this == null || gameObject == null || loader == null) return;

        if (loader.interactiveDic.TryGetValue(gameObject.name, out bool _isOpen))
        {
            isOpen = _isOpen;

            StartCoroutine(DelayOpen());
            
            loader.interactiveDic.Remove(gameObject.name);
        }
    }

    private void OnDestroy()
    {
        if (loader != null)
        {
            loader.UnregisterOnSaved(Saved);
            loader.UnregisterOnLoaded(Loaded);
        }
    }

    IEnumerator DelayOpen()
    {
        yield return new WaitForSeconds(.5f);

        if (anim != null)
            anim.SetBool("Open", isOpen);
        else
            Debug.Log("aaaa");
    }
}

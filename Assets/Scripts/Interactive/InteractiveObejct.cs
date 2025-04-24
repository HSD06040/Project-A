using UnityEngine;
using UnityEngine.Rendering;

public class InteractiveObejct : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject interactiveKey;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Animator keyAnim;
    private SceneLoader loader;
    private int collectorCount = 0;
    protected bool isOpen = false;
    private bool isAddEvnet;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public virtual void Interactive()
    {
        if (!interactiveKey.activeSelf)
        {
            interactiveKey.SetActive(true);
        }
    }

    public virtual void Open()
    {
        if (isOpen)
            return;

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
            AddEvent();
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

    private void Saved()
    {
        loader.interactiveDic[gameObject.name] = isOpen;
    }

    private void Loaded()
    {
        if (loader.interactiveDic.TryGetValue(gameObject.name, out bool _isOpen))
        {
            isOpen = _isOpen;

            if (isOpen)
            {
                anim.SetBool("Open", true);
                keyAnim.SetBool("In", false);
            }    

            loader.interactiveDic.Remove(gameObject.name);
        }
    }

    private void OnDestroy()
    {
        if (loader == null)
            return;

        RemoveEvnet();
    }

    private void RemoveEvnet()
    {
        if (isAddEvnet)
        {
            loader.OnSavedScene -= Saved;
            loader.OnLoadedScene -= Loaded;
        }
    }
    private void AddEvent()
    {
        if (!isAddEvnet)
        {
            isAddEvnet = true;
            loader.OnSavedScene += Saved;
            loader.OnLoadedScene += Loaded;
        }
    }
}
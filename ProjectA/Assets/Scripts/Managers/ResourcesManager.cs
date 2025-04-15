using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public T Load<T>(string path) where T : Object
    {
        string key = $"{typeof(T)}.{path}";

        T resource = Resources.Load<T>(path);

        return resource;
    }

    public T Instantiate<T> (T original, Vector3 position, Quaternion rotation) where T : Object
    {
        GameObject go = GameManager.Pool.Get(original, position, rotation);

        return go as T;
    }
}

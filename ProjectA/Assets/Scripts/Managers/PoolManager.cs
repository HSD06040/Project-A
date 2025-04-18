using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    private Dictionary<string, IObjectPool<GameObject>> poolDic;
    private Dictionary<string, Transform> poolParent;
    private Transform poolRoot;

    private void Awake()
    {
        poolRoot = new GameObject("PoolRoot").transform;
        poolDic = new Dictionary<string, IObjectPool<GameObject>>();
        poolParent = new Dictionary<string, Transform>();
    }

    private void CreateObjectPool(string name, GameObject prefab, int maxSize)
    {
        Transform root = new GameObject().transform;
        root.name = $"{name} Pool";
        root.transform.parent = poolRoot;
        poolParent.Add(name, root);

        ObjectPool<GameObject> pool = new ObjectPool<GameObject>
            (
            createFunc: () =>
            {
                GameObject obj = Instantiate(prefab);
                obj.name = name;
                return obj;
            },

            actionOnGet: (GameObject obj) =>
            {
                obj.SetActive(true);
                obj.transform.SetParent(null);
            },

            actionOnRelease: (GameObject obj) =>
            {
                obj.SetActive(false);
                obj.transform.SetParent(poolParent[name]);
            },

            actionOnDestroy: (GameObject obj) =>
            {
                Destroy(obj);
            },
            maxSize: maxSize
            );
        poolDic.Add(name, pool);
    }

    public T Get<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : Object
    {
        GameObject go = original as GameObject;
        string name = go.name;

        if (!poolDic.ContainsKey(name))
            CreateObjectPool(name, go, 10);

        GameObject obj = poolDic[name].Get();
        
        obj.transform.parent = parent;
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        return obj as T;
    }

    public T Get<T>(T original, Vector3 position, Quaternion rotation) where T : Object
    {
        return Get<T>(original, position, rotation, null);
    }

    public void Release<T>(T instance) where T : Object
    {
        GameObject obj = instance as GameObject;
        string name = instance.name;

        if (!poolDic.ContainsKey(name))
            return;
        
        poolDic[name].Release(obj);
    }

    public void Release<T>(T instance,float dealy) where T : Object
    {
        GameObject obj = instance as GameObject;
        string name = instance.name;

        if (!poolDic.ContainsKey(name))
            return;

        StartCoroutine(DelayRelease(instance,dealy));
    }

    IEnumerator DelayRelease<T>(T instance, float dealy) where T : Object
    {
        yield return new WaitForSeconds(dealy);

        GameObject newObj = instance as GameObject;
        string name = newObj.name;

        if (poolDic.ContainsKey(name) && newObj.activeSelf)
            poolDic[name].Release(newObj);
    }
}
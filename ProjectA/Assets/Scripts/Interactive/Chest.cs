using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Chest : InteractiveObejct
{
    [SerializeField] private string[] paths;
    [SerializeField] private List<ItemData> itemDatas;
    [SerializeField] private ItemRarity chestGrade;

    [Header("Grade materials")]
    [SerializeField] private Material[] materials;

    private Dictionary<ItemRarity, Material> rarityMaterial;

    [Header("Chest material")]
    [SerializeField] private MeshRenderer upMeshRenderer;
    [SerializeField] private MeshRenderer bottomMeshRenderer;

    [Header("Chance")]
    [SerializeField] private float unCommonChance;
    [SerializeField] private float rareChance;
    [SerializeField] private float epicChance;
    [SerializeField] private float uniqueChance;
    [SerializeField] private float legendaryChance;

    private void Awake()
    {
        rarityMaterial = new Dictionary<ItemRarity, Material>
        {
            {ItemRarity.Common      , materials[0] },
            {ItemRarity.UnCommon    , materials[1] },
            {ItemRarity.Rare        , materials[2] },
            {ItemRarity.Epic        , materials[3] },
            {ItemRarity.Unique      , materials[4] },
            {ItemRarity.Legendary   , materials[5] },
        };

        Material[] upMats = upMeshRenderer.materials;
        upMats[0] = rarityMaterial[chestGrade];
        upMeshRenderer.materials = upMats;

        Material[] mats = bottomMeshRenderer.materials;
        mats[1] = rarityMaterial[chestGrade];
        bottomMeshRenderer.materials = mats;
    }
    public override void Interactable()
    {
        base.Interactable();
    }

    public override void Open()
    {
        base.Open();

        for (int i = 0; i < UnityEngine.Random.Range(1,3); i++)
        {
            SetupChest();
            DropItem(itemDatas[UnityEngine.Random.Range(0, itemDatas.Count)]);
            keyAnim.SetBool("In", false);
            gameObject.tag = "Untagged";
            isOpen = true;
        }
    }

    protected override void OnTriggerEnter(Collider hit)
    {
        base.OnTriggerEnter(hit);
    }

    protected override void OnTriggerExit(Collider hit)
    {
        base.OnTriggerExit(hit);
    }

    private void DropItem(ItemData item)
    {
        GameObject go = Resources.Load("Prefabs/ItemObejct") as GameObject;
        GameObject newItem = Instantiate(go, transform.position + new Vector3(0,1,0), Quaternion.identity);
        newItem.GetComponent<ItemObject>().SetupItemObejct(item);
    }

    private void SetupChest()
    {
        int random = UnityEngine.Random.Range(0, 100);
        int i = 0;

        if (legendaryChance > random) i = 0;
        else if (uniqueChance > random) i = 1;
        else if (epicChance > random) i = 2;
        else if (rareChance > random) i = 3;
        else if (unCommonChance > random) i = 4; else i = 5; 

        itemDatas = LoadAllScriptableObejcts<ItemData>(paths[i]);
    }

    private List<T> LoadAllScriptableObejcts<T>(string path) where T : ScriptableObject
    {
        List<T> results = new List<T>();

        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name, new[] {path});

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            if(asset != null)
            {
                results.Add(asset);
            }
        }

        return results;
    }
}

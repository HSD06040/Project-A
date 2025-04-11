using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Chest : InteractiveObejct
{
    [SerializeField] private List<ItemData> itemDatas;
    private ItemRarity dropItemRarity;

    [Header("Chest Data")]
    [SerializeField] private ChestData chestData;

    [Header("Chest Material")]
    [SerializeField] private MeshRenderer upMeshRenderer;
    [SerializeField] private MeshRenderer bottomMeshRenderer;

    private void Awake()
    {
        Material[] upMats = upMeshRenderer.materials;
        upMats[0] = chestData.material;
        upMeshRenderer.materials = upMats;

        Material[] bottomMats = bottomMeshRenderer.materials;
        bottomMats[1] = chestData.material;
        bottomMeshRenderer.materials = bottomMats;
    }
    public override void Interactive()
    {
        base.Interactive();
    }

    public override void Open()
    {
        base.Open();

        for (int i = 0; i < UnityEngine.Random.Range(1, 3); i++)
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
        GameObject newItem = Instantiate(go, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
        newItem.GetComponent<ItemObject>().SetupItemObejct(item);
    }

    private void SetupChest()
    {
        int itemCount = Random.Range(1, 3);
        int luck = GameManager.Data.playerStatData.luck.GetValue();

        for (int i = 0; i < itemCount; i++)
        {
            float random = UnityEngine.Random.Range(0, 100);

            if      (chestData.legendaryChance  + luck/20 > random)     dropItemRarity = ItemRarity.Legendary;
            else if (chestData.uniqueChance     + luck/10 > random)     dropItemRarity = ItemRarity.Unique;
            else if (chestData.epicChance       + luck/6 > random)      dropItemRarity = ItemRarity.Epic;
            else if (chestData.rareChance       + luck/3 > random)      dropItemRarity = ItemRarity.Rare;
            else if (chestData.unCommonChance   + luck > random)        dropItemRarity = ItemRarity.UnCommon;
            else                                                        dropItemRarity = ItemRarity.Common;

            List<ItemData> allitemDatas = LoadAllScriptableObejcts<ItemData>($"Assets/Data/Items/{dropItemRarity}");

            itemDatas.Add(allitemDatas[Random.Range(0, allitemDatas.Count)]);
        }
    }

    private List<T> LoadAllScriptableObejcts<T>(string path) where T : ScriptableObject
    {
        List<T> results = new List<T>();

        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name, new[] { path });

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            if (asset != null)
            {
                results.Add(asset);
            }
        }

        return results;
    }
}

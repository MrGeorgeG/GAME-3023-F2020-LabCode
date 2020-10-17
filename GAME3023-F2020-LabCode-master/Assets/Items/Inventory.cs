using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveHandler
{
    void OnSave();
    void OnLoad();
}

public class Inventory : MonoBehaviour, ISaveHandler
{
    [SerializeField]
    ItemTeble itemTeble;

    [SerializeField]
    GameObject inventoryPanel;

    [SerializeField]
    List<ItemSlot> itemSlots;

    [SerializeField]
    List<Item> startingItems;

    // Start is called before the first frame update
    void Start()
    {
        GameSaver.OnSave.AddListener(OnSave);
        GameSaver.OnLoad.AddListener(OnLoad);

        for (int i = 1; i < itemSlots.Count; i++)
        {
            GameObject newObject = Instantiate(itemSlots[0].gameObject, inventoryPanel.transform);
            ItemSlot newSlot = newObject.GetComponent<ItemSlot>();
            itemSlots[i] = newSlot;
        }

        for(int i = 0; i < startingItems.Count && i < itemSlots.Count; i++)
        {
            itemSlots[i].itemInSlot = startingItems[i];
        }

        foreach(ItemSlot item in itemSlots)
        {
            item.onItemUse.AddListener(OnItemUsed);
            item.itemCount = 16;
            item.UpdateIcon();
        }
    }

    void OnItemUsed(Item itemUsed)
    {
        //Debug.Log("Inventory: item used of category " + itemUsed.category);
    }

    public void OnSave()
    {
        PlayerPrefs.SetInt("Hellokay",1888);

        //Make empty string
        //For each item slot
        //Get its current item
        //If there is an item, write its id, and its count to the end of the string
        //If there is not an item, write -1 and 0

        //File format:
        //ID, Count, ID, Count, ID, Count

        string saveStr = "";
        foreach (ItemSlot itemSlot in itemSlots)
        {
            int id = -1;
            int count = 0;

            if (itemSlot.itemInSlot != null)
            {
                id = itemSlot.itemInSlot.ItemID;
                count = itemSlot.itemCount;
            }

            saveStr += id.ToString() + ',' + count.ToString() + ',';
        }
        PlayerPrefs.SetString(gameObject.name + "Inventory", saveStr);

    }

    public void OnLoad()
    {


        string loadedData = PlayerPrefs.GetString(gameObject.name + "Inventory", "");

        Debug.Log(loadedData);

        char[] delimiters = new char[] { ',' };
        string[] splitData = loadedData.Split(delimiters);

        for (int i = 0; i < itemSlots.Count; i++)
        {
            int dataIdx = i * 2;

            int id = int.Parse(splitData[dataIdx]);
            int count = int.Parse(splitData[dataIdx + 1]);

            if (id < 0)
            {
                itemSlots[i].itemInSlot = null;
                itemSlots[i].itemCount = 0;
            }else
            {
                itemSlots[i].itemInSlot = itemTeble.itemTable[id];
                itemSlots[i].itemCount = count;
            }

            itemSlots[i].UpdateIcon();
        }

        //Get save string
        //Split save string
        //For each itemSlot, grab a pair of entried (ID, Count) and parse them to int
        //If ID is -1, replace itemSlot's item with null
        //Otherwise, replace itemSlot with the corresponding item from the itemTable, and set its count to the parsed count
    }
}

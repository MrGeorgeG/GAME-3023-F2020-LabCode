using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "NewItemTable", menuName = "ScriptableObjects/ItemTable", order = 2)]
public class ItemTeble : ScriptableObject
{
    public Item[] itemTable;

    public void AssignItemIDs()
    {
        for (int i = 0; i < itemTable.Length; i++)
        {
            try
            {
                itemTable[i].ItemID = i;
            }catch(ItemException)
            {
                //this is fine
            }
        }    
    }

}

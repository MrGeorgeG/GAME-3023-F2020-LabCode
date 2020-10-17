using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ItemSlot : MonoBehaviour
{
    public UnityEvent<Item> onItemUse;

    public Item itemInSlot = null;
    public int itemCount = 0;

    [SerializeField]
    TMPro.TextMeshProUGUI itemCountText;

    [SerializeField]
    Image itemIcon;

    // Start is called before the first frame update
    void Start()
    {
        UpdateIcon();
    }

    /// <summary>
    /// Activate the item currently held in the slot
    /// </summary>
    public void UseItem()
    {
        if(itemInSlot != null)
        {
            if(itemCount >= 1)
            {
                itemInSlot.Use();
                onItemUse.Invoke(itemInSlot);
                itemCount--;
                UpdateIcon();
            }
        }
    }

    public void UpdateIcon()
    {
        if(itemCount == 0)
        {
            itemInSlot = null;
        }

      if(itemInSlot != null)
        {
            itemCountText.text = itemCount.ToString();
            itemIcon.sprite = itemInSlot.Icon;
            itemIcon.gameObject.SetActive(true);
        } else
        {
            itemIcon.gameObject.SetActive(false);
        }
    }
}

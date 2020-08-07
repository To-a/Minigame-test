using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    //TODO constructor does not work with monobehaviour, rewrite this class
    //TODO get mouseinput working
    //Pairing item transform with panel in inventory

    public Transform item;
    public Transform panel;
    public ItemStatusManager m_ItemStatus;
    public Button button;

    RectTransform rect;

    public int index;

    private void Start() {
        rect = GetComponent<RectTransform>();
    }
    void Update()
    {
        
    }

    

    public ItemSlot(Transform aItem, Transform aPanel, int aIndex){
        item = aItem;
        panel = aPanel;
        index = aIndex;
        if (item.GetComponent<ItemStatusManager>()){
            m_ItemStatus = item.GetComponent<ItemStatusManager>();
        }
    }

    public void setItemStatus(ItemStatusManager aItemStatus){
        m_ItemStatus = aItemStatus;
    }

}


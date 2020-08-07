using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInventory : MonoBehaviour
{
    //TODO 1.Add item to list ...done
    //TODO 2.Store item info in Itemslot ...done
    //add another element to ItemSlot.cs ...done
    //TODO 3.Display item in UI ...done
    //disable physics?
    //TODO 4.Retrieve item from UI
    //get mouse input working
    //TODO 5.Remove item from list
    //if transform == empty do nothing
    //TODO 6.Reorder items in inventory
    //TODO 7.Stack identical items
    public bool isOpen;
    public List<ItemSlot> itemSlots;
    public int itemCount;
    public int slotCount;
    public GameObject emptySlot;
    public Camera playerCamera;
    Collider itemCollider;
    GameObject clickedObject;

    void Start()
    {
        itemCount = 0;
        slotCount = 0;
        isOpen = false;
        itemSlots = new List<ItemSlot>();
        foreach(Transform slot in transform.GetComponentInChildren<Transform>()){
            if (slot.tag == "InventoryPanel"){
                Transform empty = emptySlot.transform;
                ItemSlot itemSlot = slot.gameObject.AddComponent<ItemSlot>();
                //redudant code - see ItemSlot.cs
                itemSlot.item = empty;
                itemSlot.panel = slot;
                itemSlot.index = slotCount;
                itemSlot.button = itemSlot.GetComponent<Button>();
                itemSlot.button.onClick.AddListener(() => removeFromInventory(itemSlot.index));
                itemSlots.Add(itemSlot);
                slotCount += 1;
            }
            
        }

        gameObject.SetActive(false);
    }

    
    void Update()
    {
       
    }

    public void toggle(){
        if (isOpen){
            isOpen = false;
            Cursor.lockState = CursorLockMode.Locked;
            gameObject.SetActive(false);
        } else {
            isOpen = true;
            Cursor.lockState = CursorLockMode.None;
            gameObject.SetActive(true);
            //remove all physics in inventory
            foreach (ItemSlot itemSlot in itemSlots){
                if (itemSlot.item.name != "empty"){
                    //itemSlot.m_ItemStatus.m_Rb.isKinematic = false;
                    //itemSlot.m_ItemStatus.m_Rb.useGravity = false;
                    //itemSlot.m_ItemStatus.m_Rb.velocity = Vector3.zero;
                    //itemSlot.m_ItemStatus.m_Rb.angularVelocity = Vector3.zero;
                    Destroy(itemSlot.m_ItemStatus.m_Rb);
                    itemCollider = itemSlot.m_ItemStatus.GetComponent<Collider>();
                    itemCollider.enabled = false;
                    itemSlot.item.SetParent(transform);
                    itemSlot.item.localScale = Vector3.one * 30f;
                    itemSlot.item.position = itemSlot.panel.position;
                    itemSlot.item.gameObject.SetActive(true);
                }
            }
        }
    }

    public void addToInventory(Transform itemToAdd){
        if (itemSlots[itemCount].item.name == "empty"){
            itemSlots[itemCount].item = itemToAdd;
            if (itemSlots[itemCount].item.GetComponent<ItemStatusManager>()){
                itemSlots[itemCount].m_ItemStatus = itemSlots[itemCount].item.GetComponent<ItemStatusManager>(); 
                itemSlots[itemCount].m_ItemStatus.inInventory = true;
            }
        }
        Debug.Log(itemSlots[itemCount].item.name);
        itemCount += 1;
    }
    //TODO
    private Transform removeFromInventory(int slotIndex){
        Debug.Log("remove");
        Transform itemToRemove = itemSlots[slotIndex].item;
        itemSlots[slotIndex].item = emptySlot.transform;
        itemSlots[slotIndex].m_ItemStatus = null;
        return itemToRemove;
    }
}

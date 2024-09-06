using System;
using Unity.Netcode;
using UnityEngine;
using static InventoryResource;
using static PlayerShipMovement;

public class ChestScript : MonoBehaviour
{

    [SerializeField] GameObject canvas;
    //[SerializeField] int quantity;
    [SerializeField] ResourceType resourceT;
    [SerializeField] bool canAddItem = true;

    public void ShowCanvas()
    {
        canvas.SetActive(true);
    }

    public void HideCanvas()
    {
        canvas.SetActive(false);
    }

    public ResourceType WhatType()
    {
        return resourceT;
    }

    public bool CanGet()
    {
        return ResourcesUI.Instance.GetQuantity(resourceT) > 0;
    }

    public void /*ResourceType*/ GetItem(CharacterType playerShip)
    {
        
        if (NetworkManager.Singleton != null)
        {
            if (ResourcesUI.Instance.GetQuantity(resourceT) > 0)
            {
                ResourcesUI.Instance.ChestConsumeServerRpc(resourceT, playerShip);
                //return resourceT;
            }
            else
            {
                //return ResourceType.None;
            }
        }
        else if (ResourcesUI.Instance.ChestConsume(resourceT))
        {
            //return resourceT;
            //ConsumeConfirmed();
            Inventory.Instance.GetItem(resourceT, playerShip);
        }
        else
        {
            //return ResourceType.None;
        }

        //if (ResourcesUI.Instance.GetQuantity(resourceT) > 0)
        //{
        //    if (NetworkManager.Singleton != null)
        //    {
        //        ResourcesUI.Instance.UpdateChestQuantityServerRpc(resourceT, ResourcesUI.Instance.GetQuantity(resourceT) - 1);
        //    }
        //    else
        //    {
        //        ResourcesUI.Instance.UpdateChestQuantity(resourceT, ResourcesUI.Instance.GetQuantity(resourceT) - 1);
        //    }
        //
        //    return resourceT;
        //}

    }

    public bool CanAddItem()
    {
        return canAddItem;
    }

    public void AddItem()
    {
        if (NetworkManager.Singleton != null)
        {
            ResourcesUI.Instance.ChestAddServerRpc(resourceT);
        }
        else
        {
            ResourcesUI.Instance.ChestAdd(resourceT);
        }
    }
}
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using static InventoryResource;
using static PlayerShipMovement;

public class ResourcesUI : NetworkBehaviour
{
    [SerializeField]
    SerializableDictionaryBase<ResourceType, int> resourcesQuants = new SerializableDictionaryBase<ResourceType, int>()
    {
    };

    [SerializeField]
    SerializableDictionaryBase<ResourceType, TextMeshProUGUI> resourcesLabels = new SerializableDictionaryBase<ResourceType, TextMeshProUGUI>()
    {
    };

    public static ResourcesUI Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }


    public bool ChestConsume(ResourceType type)
    {
        if (resourcesQuants[type] > 0)
        {
            resourcesQuants[type]--;
            resourcesLabels[type].text = resourcesQuants[type].ToString();
            //Inventory.Instance.GetItem(type, chara);
            return true;
        }
        else
        {
            return false;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChestConsumeServerRpc(ResourceType type, CharacterType characterType)
    {
        if (resourcesQuants[type] > 0)
        {
            //resourcesQuants[type]--;
            //resourcesLabels[type].text = resourcesQuants[type].ToString();
            
            ChestConsumeClientRpc(type, characterType);
        }
    }

    [ClientRpc]
    public void ChestConsumeClientRpc(ResourceType type, CharacterType characterType)
    {
        if (resourcesQuants[type] > 0)
        {
            Inventory.Instance.GetItem(type, characterType);
            resourcesQuants[type]--;
            resourcesLabels[type].text = resourcesQuants[type].ToString();
        }
    }


    public void ChestAdd(ResourceType type)
    {
        resourcesQuants[type]++;
        resourcesLabels[type].text = resourcesQuants[type].ToString();
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChestAddServerRpc(ResourceType type)
    {
        resourcesQuants[type]++;
        resourcesLabels[type].text = resourcesQuants[type].ToString();
        ChestAddClientRpc(type);
    }

    [ClientRpc]
    public void ChestAddClientRpc(ResourceType type)
    {
        if (IsOwner)
        {
            return;
        }

        resourcesQuants[type]++;
        resourcesLabels[type].text = resourcesQuants[type].ToString();
    }

    //[ServerRpc(RequireOwnership = false)]
    //public void UpdateChestQuantityServerRpc(ResourceType type, int quantity)
    //{
    //    resourcesQuants[type] = quantity;
    //    resourcesLabels[type].text = quantity.ToString();
    //    UpdateChestQuantityClientRpc(type, quantity);
    //}
    //
    //[ClientRpc]
    //private void UpdateChestQuantityClientRpc(ResourceType type, int quantity)
    //{
    //    if (IsOwner)
    //    {
    //        return;
    //    }
    //
    //    resourcesQuants[type] = quantity;
    //    resourcesLabels[type].text = quantity.ToString();
    //}

    public int GetQuantity(ResourceType type)
    {
        return resourcesQuants[type];
    }
}
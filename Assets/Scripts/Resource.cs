using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using static InventoryResource;
using static PlayerShipMovement;

public class Resource : NetworkBehaviour
{
    [SerializeField]
    GameObject canvas;


    [SerializeField]
    public ResourceType resourceType = ResourceType.Stone;

    private void Awake()
    {
        if (canvas != null)
        {
            canvas.transform.SetParent(null);
        }
    }

    public bool PickUp(CharacterType characterType)
    {
        if (NetworkManager.Singleton != null)
        {
            if (Inventory.Instance.GetItem(resourceType, characterType))
            {
                DespawnResourceServerRpc();
                return true;
            }
        }
        else
        {
            if (Inventory.Instance.GetItem(resourceType, characterType))
            { 
                Destroy(transform.parent.gameObject);
                return true;
            }
        }

        return false;
    }

    [ServerRpc(RequireOwnership = false)]
    public void DespawnResourceServerRpc()
    {
        DespawnResourceClientRpc();
    }
    [ClientRpc]
    public void DespawnResourceClientRpc()
    {
        NetworkObjectDespawner.DespawnNetworkObject(NetworkObject);
    }

    public void ShowCanvas()
    {
        canvas.SetActive(true);
    }

    public void HideCanvas()
    {
        canvas.SetActive(false);
    }
}

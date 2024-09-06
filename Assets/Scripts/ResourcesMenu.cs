using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static InventoryResource;

public class ResourcesMenu : MonoBehaviour
{
    private bool active = false;

    [SerializeField]
    SerializableDictionaryBase<InventoryResource, int> resourcesQuant = new SerializableDictionaryBase<InventoryResource, int>() {
        //{ Scenes.SplashScreenScene, "Assets/_Core/Scenes/SplashScreenScene.unity" },
        //{ Scenes.DisplayNameScene, "Assets/_Core/Scenes/DisplayNameScene.unity" },
        //{ Scenes.ChampionMintScene, "Assets/_Core/Scenes/ChampionMintScene.unity" },
        //{ Scenes.ChampionMintedScene, "Assets/_Core/Scenes/ChampionMintedScene.unity" },
        //{ Scenes.MainMenuScene, "Assets/_Core/Scenes/MainMenuScene.unity" },
        //{ Scenes.GameScene, "Assets/_Core/Scenes/GameScene.unity" },
        //{ Scenes.CageEnvironmentScene, "Assets/_Core/Scenes/Environment_Scene.unity" }
    };

    [SerializeField]
    SerializableDictionaryBase<InventoryResource, TextMeshProUGUI> resourcesLabels = new SerializableDictionaryBase<InventoryResource, TextMeshProUGUI>()
    {
        //{ Scenes.SplashScreenScene, "Assets/_Core/Scenes/SplashScreenScene.unity" },
        //{ Scenes.DisplayNameScene, "Assets/_Core/Scenes/DisplayNameScene.unity" },
        //{ Scenes.ChampionMintScene, "Assets/_Core/Scenes/ChampionMintScene.unity" },
        //{ Scenes.ChampionMintedScene, "Assets/_Core/Scenes/ChampionMintedScene.unity" },
        //{ Scenes.MainMenuScene, "Assets/_Core/Scenes/MainMenuScene.unity" },
        //{ Scenes.GameScene, "Assets/_Core/Scenes/GameScene.unity" },
        //{ Scenes.CageEnvironmentScene, "Assets/_Core/Scenes/Environment_Scene.unity" }
    };

    public static ResourcesMenu Instance;
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

    public void Show()
    {
        if (!active)
        {
            gameObject.SetActive(true);
            active = true;
        }
    }


    public void Hide()
    {
        if (active)
        {
            gameObject.SetActive(false);
            active = false;
        }
    }

    public bool GetItem(ResourceType type)
    {
        foreach (var item in resourcesQuant)
        {
            if (item.Value == null)
            {
                //Slot has space
                //resourcesQuant[item.Key] = Instantiate(resourcesQuant[type], item.Key.transform); // Instantiate the resource in the inventory slot
                return true;
            }
        }

        //No slot has space
        return false;
    }

    public bool UseItem(ResourceType type)
    {
        foreach (var item in resourcesQuant.Reverse())
        {
            //if (item.Value != null && item.Value.resourceType == type)
            {
                // Has item type, use it
                //Destroy(item.Value);
                //resourcesQuant[item.Key] = null;

                return true;
            }
        }

        return false;
    }
}

using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveOverworld(MapManager mapManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/overworld.sav";

        FileStream stream = new FileStream(path, FileMode.Create);

        OverworldData data = new OverworldData(mapManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static OverworldData LoadOverworld()
    {
        string path = Application.persistentDataPath + "/overworld.sav";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            OverworldData data = (OverworldData)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }

    public static void DeleteOverworld()
    {
        string path = Application.persistentDataPath + "/overworld.sav";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public static void SaveInventory(Inventory inventory, EquipmentManager equipment)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/inventory.sav";

        FileStream stream = new FileStream(path, FileMode.Create);

        InventoryData data = new InventoryData(inventory, equipment);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static InventoryData LoadInventory()
    {
        string path = Application.persistentDataPath + "/inventory.sav";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            InventoryData data = (InventoryData)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }

    public static void DeleteInventory()
    {
        string path = Application.persistentDataPath + "/inventory.sav";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

}

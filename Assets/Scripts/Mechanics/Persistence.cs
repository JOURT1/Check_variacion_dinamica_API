using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
 
public class Inventory
{
    public string id;
    public string name;
    public string type;
    public int weight;
 
    public override string ToString()
    {
        return $"ID: {id}, Name: {name}, Type: {type}, Weight: {weight}";
    }
}
 
public class Persistence : MonoBehaviour
{
    private Inventory inventory;
 
    private void Start()
    {
        inventory = new Inventory();
 
        inventory.id = "1";
        inventory.name = "Sword";
        inventory.type = "Weapon";
        inventory.weight = 5;
    }
 
    public void SaveInventory()
    {
        string json = JsonUtility.ToJson(inventory);
        //Debug.Log(json);
 
        using (StreamWriter wr = new StreamWriter(Application.dataPath + Path.AltDirectorySeparatorChar + "Inventory.json"))
        {
            wr.Write(json);
        }
    }
 
    public void LoadInventory()
    {
        string json = string.Empty;
 
        using (StreamReader rd = new StreamReader(Application.dataPath + Path.AltDirectorySeparatorChar + "Inventory.json"))
        {
            json = rd.ReadToEnd();
        }
 
        Inventory inventoryData = JsonUtility.FromJson<Inventory>(json);
        Debug.Log(inventoryData.ToString());
    }
 
    public void IncrementScore()
    {
        SaveInventory();
    }
}
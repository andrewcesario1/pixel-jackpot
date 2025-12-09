using System.Collections.Generic;

[System.Serializable]
public class ItemSaveData
{
    public string ItemName;
    public float TimeLeft;
}

[System.Serializable]
public class ItemSaveDataWrapper
{
    public List<ItemSaveData> Items;
}

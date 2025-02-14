using UnityEngine;

public class LevelInfo : Singleton<LevelInfo>
{
    [SerializeField]private int parNumber;

    public int GetParNumber()
    {
        return parNumber;
    }
}

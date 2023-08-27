
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public int eCrystals { get; private set; } = 0;


    public void AddECrystals(int value)
    {
        if (value == 0)
            return;
        eCrystals += value;
        SaveLoadManager.Instance.SaveObject.eCrystals = eCrystals;
    }

    public bool RemoveECrystal(int value)
    {
        if ((eCrystals - value) < 0)
            return false;

        AddECrystals(-value);
        return true;
    }

    private void Awake()
    {
        
    }

    private void Start()
    {       
        eCrystals = SaveLoadManager.Instance.SaveObject.eCrystals;
    }


}

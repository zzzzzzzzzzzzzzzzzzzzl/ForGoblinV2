
using System;

public class PlayerData
{
    public int team;
    public Upgrades Upgrades = new(0);
    public event Action updateGatherRateUI;
    public event Action updateMushroomUI;
    public event Action updateCrystalUI;

    float _gatherRate=1;
    public float gatherRate
    {
        get => _gatherRate;
        set
        {
            _gatherRate = value;
            updateGatherRateUI?.Invoke();
        }
    } 
    int _mushroom;
    public int mushroom
    {
        get => _mushroom;
        set
        {
            _mushroom = value;
            updateMushroomUI?.Invoke();
        }
    }

    int _crystal=1000;
    public int crystal
    {
        get => _crystal;
        set
        {
            _crystal = value;
            updateCrystalUI?.Invoke();
        }
    }
    public bool makePurchase(string currencyType, int amount)
    {
        if (currencyType == "mushroom")
        {
            if (mushroom >= amount)
            {
                mushroom -= amount;
                return true;
            }
        }
        if (currencyType == "crystal")
        {
            if (crystal >= amount)
            {
                crystal -= amount;
                return true;
            }
        }

        return false;
    }
}
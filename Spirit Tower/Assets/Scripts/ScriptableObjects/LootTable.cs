using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Loot
{
    public PowerUp ThisLoot;
    public int LootChance;
}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] Loots;

    public PowerUp LootPowerUp()
    {
        int WeightProb = 0;
        int CurrentProb = Random.Range(0, 100);

        for (int i = 0; i < Loots.Length; i++)
        {
            WeightProb += Loots[i].LootChance;
            if (CurrentProb <= WeightProb)
            {
                return Loots[i].ThisLoot;
            }
        }
        return null;
    }
}


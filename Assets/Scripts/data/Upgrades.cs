using System;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades
{

  public Dictionary<string, Dictionary<string, int>> upgradesPurchased = new();
  public float gatherRate = 1;
  public int mushroomCount;
  public int crystalJuice;
  public static float baseCost = 10;
  public static float costMulti = 1.5f;

  public static Dictionary<string, Func<float, int, float>> increment = new()
  {
           { "health",(float baseStat,int amount)=> amount*baseStat*.1f },
            { "armour",(float baseStat,int amount)=> amount*1f },
            { "attackSpeed",(float baseStat,int amount)=> amount*baseStat*.1f },
            { "attack",(float baseStat,int amount)=> amount*baseStat*.1f },
            { "speed",(float baseStat,int amount)=> amount*baseStat*.1f },
            { "crit",(float baseStat,int amount)=> amount*5f },
            { "slowMulti",(float baseStat,int amount)=> amount*-.2f },
            { "slowDuration",(float baseStat,int amount)=> amount*baseStat*.1f },
            {  "range",(float baseStat,int amount)=> amount*baseStat*.1f },
            { "healAmount",(float baseStat,int amount)=> amount*baseStat*.1f },
            { "healSpeed",(float baseStat,int amount)=> amount*baseStat*.1f },
            {  "healRange",(float baseStat,int amount)=> amount*baseStat*.1f },
            {  "forageRange",(float baseStat,int amount)=> amount*baseStat*.1f },
            {  "capacity",(float baseStat,int amount)=> amount*1f },
            {  "splash",(float baseStat,int amount)=> amount*baseStat*.1f },
            { "summonCD",(float baseStat,int amount)=> amount*baseStat*.1f },
            { "summonSpeed",(float baseStat,int amount)=> amount*baseStat*.1f },
            { "summonHealth",(float baseStat,int amount)=> amount*baseStat*.1f },
            { "summonAttack",(float baseStat,int amount)=> amount*baseStat*.1f },
            { "summonAttackSpeed",(float baseStat,int amount)=> amount*baseStat*.1f },
   };
  public static Dictionary<string, List<string>> includeStats = new()
    {
         {"Archer",new (){
            "attackSpeed",
            "attack",
            "speed",
            "slowMulti",
            "range"
         }
            },
        { "Healer",new(){
            "healAmount",
            "healSpeed",
            }
          },
        {"Macer",new(){
            "health",
            "attackSpeed",
            "attack",
            "crit",
        }
          },
        {"Magi",new(){
            "attackSpeed",
            "attack",
             "splash",
        }
          },
        {"Shielder",new (){
            "health",
            "armour",
            "attackSpeed",
            "attack",
            }
          },
        {
    "Spearer",new(){
            "health",
            "attackSpeed",
            "attack",
            "speed",
    }
          },
        {
    "Sporager",new(){
            "speed",
             "capacity",
            }
          },
        {
    "Summoner",new(){
            "summonCD",
            "summonHealth",
            "summonAttack",
            }
          },
        {
    "Swordsman",new(){
            "health",
            "attackSpeed",
            "attack",
            "speed",
            }
          },

    };
  public static Dictionary<string, float> statBarFillPercentage = new()
  {
            {"health",.01f},
            {"armour",.10f},
            {"attackSpeed",.60f},
            {"attack",.3f},
            {"speed",.20f},
            {"crit",.1f},
            {"slowMulti",-1},
            {"slowDuration",-1},
             {"range",.1f},
            {"healAmount",.3f},
            {"healSpeed",.20f},
             {"healRange",-1},
             {"forageRange",-1},
             {"capacity",-1},
             {"splash",-1},
            {"summonCD",-1},
            {"summonSpeed",-1},
            {"summonHealth",.20f},
            {"summonAttack",.6f},
            {"summonAttackSpeed",-1},
   };
  public static Dictionary<string, Color> statBarColors = new()
  {
            {"health", Color.green},
            {"armour", Color.gray},
            {"attackSpeed", Color.yellow},
            {"attack", Color.red},
            {"speed", Color.blue},
            {"crit", new Color(1, 0.5f, 0)},
            {"slowMulti", new Color(0.5f, 0, 0.5f)},
            {"slowDuration", new Color(0.5f, 0, 0.5f)},
            {"range", new Color(0, 0.5f, 1)},
            {"healAmount", new Color(1, 0.5f, 1)},
            {"healSpeed", new Color(1, 0.5f, 1)},
            {"healRange", new Color(1, 0.5f, 1)},
            {"forageRange", new Color(1, 0.5f, 1)},
            {"capacity", new Color(1, 0.5f, 1)},
            {"splash", new Color(1, 0.5f, 1)},
            {"summonCD", new Color(1, 0.5f, 1)},
            {"summonSpeed", new Color(1, 0.5f, 1)},
            {"summonHealth", new Color(1, 0.5f, 1)},
            {"summonAttack", new Color(1, 0.5f, 1)},
            {"summonAttackSpeed", new Color(1, 0.5f, 1)}
   };


  public Upgrades(int baseAmount = 0)
  {
    initUpgrades(baseAmount);
  }
  void initUpgrades(int baseUpgrades)
  {
    foreach (var units in unitData.unitStats)
    {
      upgradesPurchased[units.Key] = new();
      foreach (var stats in unitData.unitStats[units.Key])
      {
        upgradesPurchased[units.Key][stats.Key] = baseUpgrades;
      }
    }
    upgradesPurchased["gatherRate"] = new() { { "gatherRate", 0 } };
  }
  public int getCost(string unit, string stat)
  {
    return (int)baseCost * (upgradesPurchased[unit][stat] + 1);
  }
}

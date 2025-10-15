using System.Collections.Generic;


public static class unitData
{
  public static List<string> statNames = new()
  {
            "health",
            "armour",
            "attackSpeed",
            "attack",
            "speed",
            "crit",
            "slowMulti",
            "slowDuration",
             "range",
            "healAmount",
            "healSpeed",
             "healRange",
             "forageRange",
             "capacity",
             "splash",
            "summonCD",
            "summonSpeed",
            "summonHealth",
            "summonAttack",
            "summonAttackSpeed",
   };
  public static Dictionary<string, Dictionary<string, float>> unitStats = new()
    {
        {"Archer",new Dictionary<string,float>{
            {"health",60 },
            {"armour",0 },
            {"attackSpeed",.5f },
            {"attack",1},
            {"speed",1f },
            {"crit",5 },
            {"slowMulti",.95f },
            {"slowDuration",3 },
            { "range",6},
            { "cost",6},

            }
          },
        {"Spawner",new Dictionary<string,float>{
            {"health",100 },
            {"armour",5 },
            }
          },
        {"Mushroom",new Dictionary<string,float>{//dont worry about this one
            {"health",1 },
            }
          },

        { "Healer",new Dictionary<string,float>{
            {"health",60 },
            {"armour",0 },
            {"speed",.5f },
            {"healAmount",10 },
            {"healSpeed",1 },
            { "range",1},
            { "healRange",3},
            { "cost",7},

            }
          },
        {"Imp",new Dictionary<string,float>{
            {"health",60 },
            {"armour",60 },
            {"attackSpeed",60 },
            {"attack",60 },
            {"speed",60 },
            {"crit",60 },
            { "range",60},

            }
          },
        {"Macer",new Dictionary<string,float>{
            {"health",60 },
            {"armour",2 },
            {"attackSpeed",1 },
            {"attack",10 },
            {"speed",1 },
            {"crit",30 },
            { "range",1},
            { "cost",7},

            }
          },
        {"Magi",new Dictionary<string,float>{
            {"health",60 },
            {"armour",0 },
            {"attackSpeed",1 },
            {"attack",6 },
            {"speed",.5f },
            {"crit",5 },
            { "splash",1},
            { "range",3},
            { "cost",9},

            }
          },
        {"Shielder",new Dictionary<string,float>{
            {"health",60 },
            {"armour",5 },
            {"attackSpeed",1 },
            {"attack",10 },
            {"speed",1 },
            {"crit",10 },
            { "range",1},
            { "cost",9},

            }
          },
        {"Spearer",new Dictionary<string,float>{
            {"health",60 },
            {"armour",0 },
            {"attackSpeed",1 },
            {"attack",10 },
            {"speed",1 },
            {"crit",5 },
            { "range",1},
            { "cost",7},
            }
          },
        {"Sporager",new Dictionary<string,float>{
            {"health",60 },
            {"armour",0 },
            {"speed",1 },
            { "forageRange",12},
            { "capacity",4},
            { "cost",5},
            }


          },
        {"Summoner",new Dictionary<string,float>{
            {"health",60 },
            {"armour",0 },
            {"attackSpeed",1 },
            {"attack",5 },
            {"speed",.3f },
            {"crit",5 },
            { "range",1},
            {"summonCD",3 },
            {"summonSpeed",1 },
            {"summonHealth",20 },
            {"summonAttack",10 },
            {"summonAttackSpeed",1 },
            { "cost",9},
            }
          },
        {"Swordsman",new Dictionary<string,float>{
            {"health",60 },
            {"armour",0 },
            {"attackSpeed",1 },
            {"attack",10 },
            {"speed",1 },
            {"crit",5 },
            { "range",1},
            { "cost",3},
            }
          },

    };
}

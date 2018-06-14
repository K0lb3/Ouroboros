// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_ReqTowerResuponse
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class JSON_ReqTowerResuponse
  {
    public long rtime;
    public JSON_ReqTowerResuponse.Json_TowerStatus stats;
    public JSON_ReqTowerResuponse.Json_TowerPlayerUnit[] pdeck;
    public JSON_ReqTowerResuponse.Json_TowerEnemyUnit[] edeck;
    public int[] lot_enemies;
    public short reset_cost;
    public byte round;
    public byte is_reset;
    public int turn_num;
    public int died_num;
    public int retire_num;
    public int recover_num;
    public JSON_ReqTowerResuponse.Json_RankStatus rank;

    public class Json_TowerStatus
    {
      public string fname;
      public string state;

      public QuestStates questStates
      {
        get
        {
          string state = this.state;
          if (state != null)
          {
            if (JSON_ReqTowerResuponse.Json_TowerStatus.\u003C\u003Ef__switch\u0024mapD == null)
              JSON_ReqTowerResuponse.Json_TowerStatus.\u003C\u003Ef__switch\u0024mapD = new Dictionary<string, int>(4)
              {
                {
                  "win",
                  0
                },
                {
                  "lose",
                  1
                },
                {
                  "ritire",
                  1
                },
                {
                  "cancel",
                  1
                }
              };
            int num;
            if (JSON_ReqTowerResuponse.Json_TowerStatus.\u003C\u003Ef__switch\u0024mapD.TryGetValue(state, out num))
            {
              if (num == 0)
                return QuestStates.Cleared;
              if (num == 1)
                return QuestStates.Challenged;
            }
          }
          return QuestStates.New;
        }
        set
        {
          switch (value)
          {
            case QuestStates.New:
            case QuestStates.Challenged:
              this.state = "ritire";
              break;
            case QuestStates.Cleared:
              this.state = "win";
              break;
          }
        }
      }
    }

    public class Json_TowerPlayerUnit
    {
      public string uname;
      public int damage;
      public int is_died;
    }

    public class Json_TowerEnemyUnit
    {
      public int eid;
      public int hp;
      public int jewel;
    }

    public class Json_RankStatus
    {
      public int turn_num;
      public int died_num;
      public int retire_num;
      public int recovery_num;
      public int spd_rank;
      public int tec_rank;
      public int spd_score;
      public int tec_score;
      public int ret_score;
      public int rcv_score;
    }
  }
}

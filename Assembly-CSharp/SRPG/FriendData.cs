// Decompiled with JetBrains decompiler
// Type: SRPG.FriendData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;

namespace SRPG
{
  public class FriendData
  {
    public UnitData Unit;
    public FriendStates State;
    public string UID;
    public string FUID;
    public string PlayerName;
    public int PlayerLevel;
    public long LastLogin;
    public string CreatedAt;
    public bool IsFavorite;
    public string UnitID;
    public int UnitLevel;
    public int UnitRarity;
    public string SelectAward;
    public string Wish;
    public string WishStatus;
    public bool MultiPush;
    public string MultiComment;

    public EElement UnitElement
    {
      get
      {
        UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(this.UnitID);
        if (unitParam != null)
          return unitParam.element;
        return EElement.None;
      }
    }

    public void Deserialize(Json_Friend json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.UID = json.uid;
      this.FUID = json.fuid;
      this.PlayerName = json.name;
      this.PlayerLevel = json.lv;
      this.LastLogin = json.lastlogin;
      this.CreatedAt = json.created_at;
      this.IsFavorite = json.is_favorite != 0;
      this.SelectAward = json.award;
      this.Wish = json.wish;
      this.WishStatus = json.status;
      this.MultiPush = json.is_multi_push == 1;
      this.MultiComment = json.multi_comment;
      if (json.unit != null)
      {
        this.UnitID = json.unit.iname;
        this.UnitLevel = json.unit.lv;
        this.UnitRarity = json.unit.rare;
        UnitData unitData = new UnitData();
        unitData.Deserialize(json.unit);
        this.Unit = unitData;
      }
      string type = json.type;
      if (type != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (FriendData.\u003C\u003Ef__switch\u0024map11 == null)
        {
          // ISSUE: reference to a compiler-generated field
          FriendData.\u003C\u003Ef__switch\u0024map11 = new Dictionary<string, int>(3)
          {
            {
              "friend",
              0
            },
            {
              "follow",
              1
            },
            {
              "follower",
              2
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (FriendData.\u003C\u003Ef__switch\u0024map11.TryGetValue(type, out num))
        {
          switch (num)
          {
            case 0:
              this.State = FriendStates.Friend;
              return;
            case 1:
              this.State = FriendStates.Follow;
              return;
            case 2:
              this.State = FriendStates.Follwer;
              return;
          }
        }
      }
      this.State = FriendStates.None;
    }

    public bool IsFriend()
    {
      return this.State == FriendStates.Friend;
    }

    public int GetCost()
    {
      if (this.Unit == null)
        return 0;
      int num = this.Unit.Lv * (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.SupportCost;
      if (this.State != FriendStates.Friend)
        num *= 2;
      return num;
    }
  }
}

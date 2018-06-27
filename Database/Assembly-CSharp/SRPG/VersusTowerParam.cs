// Decompiled with JetBrains decompiler
// Type: SRPG.VersusTowerParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG
{
  public class VersusTowerParam
  {
    public static readonly int RANK_RANGE = 4;
    public static readonly int RANK_NUM = 7;
    public OString VersusTowerID;
    public OString FloorName;
    public OInt Floor;
    public OInt RankupNum;
    public OInt WinNum;
    public OInt LoseNum;
    public OInt BonusNum;
    public OInt DownFloor;
    public OInt ResetFloor;
    public OString[] WinIteminame;
    public VERSUS_ITEM_TYPE[] WinItemType;
    public OInt[] WinItemNum;
    public OString[] JoinIteminame;
    public VERSUS_ITEM_TYPE[] JoinItemType;
    public OInt[] JoinItemNum;
    public OString[] SpIteminame;
    public VERSUS_ITEM_TYPE[] SpItemType;
    public OInt[] SpItemnum;
    public OString[] SeasonIteminame;
    public VERSUS_ITEM_TYPE[] SeasonItemType;
    public OInt[] SeasonItemnum;
    public OString ArrivalIteminame;
    public VERSUS_ITEM_TYPE ArrivalItemType;
    public OInt ArrivalItemNum;

    public void Deserialize(JSON_VersusTowerParam json)
    {
      if (json == null)
        return;
      this.VersusTowerID = (OString) json.vstower_id;
      this.FloorName = (OString) json.iname;
      this.Floor = (OInt) json.floor;
      this.RankupNum = (OInt) json.rankup_num;
      this.WinNum = (OInt) json.win_num;
      this.LoseNum = (OInt) json.lose_num;
      this.BonusNum = (OInt) json.bonus_num;
      this.DownFloor = (OInt) json.downfloor;
      this.ResetFloor = (OInt) json.resetfloor;
      if (json.winitem != null && json.win_itemnum != null)
      {
        if (json.winitem.Length != json.win_itemnum.Length)
          Debug.LogError((object) "VersusTower Param [ WinItem ] is Invalid");
        this.WinIteminame = new OString[json.winitem.Length];
        this.WinItemNum = new OInt[json.win_itemnum.Length];
        for (int index = 0; index < json.winitem.Length; ++index)
          this.WinIteminame[index] = (OString) json.winitem[index];
        for (int index = 0; index < json.win_itemnum.Length; ++index)
          this.WinItemNum[index] = (OInt) json.win_itemnum[index];
      }
      if (json.joinitem != null && json.join_itemnum != null)
      {
        if (json.joinitem.Length != json.join_itemnum.Length)
          Debug.LogError((object) "VersusTower Param [ LoseItem ] is Invalid");
        this.JoinIteminame = new OString[json.joinitem.Length];
        this.JoinItemNum = new OInt[json.join_itemnum.Length];
        for (int index = 0; index < json.joinitem.Length; ++index)
          this.JoinIteminame[index] = (OString) json.joinitem[index];
        for (int index = 0; index < json.join_itemnum.Length; ++index)
          this.JoinItemNum[index] = (OInt) json.join_itemnum[index];
      }
      if (json.spbtl_item != null && json.spbtl_itemnum != null)
      {
        if (json.spbtl_item.Length != json.spbtl_itemnum.Length)
          Debug.LogError((object) "VersusTower Param [ SpecialItem ] is Invalid");
        this.SpIteminame = new OString[json.spbtl_item.Length];
        this.SpItemnum = new OInt[json.spbtl_itemnum.Length];
        for (int index = 0; index < json.spbtl_item.Length; ++index)
          this.SpIteminame[index] = (OString) json.spbtl_item[index];
        for (int index = 0; index < json.spbtl_itemnum.Length; ++index)
          this.SpItemnum[index] = (OInt) json.spbtl_itemnum[index];
      }
      if (json.season_item != null && json.season_itemnum != null && json.season_itype != null)
      {
        if (json.season_item.Length != json.season_itemnum.Length)
          Debug.LogError((object) "VersusTower Param [ SeasonItem ] is Invalid");
        this.SeasonIteminame = new OString[json.season_item.Length];
        this.SeasonItemType = new VERSUS_ITEM_TYPE[json.season_itype.Length];
        this.SeasonItemnum = new OInt[json.season_itemnum.Length];
        for (int index = 0; index < json.season_item.Length; ++index)
          this.SeasonIteminame[index] = (OString) json.season_item[index];
        for (int index = 0; index < json.season_itype.Length; ++index)
          this.SeasonItemType[index] = (VERSUS_ITEM_TYPE) Enum.Parse(typeof (VERSUS_ITEM_TYPE), json.season_itype[index], true);
        for (int index = 0; index < json.season_itemnum.Length; ++index)
          this.SeasonItemnum[index] = (OInt) json.season_itemnum[index];
      }
      if (string.IsNullOrEmpty(json.arrival_item))
        return;
      this.ArrivalIteminame = (OString) json.arrival_item;
      this.ArrivalItemType = (VERSUS_ITEM_TYPE) Enum.Parse(typeof (VERSUS_ITEM_TYPE), json.arrival_type, true);
      this.ArrivalItemNum = (OInt) json.arrival_num;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: SRPG.GachaResultData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class GachaResultData
  {
    private static List<GachaDropData> drops_ = new List<GachaDropData>();
    private static List<GachaDropData> dropMails_ = new List<GachaDropData>();
    private static int[] excites_ = new int[5];
    private static bool use_one_more_ = false;

    public static GachaDropData[] drops
    {
      get
      {
        return GachaResultData.drops_.ToArray();
      }
    }

    public static GachaDropData[] dropMails
    {
      get
      {
        return GachaResultData.dropMails_.ToArray();
      }
    }

    public static int[] excites
    {
      get
      {
        return GachaResultData.excites_;
      }
    }

    public static GachaReceiptData receipt { get; private set; }

    public static bool IsCoin
    {
      get
      {
        return GachaResultData.receipt == null || !(GachaResultData.receipt.type == "gold");
      }
    }

    public static bool UseOneMore
    {
      get
      {
        return GachaResultData.use_one_more_;
      }
    }

    private static void Reset()
    {
      GachaResultData.drops_.Clear();
      GachaResultData.dropMails_.Clear();
      for (int index = 0; index < GachaResultData.excites_.Length; ++index)
        GachaResultData.excites_[index] = 1;
      GachaResultData.receipt = (GachaReceiptData) null;
      GachaResultData.use_one_more_ = false;
    }

    public static void Init(List<GachaDropData> a_drops = null, List<GachaDropData> a_dropMails = null, GachaReceiptData a_receipt = null, bool a_use_onemore = false)
    {
      GachaResultData.Reset();
      if (a_drops != null)
        GachaResultData.drops_ = a_drops;
      if (a_dropMails != null)
        GachaResultData.dropMails_ = a_dropMails;
      GachaResultData.excites_ = GachaResultData.CalcExcites(a_drops);
      if (a_receipt != null)
        GachaResultData.receipt = a_receipt;
      using (List<GachaDropData>.Enumerator enumerator = GachaResultData.drops_.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          GachaDropData current = enumerator.Current;
          current.excites = GachaResultData.CalcExcitesForDrop(current);
        }
      }
      GachaResultData.use_one_more_ = a_use_onemore;
    }

    public static int[] CalcExcites(List<GachaDropData> a_drops)
    {
      int num = 1;
      using (List<GachaDropData>.Enumerator enumerator = a_drops.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          GachaDropData current = enumerator.Current;
          if (current != null)
            num = Math.Max(num, current.Rare);
        }
      }
      return GachaExciteMaster.Select(JSONParser.parseJSONArray<Json_GachaExcite>(AssetManager.LoadTextData("Data/gacha/animation_pattern")), num);
    }

    public static int[] CalcExcitesForDrop(GachaDropData a_drop)
    {
      int rare = 0;
      if (a_drop != null)
        rare = a_drop.Rare;
      return GachaExciteMaster.SelectStone(JSONParser.parseJSONArray<Json_GachaExcite>(AssetManager.LoadTextData("Data/gacha/stone_animation_pattern")), rare);
    }
  }
}

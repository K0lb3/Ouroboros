// Decompiled with JetBrains decompiler
// Type: SRPG.GachaResultData
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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

    private static void Reset()
    {
      GachaResultData.drops_.Clear();
      GachaResultData.dropMails_.Clear();
      for (int index = 0; index < GachaResultData.excites_.Length; ++index)
        GachaResultData.excites_[index] = 1;
      GachaResultData.receipt = (GachaReceiptData) null;
    }

    public static void Init(List<GachaDropData> a_drops = null, List<GachaDropData> a_dropMails = null, GachaReceiptData a_receipt = null)
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

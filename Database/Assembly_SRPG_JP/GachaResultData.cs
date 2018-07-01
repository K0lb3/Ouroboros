// Decompiled with JetBrains decompiler
// Type: SRPG.GachaResultData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class GachaResultData
  {
    private static List<GachaDropData> drops_ = new List<GachaDropData>();
    private static List<GachaDropData> dropMails_ = new List<GachaDropData>();
    private static List<int> summonCoins_ = new List<int>();
    private static int[] excites_ = new int[5];
    private static bool use_one_more_ = false;
    private static int m_is_pending = 0;
    private static int m_redraw_rest = 0;

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

    public static List<int> summonCoins
    {
      get
      {
        return GachaResultData.summonCoins_;
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

    public static bool IsPending
    {
      get
      {
        return GachaResultData.m_is_pending == 1;
      }
    }

    public static int RedrawRest
    {
      get
      {
        return GachaResultData.m_redraw_rest;
      }
    }

    public static bool IsRedrawGacha
    {
      get
      {
        return GachaResultData.m_is_pending != -1;
      }
    }

    public static void Reset()
    {
      GachaResultData.drops_.Clear();
      GachaResultData.dropMails_.Clear();
      GachaResultData.summonCoins_.Clear();
      for (int index = 0; index < GachaResultData.excites_.Length; ++index)
        GachaResultData.excites_[index] = 1;
      GachaResultData.receipt = (GachaReceiptData) null;
      GachaResultData.use_one_more_ = false;
      GachaResultData.m_is_pending = -1;
      GachaResultData.m_redraw_rest = -1;
    }

    public static void Init(List<GachaDropData> a_drops = null, List<GachaDropData> a_dropMails = null, List<int> a_summonCoins = null, GachaReceiptData a_receipt = null, bool a_use_onemore = false, int a_is_pending = -1, int a_redraw_rest = -1)
    {
      GachaResultData.Reset();
      if (a_drops != null)
        GachaResultData.drops_ = a_drops;
      if (a_dropMails != null)
        GachaResultData.dropMails_ = a_dropMails;
      if (a_summonCoins != null)
        GachaResultData.summonCoins_ = a_summonCoins;
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
      GachaResultData.m_is_pending = a_is_pending;
      GachaResultData.m_redraw_rest = a_redraw_rest;
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

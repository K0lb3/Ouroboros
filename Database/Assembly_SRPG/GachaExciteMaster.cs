// Decompiled with JetBrains decompiler
// Type: SRPG.GachaExciteMaster
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class GachaExciteMaster
  {
    public static int[] Select(Json_GachaExcite[] json, int rare)
    {
      int maxValue = 0;
      foreach (Json_GachaExcite jsonGachaExcite in json)
      {
        if (rare == jsonGachaExcite.fields.rarity)
          maxValue += jsonGachaExcite.fields.weight;
      }
      int num1 = new Random().Next(maxValue);
      int num2 = 0;
      foreach (Json_GachaExcite jsonGachaExcite in json)
      {
        if (rare == jsonGachaExcite.fields.rarity)
        {
          num2 += jsonGachaExcite.fields.weight;
          if (num1 < num2)
            return new int[5]{ GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color1), GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color2), GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color3), GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color4), GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color5) };
        }
      }
      return new int[5]{ 1, 1, 1, 1, 1 };
    }

    public static int[] SelectStone(Json_GachaExcite[] json, int rare)
    {
      int maxValue = 0;
      foreach (Json_GachaExcite jsonGachaExcite in json)
      {
        if (rare == jsonGachaExcite.fields.rarity)
          maxValue += jsonGachaExcite.fields.weight;
      }
      int num1 = new Random().Next(maxValue);
      int num2 = 0;
      foreach (Json_GachaExcite jsonGachaExcite in json)
      {
        if (rare == jsonGachaExcite.fields.rarity)
        {
          num2 += jsonGachaExcite.fields.weight;
          if (num1 < num2)
            return new int[3]{ GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color1), GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color2), GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color3) };
        }
      }
      return new int[3]{ 1, 1, 1 };
    }

    private static int ColorString2Int(string cstr)
    {
      string key = cstr;
      if (key != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (GachaExciteMaster.\u003C\u003Ef__switch\u0024mapE == null)
        {
          // ISSUE: reference to a compiler-generated field
          GachaExciteMaster.\u003C\u003Ef__switch\u0024mapE = new Dictionary<string, int>(3)
          {
            {
              "blue",
              0
            },
            {
              "yellow",
              1
            },
            {
              "red",
              2
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (GachaExciteMaster.\u003C\u003Ef__switch\u0024mapE.TryGetValue(key, out num))
        {
          switch (num)
          {
            case 0:
              return 1;
            case 1:
              return 2;
            case 2:
              return 3;
          }
        }
      }
      DebugUtility.LogError("Invalid color string.");
      return 0;
    }
  }
}

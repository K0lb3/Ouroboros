// Decompiled with JetBrains decompiler
// Type: SRPG.GachaManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class GachaManager : MonoSingleton<GachaManager>
  {
    public GameObject GachaPanel;
    private List<GachaTopParam> GachaList;
    public Button NextGachaButton;
    public Button PrevGachaButton;
    private List<GachaTopParam2> mGachaListRare;
    private List<GachaTopParam2> mGachaListNormal;
    private List<GachaTopParam2> mGachaListTicket;
    private List<GachaTopParam2> mGachaListArtifact;
    private List<GachaTopParam2> mGachaListSpecial;
    private int mUseTicketNum;
    private string mUseTicketIname;
    private int mCurrentGachaIndex;
    private bool mInitalize;

    public GachaTopParam2[] GachaListRare
    {
      get
      {
        return this.mGachaListRare.ToArray();
      }
    }

    public GachaTopParam2[] GachaListNormal
    {
      get
      {
        return this.mGachaListNormal.ToArray();
      }
    }

    public GachaTopParam2[] GachaListTicket
    {
      get
      {
        return this.mGachaListTicket.ToArray();
      }
    }

    public GachaTopParam2[] GachaListArtifact
    {
      get
      {
        return this.mGachaListArtifact.ToArray();
      }
    }

    public GachaTopParam2[] GachaListSpecial
    {
      get
      {
        return this.mGachaListSpecial.ToArray();
      }
    }

    public int UseTicketNum
    {
      get
      {
        return this.mUseTicketNum;
      }
      set
      {
        this.mUseTicketNum = value;
      }
    }

    public string UseTicketIname
    {
      get
      {
        return this.mUseTicketIname;
      }
      set
      {
        this.mUseTicketIname = value;
      }
    }

    public int CurrentGachaIndex
    {
      get
      {
        return this.mCurrentGachaIndex;
      }
      set
      {
        this.mCurrentGachaIndex = value;
      }
    }

    public GachaTopParam GetCurrentGacha()
    {
      return this.GachaList[this.CurrentGachaIndex];
    }

    protected override void Initialize()
    {
      base.Initialize();
    }

    private void Start()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (instance.Gachas == null || instance.Gachas.Length <= 0)
        return;
      this.SetupGachaList2(instance.Gachas);
      this.mInitalize = true;
    }

    public void RefreshGachaList()
    {
    }

    private void OnShiftGacha(Button button)
    {
      if (!this.mInitalize)
        return;
      int num = !UnityEngine.Object.op_Equality((UnityEngine.Object) button, (UnityEngine.Object) this.NextGachaButton) ? 1 : -1;
      int count = this.GachaList.Count;
      this.StartCoroutine(this.ShiftGachaAsync((this.mCurrentGachaIndex + num + count) % count));
    }

    [DebuggerHidden]
    private IEnumerator ShiftGachaAsync(int index)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaManager.\u003CShiftGachaAsync\u003Ec__Iterator75()
      {
        index = index,
        \u003C\u0024\u003Eindex = index,
        \u003C\u003Ef__this = this
      };
    }

    private void SetupGachaList2(GachaParam[] gparams)
    {
      this.mGachaListRare = new List<GachaTopParam2>();
      this.mGachaListNormal = new List<GachaTopParam2>();
      this.mGachaListArtifact = new List<GachaTopParam2>();
      this.mGachaListTicket = new List<GachaTopParam2>();
      this.mGachaListSpecial = new List<GachaTopParam2>();
      for (int index = 0; index < gparams.Length; ++index)
      {
        GachaTopParam2 gachaTopParam2_1 = new GachaTopParam2();
        GachaTopParam2 gachaTopParam2_2 = this.Deserialize(gparams[index]);
        if (gparams[index].category.Contains("coin"))
          this.mGachaListRare.Add(gachaTopParam2_2);
        else if (gparams[index].category.Contains("gold"))
          this.mGachaListNormal.Add(gachaTopParam2_2);
        else if (gparams[index].group.Contains("bugu-"))
          this.mGachaListArtifact.Add(gachaTopParam2_2);
        else if (!string.IsNullOrEmpty(gparams[index].ticket_iname))
          this.mGachaListTicket.Add(gachaTopParam2_2);
        else
          this.mGachaListSpecial.Add(gachaTopParam2_2);
      }
    }

    private GachaTopParam2 Deserialize(GachaParam param)
    {
      if (param == null)
        return (GachaTopParam2) null;
      return new GachaTopParam2()
      {
        iname = param.iname,
        category = param.category,
        coin = param.coin,
        coin_p = param.coin_p,
        gold = param.gold,
        num = param.num,
        ticket = param.ticket_iname,
        ticket_num = param.ticket_num,
        units = param.units,
        step = param.step,
        step_index = param.step_index,
        step_num = param.step_num,
        limit = param.limit,
        limit_num = param.limit_num,
        limit_stock = param.limit_stock,
        type = string.Empty,
        asset_bg = param.asset_bg,
        asset_title = param.asset_title,
        group = param.group,
        btext = param.btext,
        confirm = param.confirm
      };
    }

    private void SetupGachaList(GachaParam[] gparams)
    {
      this.GachaList = new List<GachaTopParam>();
      for (int index1 = 0; index1 < gparams.Length; ++index1)
      {
        string group = gparams[index1].group;
        int index2 = 0;
        GachaTopParam gachaTopParam;
        if (this.GachaList != null && !string.IsNullOrEmpty(group) && this.GachaList.FindIndex((Predicate<GachaTopParam>) (s => s.group == group)) != -1)
        {
          gachaTopParam = this.GachaList[this.GachaList.FindIndex((Predicate<GachaTopParam>) (s => s.group == group))];
          index2 = Array.IndexOf<string>(gachaTopParam.iname, (string) null);
        }
        else
          gachaTopParam = new GachaTopParam();
        gachaTopParam.iname[index2] = gparams[index1].iname;
        gachaTopParam.category[index2] = gparams[index1].category;
        gachaTopParam.coin[index2] = gparams[index1].coin;
        gachaTopParam.gold[index2] = gparams[index1].gold;
        gachaTopParam.coin_p[index2] = gparams[index1].coin_p;
        gachaTopParam.num[index2] = gparams[index1].num;
        gachaTopParam.ticket[index2] = string.IsNullOrEmpty(gparams[index1].ticket_iname) ? string.Empty : gparams[index1].ticket_iname;
        gachaTopParam.ticket_num[index2] = gparams[index1].ticket_num;
        gachaTopParam.units = gparams[index1].units;
        gachaTopParam.step[index2] = gparams[index1].step;
        gachaTopParam.step_index[index2] = gparams[index1].step_index;
        gachaTopParam.step_num[index2] = gparams[index1].step_num;
        gachaTopParam.limit[index2] = gparams[index1].limit;
        gachaTopParam.limit_num[index2] = gparams[index1].limit_num;
        gachaTopParam.limit_stock[index2] = gparams[index1].limit_stock;
        gachaTopParam.type = string.Empty;
        gachaTopParam.asset_bg = string.IsNullOrEmpty(gparams[index1].asset_bg) ? string.Empty : gparams[index1].asset_bg;
        gachaTopParam.asset_title = string.IsNullOrEmpty(gparams[index1].asset_title) ? string.Empty : gparams[index1].asset_title;
        gachaTopParam.group = group;
        gachaTopParam.btext[index2] = gparams[index1].btext;
        gachaTopParam.confirm[index2] = gparams[index1].confirm;
        if (gachaTopParam.coin_p[index2] > 0)
          gachaTopParam.sort.Insert(0, index2);
        else
          gachaTopParam.sort.Add(index2);
        if (index2 == 0)
          this.GachaList.Add(gachaTopParam);
      }
    }

    private int GetGachaParamIndex(List<GachaTopParam> list, string iname)
    {
      for (int index = 0; index < list.Count; ++index)
      {
        GachaTopParam gachaTopParam = list[index];
        if (gachaTopParam != null && gachaTopParam.iname != null)
        {
          foreach (string str in gachaTopParam.iname)
          {
            if (str == iname)
              return index;
          }
        }
      }
      return 0;
    }
  }
}

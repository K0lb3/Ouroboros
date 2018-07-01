// Decompiled with JetBrains decompiler
// Type: SRPG.StatusList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class StatusList : MonoBehaviour
  {
    public StatusListItem ListItem;
    public bool ShowSign;
    private List<StatusListItem> mItems;
    private Color mDefaultValueColor;
    private Color mDefaultBonusColor;

    public StatusList()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ListItem.Value, (UnityEngine.Object) null))
        this.mDefaultValueColor = ((Graphic) this.ListItem.Value).get_color();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ListItem.Bonus, (UnityEngine.Object) null))
        return;
      this.mDefaultBonusColor = ((Graphic) this.ListItem.Bonus).get_color();
    }

    private void Start()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null) || !((Component) this.ListItem).get_gameObject().get_activeInHierarchy())
        return;
      ((Component) this.ListItem).get_gameObject().SetActive(false);
    }

    public void SetValues(BaseStatus paramAdd, BaseStatus paramMul, BaseStatus modAdd, BaseStatus modMul, bool isSecret = false)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning(((Component) this).get_gameObject().GetPath((GameObject) null) + ": ListItem not set");
      }
      else
      {
        int index1 = 0;
        string[] names = Enum.GetNames(typeof (ParamTypes));
        Array values = Enum.GetValues(typeof (ParamTypes));
        for (int index2 = 0; index2 < values.Length; ++index2)
        {
          int num1 = (int) paramAdd[(ParamTypes) values.GetValue(index2)];
          int bonus1 = (int) modAdd[(ParamTypes) values.GetValue(index2)] - num1;
          if (num1 != 0 && index2 != 2)
          {
            this.AddValue(index1, names[index2], num1, bonus1, false, isSecret, false);
            ++index1;
          }
          int num2 = (int) paramMul[(ParamTypes) values.GetValue(index2)];
          int bonus2 = (int) modMul[(ParamTypes) values.GetValue(index2)] - num2;
          if (num2 != 0 && index2 != 2)
          {
            this.AddValue(index1, names[index2], num2, bonus2, true, isSecret, false);
            ++index1;
          }
        }
        for (; index1 < this.mItems.Count; ++index1)
          ((Component) this.mItems[index1]).get_gameObject().SetActive(false);
      }
    }

    public void SetValues(BaseStatus paramAdd, BaseStatus paramMul, bool isSecret = false)
    {
      this.SetValues(paramAdd, paramMul, paramAdd, paramMul, isSecret);
    }

    private void AddValue(int index, string type, int value, int bonus, bool multiply, bool isSecret = false, bool use_bonus_color = false)
    {
      if (this.mItems.Count <= index)
      {
        StatusListItem statusListItem = (StatusListItem) UnityEngine.Object.Instantiate<StatusListItem>((M0) this.ListItem);
        ((Component) statusListItem).get_transform().SetParent(((Component) this).get_transform(), false);
        this.mItems.Add(statusListItem);
      }
      StatusListItem mItem = this.mItems[index];
      ((Component) mItem).get_gameObject().SetActive(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) mItem.Value, (UnityEngine.Object) null))
      {
        ((Component) mItem.Value).get_gameObject().SetActive(false);
        ((Graphic) mItem.Value).set_color(!use_bonus_color || bonus == 0 ? this.mDefaultValueColor : this.mDefaultBonusColor);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) mItem.Bonus, (UnityEngine.Object) null))
        ((Component) mItem.Bonus).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) mItem.Label, (UnityEngine.Object) null))
        mItem.Label.set_text(LocalizedText.Get("sys." + type));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) mItem.Value, (UnityEngine.Object) null))
      {
        string str = !isSecret ? value.ToString() : "???";
        if (this.ShowSign && value > 0)
          str = "+" + str;
        if (multiply)
          str += "%";
        mItem.Value.set_text(str);
        ((Component) mItem.Value).get_gameObject().SetActive(true);
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) mItem.Bonus, (UnityEngine.Object) null) || bonus == 0 || use_bonus_color)
        return;
      string str1 = bonus.ToString();
      if (this.ShowSign && bonus > 0)
        str1 = "+" + str1;
      if (multiply)
        str1 += "%";
      mItem.Bonus.set_text(str1);
      ((Component) mItem.Bonus).get_gameObject().SetActive(true);
    }

    public void SetDirectValues(BaseStatus old_status, BaseStatus new_status)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning(((Component) this).get_gameObject().GetPath((GameObject) null) + ": ListItem not set");
      }
      else
      {
        int index1 = 0;
        string[] names = Enum.GetNames(typeof (ParamTypes));
        Array values = Enum.GetValues(typeof (ParamTypes));
        for (int index2 = 0; index2 < values.Length; ++index2)
        {
          int oldStatu = (int) old_status[(ParamTypes) values.GetValue(index2)];
          int newStatu = (int) new_status[(ParamTypes) values.GetValue(index2)];
          if (oldStatu != newStatu && index2 != 2)
          {
            this.AddValue(index1, names[index2], oldStatu, newStatu, false, false, false);
            ++index1;
          }
        }
        for (; index1 < this.mItems.Count; ++index1)
          ((Component) this.mItems[index1]).get_gameObject().SetActive(false);
      }
    }

    public void SetValuesAfterOnly(BaseStatus paramAdd, BaseStatus paramMul, BaseStatus modAdd, BaseStatus modMul, bool isSecret = false, bool use_bonus_color = false)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning(((Component) this).get_gameObject().GetPath((GameObject) null) + ": ListItem not set");
      }
      else
      {
        int index1 = 0;
        string[] names = Enum.GetNames(typeof (ParamTypes));
        Array values = Enum.GetValues(typeof (ParamTypes));
        for (int index2 = 0; index2 < values.Length; ++index2)
        {
          int num1 = (int) modAdd[(ParamTypes) values.GetValue(index2)];
          int bonus1 = num1 - (int) paramAdd[(ParamTypes) values.GetValue(index2)];
          if (num1 != 0 && index2 != 2)
          {
            this.AddValue(index1, names[index2], num1, bonus1, false, isSecret, use_bonus_color);
            ++index1;
          }
          int num2 = (int) modMul[(ParamTypes) values.GetValue(index2)];
          int bonus2 = num2 - (int) paramMul[(ParamTypes) values.GetValue(index2)];
          if (num2 != 0 && index2 != 2)
          {
            this.AddValue(index1, names[index2], num2, bonus2, true, isSecret, use_bonus_color);
            ++index1;
          }
        }
        for (; index1 < this.mItems.Count; ++index1)
          ((Component) this.mItems[index1]).get_gameObject().SetActive(false);
      }
    }

    public void SetValues_TotalAndBonus(BaseStatus paramAdd, BaseStatus paramMul, BaseStatus modAdd, BaseStatus modMul, BaseStatus paramBonusAdd, BaseStatus paramBonusMul, BaseStatus modBonusAdd, BaseStatus modBonusMul)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning(((Component) this).get_gameObject().GetPath((GameObject) null) + ": ListItem not set");
      }
      else
      {
        int index1 = 0;
        string[] names = Enum.GetNames(typeof (ParamTypes));
        Array values = Enum.GetValues(typeof (ParamTypes));
        Dictionary<ParamTypes, StatusList.ParamValues> dictionary1 = new Dictionary<ParamTypes, StatusList.ParamValues>();
        Dictionary<ParamTypes, StatusList.ParamValues> dictionary2 = new Dictionary<ParamTypes, StatusList.ParamValues>();
        for (int index2 = 0; index2 < values.Length; ++index2)
        {
          ParamTypes key = (ParamTypes) values.GetValue(index2);
          if (key != ParamTypes.HpMax)
          {
            int num1 = (int) modAdd[key] + (int) modBonusAdd[key];
            int num2 = num1 - ((int) paramAdd[key] + (int) paramBonusAdd[key]);
            if (num1 != 0)
            {
              if (!dictionary1.ContainsKey(key))
                dictionary1.Add(key, new StatusList.ParamValues());
              dictionary1[key].main_value += num1;
              dictionary1[key].is_def_main = num2 != 0;
            }
            int num3 = (int) modBonusAdd[key];
            int num4 = num3 - (int) paramBonusAdd[key];
            if (num3 != 0)
            {
              if (!dictionary1.ContainsKey(key))
                dictionary1.Add(key, new StatusList.ParamValues());
              dictionary1[key].bonus_value += num3;
              dictionary1[key].is_def_bonus = num4 != 0;
            }
          }
        }
        for (int index2 = 0; index2 < values.Length; ++index2)
        {
          ParamTypes key = (ParamTypes) values.GetValue(index2);
          if (key != ParamTypes.HpMax)
          {
            int num1 = (int) modMul[key] + (int) modBonusMul[key];
            int num2 = num1 - ((int) paramMul[key] + (int) paramBonusMul[key]);
            if (num1 != 0)
            {
              if (!dictionary2.ContainsKey(key))
                dictionary2.Add(key, new StatusList.ParamValues());
              dictionary2[key].main_value += num1;
              dictionary2[key].is_def_main = num2 != 0;
            }
            int num3 = (int) modBonusMul[key];
            int num4 = num3 - (int) paramBonusMul[key];
            if (num3 != 0)
            {
              if (!dictionary2.ContainsKey(key))
                dictionary2.Add(key, new StatusList.ParamValues());
              dictionary2[key].bonus_value += num3;
              dictionary2[key].is_def_bonus = num4 != 0;
            }
          }
        }
        using (Dictionary<ParamTypes, StatusList.ParamValues>.KeyCollection.Enumerator enumerator = dictionary1.Keys.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            ParamTypes current = enumerator.Current;
            StatusList.ParamValues paramValues = dictionary1[current];
            string type = names[(int) current];
            this.AddValue_TotalAndBonus(index1, type, paramValues.main_value, paramValues.bonus_value, paramValues.is_def_main, paramValues.is_def_bonus, false);
            ++index1;
          }
        }
        using (Dictionary<ParamTypes, StatusList.ParamValues>.KeyCollection.Enumerator enumerator = dictionary2.Keys.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            ParamTypes current = enumerator.Current;
            StatusList.ParamValues paramValues = dictionary2[current];
            string type = names[(int) current];
            this.AddValue_TotalAndBonus(index1, type, paramValues.main_value, paramValues.bonus_value, paramValues.is_def_main, paramValues.is_def_bonus, true);
            ++index1;
          }
        }
        for (; index1 < this.mItems.Count; ++index1)
          ((Component) this.mItems[index1]).get_gameObject().SetActive(false);
      }
    }

    private void AddValue_TotalAndBonus(int index, string type, int main_value, int bonus_value, bool is_def_main, bool is_def_bonus, bool multiply)
    {
      Color color1 = !is_def_main ? this.mDefaultValueColor : this.mDefaultBonusColor;
      Color color2 = !is_def_main ? this.mDefaultBonusColor : this.mDefaultBonusColor;
      if (this.mItems.Count <= index)
      {
        StatusListItem statusListItem = (StatusListItem) UnityEngine.Object.Instantiate<StatusListItem>((M0) this.ListItem);
        ((Component) statusListItem).get_transform().SetParent(((Component) this).get_transform(), false);
        this.mItems.Add(statusListItem);
      }
      StatusListItem mItem = this.mItems[index];
      ((Component) mItem).get_gameObject().SetActive(true);
      Text text = mItem.Value;
      Text bonus = mItem.Bonus;
      ((Component) text).get_gameObject().SetActive(false);
      ((Component) bonus).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) mItem.Label, (UnityEngine.Object) null))
        mItem.Label.set_text(LocalizedText.Get("sys." + type));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) text, (UnityEngine.Object) null))
      {
        string str = main_value.ToString();
        if (this.ShowSign && main_value > 0)
          str = "+" + str;
        if (multiply)
          str += "%";
        text.set_text(str);
        ((Graphic) text).set_color(color1);
        ((Component) text).get_gameObject().SetActive(true);
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) bonus, (UnityEngine.Object) null) || bonus_value == 0)
        return;
      string str1 = bonus_value.ToString();
      if (this.ShowSign && bonus_value > 0)
        str1 = "+" + str1;
      if (multiply)
        str1 += "%";
      bonus.set_text(string.Format(LocalizedText.Get("sys.STATUS_FORMAT_PARAM_BONUS"), (object) str1));
      ((Graphic) bonus).set_color(color2);
      ((Component) bonus).get_gameObject().SetActive(true);
    }

    public void SetValues_Restrict(BaseStatus paramBaseAdd, BaseStatus paramBaseMul, BaseStatus paramBonusAdd, BaseStatus paramBonusMul, bool new_param_only)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning(((Component) this).get_gameObject().GetPath((GameObject) null) + ": ListItem not set");
      }
      else
      {
        int index1 = 0;
        string[] names = Enum.GetNames(typeof (ParamTypes));
        Array values = Enum.GetValues(typeof (ParamTypes));
        for (int index2 = 0; index2 < values.Length; ++index2)
        {
          ParamTypes index3 = (ParamTypes) values.GetValue(index2);
          if (index3 != ParamTypes.HpMax)
          {
            int num1 = (int) paramBaseAdd[index3];
            int bonus1 = (int) paramBonusAdd[index3];
            if (bonus1 != 0)
            {
              if (num1 == 0 && new_param_only)
              {
                this.AddValue(index1, names[index2], bonus1, bonus1, false, false, false);
                ++index1;
              }
              if (num1 != 0 && !new_param_only)
              {
                this.AddValue(index1, names[index2], bonus1, bonus1, false, false, false);
                ++index1;
              }
            }
            int num2 = (int) paramBaseMul[index3];
            int bonus2 = (int) paramBonusMul[index3];
            if (bonus2 != 0)
            {
              if (num2 == 0 && new_param_only)
              {
                this.AddValue(index1, names[index2], bonus2, bonus2, true, false, false);
                ++index1;
              }
              if (num2 != 0 && !new_param_only)
              {
                this.AddValue(index1, names[index2], bonus2, bonus2, true, false, false);
                ++index1;
              }
            }
          }
        }
        for (; index1 < this.mItems.Count; ++index1)
          ((Component) this.mItems[index1]).get_gameObject().SetActive(false);
      }
    }

    private class ParamValues
    {
      public bool is_def_main;
      public bool is_def_bonus;
      public int main_value;
      public int bonus_value;
    }
  }
}

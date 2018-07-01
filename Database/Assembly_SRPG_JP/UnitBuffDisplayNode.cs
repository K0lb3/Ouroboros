// Decompiled with JetBrains decompiler
// Type: SRPG.UnitBuffDisplayNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitBuffDisplayNode : MonoBehaviour
  {
    public GameObject Icon;
    public GameObject Arrows;
    private RectTransform m_RectTransform;
    private UnitBuffDisplayNode.DispType m_DispType;
    private UnitBuffDisplayNode.EffectType m_EffectType;

    public UnitBuffDisplayNode()
    {
      base.\u002Ector();
    }

    public RectTransform rectTransform
    {
      get
      {
        if (Object.op_Equality((Object) this.m_RectTransform, (Object) null))
          this.m_RectTransform = (RectTransform) ((Component) this).GetComponent<RectTransform>();
        return this.m_RectTransform;
      }
    }

    private void Awake()
    {
    }

    private void OnDestroy()
    {
      this.Release();
    }

    public void Setup(UnitBuffDisplayNode.Param param)
    {
      this.m_DispType = param.dispType;
      this.m_EffectType = param.effectType;
      if (Object.op_Inequality((Object) this.Icon, (Object) null) && this.m_DispType != UnitBuffDisplayNode.DispType.NONE)
      {
        if (param.data != null)
        {
          Image component = (Image) this.Icon.GetComponent<Image>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            component.set_sprite(param.data.sprite);
            this.Icon.SetActive(true);
          }
        }
        else
          this.Icon.SetActive(false);
      }
      if (!Object.op_Inequality((Object) this.Arrows, (Object) null) || this.m_EffectType == UnitBuffDisplayNode.EffectType.NONE)
        return;
      string str = "arrow_" + this.m_EffectType.ToString().ToLower();
      Transform transform = this.Arrows.get_transform();
      int num = 0;
      for (int childCount = transform.get_childCount(); num < childCount; ++num)
      {
        Transform child = transform.GetChild(num);
        if (((Object) child).get_name() == str)
          ((Component) child).get_gameObject().SetActive(true);
        else
          ((Component) child).get_gameObject().SetActive(false);
      }
    }

    private void Release()
    {
    }

    private void Update()
    {
    }

    public UnitBuffDisplayNode.DispType GetDispType()
    {
      return this.m_DispType;
    }

    public UnitBuffDisplayNode.EffectType GetEffectType()
    {
      return this.m_EffectType;
    }

    public void SetPos(float x, float y)
    {
      this.rectTransform.set_anchoredPosition(new Vector2(x, y));
    }

    public static UnitBuffDisplayNode.Param[] CreateParams(UnitBuffDisplay parent, Unit owner, BuffAttachment buff)
    {
      List<UnitBuffDisplayNode.Param> objList = new List<UnitBuffDisplayNode.Param>();
      for (int index = 1; index < 5; ++index)
      {
        UnitBuffDisplayNode.BuffType buffType = (UnitBuffDisplayNode.BuffType) index;
        if ((double) UnitBuffDisplayNode.GetValue(buffType, buff) != 0.0)
        {
          UnitBuffDisplay.NodeData nodeData = parent.GetNodeData(buffType);
          if (nodeData != null)
            objList.Add(new UnitBuffDisplayNode.Param(owner, buff, nodeData));
        }
      }
      if (objList.Count == 0)
        objList.Add(new UnitBuffDisplayNode.Param(owner, buff, (UnitBuffDisplay.NodeData) null));
      return objList.ToArray();
    }

    public static int GetValue(UnitBuffDisplayNode.BuffType buffType, BuffAttachment buff)
    {
      switch (buffType)
      {
        case UnitBuffDisplayNode.BuffType.ATK:
          return (int) buff.status.param.atk;
        case UnitBuffDisplayNode.BuffType.DEF:
          return (int) buff.status.param.def;
        case UnitBuffDisplayNode.BuffType.MAG:
          return (int) buff.status.param.mag;
        case UnitBuffDisplayNode.BuffType.MND:
          return (int) buff.status.param.mnd;
        default:
          return 0;
      }
    }

    public static bool NeedDispOn(UnitBuffDisplayNode.Param param)
    {
      return param.dispType != UnitBuffDisplayNode.DispType.NONE;
    }

    public enum BuffType
    {
      NONE,
      ATK,
      DEF,
      MAG,
      MND,
      MAX,
    }

    public enum DispType
    {
      NONE,
      ATK,
      DEF,
      MAG,
      MND,
    }

    public enum EffectType
    {
      NONE,
      UP,
      DOWN,
    }

    public struct Param
    {
      public Unit owner;
      public BuffAttachment buff;
      public UnitBuffDisplay.NodeData data;

      public Param(Unit _owner, BuffAttachment _buff, UnitBuffDisplay.NodeData _data)
      {
        this.owner = _owner;
        this.buff = _buff;
        this.data = _data;
      }

      public bool isAlive
      {
        get
        {
          if (this.owner != null)
          {
            int index = 0;
            for (int count = this.owner.BuffAttachments.Count; index < count; ++index)
            {
              if (this.buff == this.owner.BuffAttachments[index])
                return true;
            }
          }
          return false;
        }
      }

      public bool isNeedDispOn
      {
        get
        {
          return UnitBuffDisplayNode.NeedDispOn(this);
        }
      }

      public UnitBuffDisplayNode.BuffType buffType
      {
        get
        {
          if (this.data != null)
            return this.data.buff;
          return UnitBuffDisplayNode.BuffType.NONE;
        }
      }

      public UnitBuffDisplayNode.DispType dispType
      {
        get
        {
          if (this.data != null)
            return this.data.disp;
          return UnitBuffDisplayNode.DispType.NONE;
        }
      }

      public UnitBuffDisplayNode.EffectType effectType
      {
        get
        {
          if (this.value > 0)
            return UnitBuffDisplayNode.EffectType.UP;
          return this.value < 0 ? UnitBuffDisplayNode.EffectType.DOWN : UnitBuffDisplayNode.EffectType.NONE;
        }
      }

      public int value
      {
        get
        {
          if (this.data != null)
            return UnitBuffDisplayNode.GetValue(this.data.buff, this.buff);
          return 0;
        }
      }
    }
  }
}

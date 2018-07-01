// Decompiled with JetBrains decompiler
// Type: SRPG.DropGoldEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG
{
  public class DropGoldEffect : MonoBehaviour
  {
    public const string GOLD_GAMEOBJECT_NAME = "UI_GOLD";
    [NonSerialized]
    public int Gold;
    private RectTransform m_TargetRect;
    private Unit m_DropOwner;

    public DropGoldEffect()
    {
      base.\u002Ector();
    }

    public RectTransform TargetRect
    {
      get
      {
        return this.m_TargetRect;
      }
    }

    public Unit DropOwner
    {
      set
      {
        this.m_DropOwner = value;
      }
      get
      {
        return this.m_DropOwner;
      }
    }

    private void Start()
    {
      GameObject gameObject = GameObjectID.FindGameObject("UI_GOLD");
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        this.m_TargetRect = gameObject.get_transform() as RectTransform;
      SceneBattle.SimpleEvent.Send(SceneBattle.TreasureEvent.GROUP, "DropGoldEffect.End", (object) this);
      GameUtility.RequireComponent<OneShotParticle>(((Component) this).get_gameObject());
    }
  }
}

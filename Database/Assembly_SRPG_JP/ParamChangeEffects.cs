// Decompiled with JetBrains decompiler
// Type: SRPG.ParamChangeEffects
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG
{
  public class ParamChangeEffects : ScriptableObject
  {
    [HideInInspector]
    public ParamChangeEffects.EffectData[] Effects;

    public ParamChangeEffects()
    {
      base.\u002Ector();
    }

    public Sprite FindSprite(string type, bool isDebuff)
    {
      for (int index = 0; index < this.Effects.Length; ++index)
      {
        if (this.Effects[index].TypeName == type)
        {
          if (isDebuff)
            return this.Effects[index].OnDebuff;
          return this.Effects[index].OnBuff;
        }
      }
      return (Sprite) null;
    }

    [Serializable]
    public struct EffectData
    {
      public string TypeName;
      public Sprite OnBuff;
      public Sprite OnDebuff;
    }
  }
}

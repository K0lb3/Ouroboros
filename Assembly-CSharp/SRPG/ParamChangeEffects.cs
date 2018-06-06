// Decompiled with JetBrains decompiler
// Type: SRPG.ParamChangeEffects
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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

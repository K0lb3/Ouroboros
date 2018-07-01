// Decompiled with JetBrains decompiler
// Type: SRPG.PortraitSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class PortraitSet : ScriptableObject
  {
    public Texture2D Normal;
    public Texture2D Smile;
    public Texture2D Sad;
    public Texture2D Angry;

    public PortraitSet()
    {
      base.\u002Ector();
    }

    public Texture2D GetEmotionImage(PortraitSet.EmotionTypes type)
    {
      switch (type)
      {
        case PortraitSet.EmotionTypes.Smile:
          if (!Object.op_Equality((Object) this.Smile, (Object) null))
            return this.Smile;
          break;
        case PortraitSet.EmotionTypes.Sad:
          if (!Object.op_Equality((Object) this.Sad, (Object) null))
            return this.Sad;
          break;
        case PortraitSet.EmotionTypes.Angry:
          if (!Object.op_Equality((Object) this.Angry, (Object) null))
            return this.Angry;
          break;
      }
      return this.Normal;
    }

    public enum EmotionTypes
    {
      Normal,
      Smile,
      Sad,
      Angry,
    }
  }
}

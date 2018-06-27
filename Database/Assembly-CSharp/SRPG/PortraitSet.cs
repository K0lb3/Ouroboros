// Decompiled with JetBrains decompiler
// Type: SRPG.PortraitSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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

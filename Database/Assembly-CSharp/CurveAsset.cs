// Decompiled with JetBrains decompiler
// Type: CurveAsset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

public class CurveAsset : ScriptableObject
{
  public CurveAsset.CurveStruct[] Curves;

  public CurveAsset()
  {
    base.\u002Ector();
  }

  public AnimationCurve FindCurve(string name)
  {
    for (int index = this.Curves.Length - 1; index >= 0; --index)
    {
      if (this.Curves[index].Name == name)
        return this.Curves[index].Curve;
    }
    return (AnimationCurve) null;
  }

  [Serializable]
  public struct CurveStruct
  {
    public string Name;
    public AnimationCurve Curve;
  }
}

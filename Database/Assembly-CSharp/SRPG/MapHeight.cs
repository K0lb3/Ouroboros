// Decompiled with JetBrains decompiler
// Type: SRPG.MapHeight
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class MapHeight : MonoBehaviour
  {
    private int oldHeight;
    public int Height;
    public BitmapText MapHeightText;
    private Unit mFocusUnit;

    public MapHeight()
    {
      base.\u002Ector();
    }

    public Unit FocusUnit
    {
      set
      {
        this.mFocusUnit = value;
      }
    }

    private void Start()
    {
      this.MapHeightText.text = this.Height.ToString();
    }

    private void Update()
    {
      if (this.mFocusUnit != null)
        this.Height = SceneBattle.Instance.GetDisplayHeight(this.mFocusUnit);
      if (this.oldHeight != this.Height)
        this.MapHeightText.text = this.Height.ToString();
      this.oldHeight = this.Height;
    }
  }
}

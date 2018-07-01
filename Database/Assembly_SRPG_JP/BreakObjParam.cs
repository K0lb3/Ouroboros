// Decompiled with JetBrains decompiler
// Type: SRPG.BreakObjParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class BreakObjParam
  {
    private string mIname;
    private string mName;
    private string mExpr;
    private string mUnitId;
    private eMapBreakClashType mClashType;
    private eMapBreakAIType mAiType;
    private eMapBreakSideType mSideType;
    private eMapBreakRayType mRayType;
    private bool mIsUI;
    private int[] mRestHps;
    private int mAliveClock;
    private EUnitDirection mAppearDir;

    public string Iname
    {
      get
      {
        return this.mIname;
      }
    }

    public string Name
    {
      get
      {
        return this.mName;
      }
    }

    public string Expr
    {
      get
      {
        return this.mExpr;
      }
    }

    public string UnitId
    {
      get
      {
        return this.mUnitId;
      }
    }

    public eMapBreakClashType ClashType
    {
      get
      {
        return this.mClashType;
      }
    }

    public eMapBreakAIType AiType
    {
      get
      {
        return this.mAiType;
      }
    }

    public eMapBreakSideType SideType
    {
      get
      {
        return this.mSideType;
      }
    }

    public eMapBreakRayType RayType
    {
      get
      {
        return this.mRayType;
      }
    }

    public bool IsUI
    {
      get
      {
        return this.mIsUI;
      }
    }

    public int[] RestHps
    {
      get
      {
        return this.mRestHps;
      }
    }

    public int AliveClock
    {
      get
      {
        return this.mAliveClock;
      }
    }

    public EUnitDirection AppearDir
    {
      get
      {
        return this.mAppearDir;
      }
    }

    public void Deserialize(JSON_BreakObjParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mName = json.name;
      this.mExpr = json.expr;
      this.mUnitId = json.unit_id;
      this.mClashType = (eMapBreakClashType) json.clash_type;
      this.mAiType = (eMapBreakAIType) json.ai_type;
      this.mSideType = (eMapBreakSideType) json.side_type;
      this.mRayType = (eMapBreakRayType) json.ray_type;
      this.mIsUI = json.is_ui != 0;
      this.mRestHps = (int[]) null;
      if (!string.IsNullOrEmpty(json.rest_hps))
      {
        string[] strArray = json.rest_hps.Split(',');
        if (strArray != null && strArray.Length != 0)
        {
          this.mRestHps = new int[strArray.Length];
          for (int index = 0; index < strArray.Length; ++index)
          {
            int result = 0;
            int.TryParse(strArray[index], out result);
            this.mRestHps[index] = result;
          }
        }
      }
      this.mAliveClock = json.clock;
      this.mAppearDir = (EUnitDirection) json.appear_dir;
    }
  }
}

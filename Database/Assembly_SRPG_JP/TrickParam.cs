// Decompiled with JetBrains decompiler
// Type: SRPG.TrickParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class TrickParam
  {
    private string mIname;
    private string mName;
    private string mExpr;
    private eTrickDamageType mDamageType;
    private OInt mDamageVal;
    private SkillParamCalcTypes mCalcType;
    private EElement mElem;
    private AttackDetailTypes mAttackDetail;
    private string mBuffId;
    private string mCondId;
    private OInt mKnockBackRate;
    private OInt mKnockBackVal;
    private ESkillTarget mTarget;
    private eTrickVisualType mVisualType;
    private OInt mActionCount;
    private OInt mValidClock;
    private OBool mIsNoOverWrite;
    private string mMarkerName;
    private string mEffectName;
    private ESkillTarget mEffTarget;
    private ESelectType mEffShape;
    private OInt mEffScope;
    private OInt mEffHeight;

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

    public eTrickDamageType DamageType
    {
      get
      {
        return this.mDamageType;
      }
    }

    public OInt DamageVal
    {
      get
      {
        return this.mDamageVal;
      }
    }

    public SkillParamCalcTypes CalcType
    {
      get
      {
        return this.mCalcType;
      }
    }

    public EElement Elem
    {
      get
      {
        return this.mElem;
      }
    }

    public AttackDetailTypes AttackDetail
    {
      get
      {
        return this.mAttackDetail;
      }
    }

    public string BuffId
    {
      get
      {
        return this.mBuffId;
      }
    }

    public string CondId
    {
      get
      {
        return this.mCondId;
      }
    }

    public OInt KnockBackRate
    {
      get
      {
        return this.mKnockBackRate;
      }
    }

    public OInt KnockBackVal
    {
      get
      {
        return this.mKnockBackVal;
      }
    }

    public ESkillTarget Target
    {
      get
      {
        return this.mTarget;
      }
    }

    public eTrickVisualType VisualType
    {
      get
      {
        return this.mVisualType;
      }
    }

    public OInt ActionCount
    {
      get
      {
        return this.mActionCount;
      }
    }

    public OInt ValidClock
    {
      get
      {
        return this.mValidClock;
      }
    }

    public OBool IsNoOverWrite
    {
      get
      {
        return this.mIsNoOverWrite;
      }
    }

    public string MarkerName
    {
      get
      {
        return this.mMarkerName;
      }
    }

    public string EffectName
    {
      get
      {
        return this.mEffectName;
      }
    }

    public ESkillTarget EffTarget
    {
      get
      {
        return this.mEffTarget;
      }
    }

    public ESelectType EffShape
    {
      get
      {
        return this.mEffShape;
      }
    }

    public OInt EffScope
    {
      get
      {
        return this.mEffScope;
      }
    }

    public OInt EffHeight
    {
      get
      {
        return this.mEffHeight;
      }
    }

    public bool IsAreaEff
    {
      get
      {
        return (int) this.mEffScope != 0;
      }
    }

    public void Deserialize(JSON_TrickParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mName = json.name;
      this.mExpr = json.expr;
      this.mDamageType = (eTrickDamageType) json.dmg_type;
      this.mDamageVal = (OInt) json.dmg_val;
      this.mCalcType = (SkillParamCalcTypes) json.calc;
      this.mElem = (EElement) json.elem;
      this.mAttackDetail = (AttackDetailTypes) json.atk_det;
      this.mBuffId = json.buff;
      this.mCondId = json.cond;
      this.mKnockBackRate = (OInt) json.kb_rate;
      this.mKnockBackVal = (OInt) json.kb_val;
      this.mTarget = (ESkillTarget) json.target;
      this.mVisualType = (eTrickVisualType) json.visual;
      this.mActionCount = (OInt) json.count;
      this.mValidClock = (OInt) json.clock;
      this.mIsNoOverWrite = (OBool) (json.is_no_ow != 0);
      this.mMarkerName = json.marker;
      this.mEffectName = json.effect;
      this.mEffTarget = (ESkillTarget) json.eff_target;
      this.mEffShape = (ESelectType) json.eff_shape;
      this.mEffScope = (OInt) json.eff_scope;
      this.mEffHeight = (OInt) json.eff_height;
    }
  }
}

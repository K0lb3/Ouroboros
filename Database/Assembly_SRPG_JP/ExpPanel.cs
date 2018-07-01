// Decompiled with JetBrains decompiler
// Type: SRPG.ExpPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(100, "Level Up", FlowNode.PinTypes.Output, 100)]
  public class ExpPanel : MonoBehaviour, IFlowInterface
  {
    public Text Level;
    public Text LevelMax;
    public Text ValueCurrent;
    public Text ValueCurrentInLv;
    public Text ValueLeft;
    public Text ValueTotal;
    public Slider LevelSlider;
    public Slider ExpSlider;
    public float ExpAnimLength;
    private int mCurrentExp;
    private float mExpStart;
    private float mExpEnd;
    private float mExpAnimTime;
    private int mLevelCap;
    private ExpPanel.CalcEvent mOnCalcExp;
    private ExpPanel.CalcEvent mOnCalcLevel;
    public ExpPanel.LevelChangeEvent OnLevelChange;
    public ExpPanel.ExpPanelEvent OnFinish;

    public ExpPanel()
    {
      base.\u002Ector();
    }

    public bool IsBusy
    {
      get
      {
        return (double) this.mExpStart != (double) this.mExpEnd;
      }
    }

    public int Exp
    {
      set
      {
        this.mCurrentExp = value;
        this.mExpStart = this.mExpEnd = (float) this.mCurrentExp;
        this.SetValues((float) this.mCurrentExp);
        this.Stop();
      }
      get
      {
        return this.mCurrentExp;
      }
    }

    public int LevelCap
    {
      get
      {
        return this.mLevelCap;
      }
      set
      {
        this.mLevelCap = value;
        if (!Object.op_Inequality((Object) this.LevelMax, (Object) null))
          return;
        this.LevelMax.set_text(this.mLevelCap.ToString());
      }
    }

    public void AnimateTo(int newExp)
    {
      this.mExpStart = Mathf.Lerp(this.mExpStart, this.mExpEnd, Mathf.Clamp01(this.mExpAnimTime / this.ExpAnimLength));
      this.mExpEnd = (float) newExp;
      this.mExpAnimTime = 0.0f;
      ((Behaviour) this).set_enabled(true);
    }

    public void Stop()
    {
      this.mExpEnd = this.mExpStart;
      ((Behaviour) this).set_enabled(false);
    }

    public void SetDelegate(ExpPanel.CalcEvent expFromLv, ExpPanel.CalcEvent lvFromExp)
    {
      this.mOnCalcExp = expFromLv;
      this.mOnCalcLevel = lvFromExp;
    }

    private void SetValues(float exp)
    {
      int num1 = Mathf.FloorToInt(exp);
      int num2 = Mathf.Min(this.mOnCalcLevel(num1), this.mLevelCap);
      int num3 = this.mOnCalcExp(num2);
      int num4 = this.mOnCalcExp(Mathf.Min(num2 + 1, this.mLevelCap));
      int num5 = Mathf.Min(num1, num4);
      if (Object.op_Inequality((Object) this.Level, (Object) null))
        this.Level.set_text(num2.ToString());
      if (Object.op_Inequality((Object) this.LevelSlider, (Object) null))
      {
        this.LevelSlider.set_maxValue((float) this.mOnCalcExp(this.mLevelCap));
        this.LevelSlider.set_minValue(0.0f);
        this.LevelSlider.set_value((float) num5);
      }
      if (Object.op_Inequality((Object) this.ExpSlider, (Object) null))
      {
        if (num2 >= this.mLevelCap)
        {
          this.ExpSlider.set_maxValue(1f);
          this.ExpSlider.set_minValue(0.0f);
          this.ExpSlider.set_value(1f);
        }
        else
        {
          this.ExpSlider.set_maxValue((float) num4);
          this.ExpSlider.set_minValue((float) num3);
          this.ExpSlider.set_value((float) num5);
        }
      }
      if (Object.op_Inequality((Object) this.ValueCurrent, (Object) null))
        this.ValueCurrent.set_text(num5.ToString());
      if (Object.op_Inequality((Object) this.ValueLeft, (Object) null))
        this.ValueLeft.set_text((num4 - num5).ToString());
      if (Object.op_Inequality((Object) this.ValueCurrentInLv, (Object) null))
        this.ValueCurrentInLv.set_text((num5 - num3).ToString());
      if (!Object.op_Inequality((Object) this.ValueTotal, (Object) null))
        return;
      this.ValueTotal.set_text((num4 - num3).ToString());
    }

    private void AnimateExp(float dt)
    {
      if (this.mOnCalcExp == null || this.mOnCalcLevel == null)
        return;
      float num1 = Mathf.Lerp(this.mExpStart, this.mExpEnd, this.mExpAnimTime / this.ExpAnimLength);
      this.mExpAnimTime += dt;
      bool flag1 = (double) this.mExpAnimTime >= (double) this.ExpAnimLength;
      float exp = !flag1 ? Mathf.Lerp(this.mExpStart, this.mExpEnd, Mathf.Clamp01(this.mExpAnimTime / this.ExpAnimLength)) : this.mExpEnd;
      int num2 = Mathf.FloorToInt(num1);
      int num3 = Mathf.FloorToInt(exp);
      int levelOld = Mathf.Min(this.mOnCalcLevel(num2), this.mLevelCap);
      int levelNew = Mathf.Min(this.mOnCalcLevel(num3), this.mLevelCap);
      bool flag2 = levelOld != levelNew;
      this.mCurrentExp = Mathf.FloorToInt(exp);
      this.SetValues(exp);
      if (flag2)
      {
        this.OnLevelChange(levelOld, levelNew);
        if (levelOld < levelNew)
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
      if (!flag1)
        return;
      ((Behaviour) this).set_enabled(false);
      this.mExpStart = this.mExpEnd = (float) this.mCurrentExp;
      this.OnFinish();
    }

    private void Update()
    {
      if ((double) this.mExpStart >= (double) this.mExpEnd)
        return;
      this.AnimateExp(Time.get_unscaledDeltaTime());
    }

    public void Activated(int pinID)
    {
    }

    public delegate int CalcEvent(int value);

    public delegate void LevelChangeEvent(int levelOld, int levelNew);

    public delegate void ExpPanelEvent();
  }
}

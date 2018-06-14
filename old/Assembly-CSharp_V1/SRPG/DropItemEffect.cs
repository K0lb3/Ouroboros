// Decompiled with JetBrains decompiler
// Type: SRPG.DropItemEffect
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

namespace SRPG
{
  public class DropItemEffect : MonoBehaviour
  {
    private const string TREASURE_GAMEOBJECT_NAME = "UI_TREASURE";
    private RectTransform mTargetRect;
    private ItemIcon mItemIcon;
    private Unit.DropItem mDropItem;
    public float Acceleration;
    public float Delay;
    private float mSpeed;
    private bool mSuikomiStarted;
    private Animator mEndAnimator;
    public float OpenWait;
    public float PopSpeed;
    private float mPopSpeed;
    private float mScaleSpeed;

    public DropItemEffect()
    {
      base.\u002Ector();
    }

    public Unit.DropItem DropItem
    {
      set
      {
        this.mDropItem = value;
      }
    }

    private void StartSuikomi()
    {
      this.mSuikomiStarted = true;
      GameObject gameObject = GameObjectID.FindGameObject("UI_TREASURE");
      if (Object.op_Equality((Object) gameObject, (Object) null))
        Debug.LogError((object) "UI_TREASUREが見つかりませんでした。");
      else
        this.mTargetRect = gameObject.get_transform() as RectTransform;
      DataSource.Bind<ItemParam>(((Component) this).get_gameObject(), this.mDropItem.param);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
      this.mItemIcon = (ItemIcon) ((Component) this).get_gameObject().GetComponent<ItemIcon>();
      if (Object.op_Inequality((Object) this.mItemIcon, (Object) null))
        this.mItemIcon.Num.set_text(this.mDropItem.num.ToString());
      ((Component) this).get_transform().set_localScale(new Vector3(0.3f, 0.3f, 1f));
      ((Component) this).get_transform().set_position(new Vector3((float) ((Component) this).get_transform().get_position().x, (float) (((Component) this).get_transform().get_position().y + 25.0), (float) ((Component) this).get_transform().get_position().z));
      this.mEndAnimator = (Animator) gameObject.GetComponent<Animator>();
      this.mEndAnimator.SetBool("open", false);
    }

    private void updatePop()
    {
      this.mPopSpeed += this.PopSpeed * Time.get_deltaTime();
      if (1.0 > ((Component) this).get_transform().get_localScale().x + (double) this.mPopSpeed)
      {
        Vector3 localScale = ((Component) this).get_transform().get_localScale();
        ((Component) this).get_transform().set_localScale(new Vector3((float) localScale.x + this.mPopSpeed, (float) localScale.y + this.mPopSpeed, (float) localScale.z));
        float num = this.mPopSpeed * 100f;
        if ((double) num > 25.0)
          num = 25f;
        Vector3 localPosition = ((Component) this).get_transform().get_localPosition();
        ((Component) this).get_transform().set_localPosition(new Vector3((float) localPosition.x, (float) localPosition.y + num, (float) localPosition.z));
      }
      else
        ((Component) this).get_transform().set_localScale(new Vector3(1f, 1f, 1f));
    }

    private void Hide()
    {
      IEnumerator enumerator = ((Component) this).get_gameObject().get_transform().GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
          ((Component) enumerator.Current).get_gameObject().SetActive(false);
      }
      finally
      {
        IDisposable disposable = enumerator as IDisposable;
        if (disposable != null)
          disposable.Dispose();
      }
    }

    private void Start()
    {
      this.Hide();
    }

    private void Update()
    {
      if (Object.op_Inequality((Object) this.mEndAnimator, (Object) null) && this.mEndAnimator.GetBool("open"))
      {
        if (this.mEndAnimator.IsInTransition(0))
          return;
        AnimatorStateInfo animatorStateInfo = this.mEndAnimator.GetCurrentAnimatorStateInfo(0);
        // ISSUE: explicit reference operation
        if ((double) ((AnimatorStateInfo) @animatorStateInfo).get_normalizedTime() < 1.0)
          return;
        Object.Destroy((Object) ((Component) this).get_gameObject());
      }
      else
      {
        if (!this.mSuikomiStarted)
          this.StartSuikomi();
        if (Object.op_Equality((Object) this.mTargetRect, (Object) null))
          return;
        if ((double) this.OpenWait > 0.0)
        {
          this.OpenWait -= Time.get_deltaTime();
          if ((double) this.OpenWait > 0.0)
            return;
          IEnumerator enumerator = ((Component) this).get_gameObject().get_transform().GetEnumerator();
          try
          {
            while (enumerator.MoveNext())
              ((Component) enumerator.Current).get_gameObject().SetActive(true);
          }
          finally
          {
            IDisposable disposable = enumerator as IDisposable;
            if (disposable != null)
              disposable.Dispose();
          }
        }
        else if ((double) this.Delay > 0.0)
        {
          this.Delay -= Time.get_deltaTime();
          this.updatePop();
        }
        else
        {
          this.mSpeed += this.Acceleration * Time.get_deltaTime();
          Vector3 position = ((Transform) this.mTargetRect).get_position();
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector3& local1 = @position;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local1).x = (__Null) ((^local1).x - this.mTargetRect.get_sizeDelta().x * 0.800000011920929);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector3& local2 = @position;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local2).y = (__Null) ((^local2).y + this.mTargetRect.get_sizeDelta().y * 0.5);
          Vector3 vector3_1 = Vector3.op_Subtraction(position, ((Component) this).get_transform().get_position());
          // ISSUE: explicit reference operation
          Vector3 vector3_2 = Vector3.op_Multiply(((Vector3) @vector3_1).get_normalized(), this.mSpeed);
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          if ((double) ((Vector3) @vector3_2).get_sqrMagnitude() < (double) ((Vector3) @vector3_1).get_sqrMagnitude())
          {
            Transform transform = ((Component) this).get_transform();
            transform.set_position(Vector3.op_Addition(transform.get_position(), vector3_2));
            this.mScaleSpeed += 1f * Time.get_deltaTime();
            if (((Component) this).get_transform().get_localScale().x - (double) this.mScaleSpeed <= 0.5)
              return;
            ((Component) this).get_transform().set_localScale(new Vector3((float) ((Component) this).get_transform().get_localScale().x - this.mScaleSpeed, (float) ((Component) this).get_transform().get_localScale().y - this.mScaleSpeed, 1f));
          }
          else
          {
            ((Component) this).get_transform().set_position(position);
            if (Object.op_Inequality((Object) this.mTargetRect, (Object) null))
              GameParameter.UpdateAll(((Component) this.mTargetRect).get_gameObject());
            if (!Object.op_Inequality((Object) this.mEndAnimator, (Object) null) || this.mEndAnimator.GetBool("open"))
              return;
            this.mEndAnimator.SetBool("open", true);
            this.Hide();
          }
        }
      }
    }
  }
}

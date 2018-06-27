// Decompiled with JetBrains decompiler
// Type: SRPG.DropItemEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

namespace SRPG
{
  public class DropItemEffect : MonoBehaviour
  {
    private const string TREASURE_GAMEOBJECT_NAME = "UI_TREASURE";
    private DropItemEffect.State m_State;
    private RectTransform m_TargetRect;
    private ItemIcon m_ItemIcon;
    private Unit m_DropOwner;
    private Unit.DropItem m_DropItem;
    public float Acceleration;
    public float Delay;
    private float m_Speed;
    private Animator m_EndAnimator;
    public float OpenWait;
    public float PopSpeed;
    private float m_PopSpeed;
    private float m_ScaleSpeed;
    private float m_DeleteDelay;

    public DropItemEffect()
    {
      base.\u002Ector();
    }

    public RectTransform TargetRect
    {
      get
      {
        return this.m_TargetRect;
      }
    }

    public Unit DropOwner
    {
      set
      {
        this.m_DropOwner = value;
      }
      get
      {
        return this.m_DropOwner;
      }
    }

    public Unit.DropItem DropItem
    {
      set
      {
        this.m_DropItem = value;
      }
    }

    private void Start()
    {
      this.Hide();
    }

    private void Update()
    {
      switch (this.m_State)
      {
        case DropItemEffect.State.SETUP:
          this.State_Setup();
          break;
        case DropItemEffect.State.OPEN:
          this.State_Open();
          break;
        case DropItemEffect.State.POPUP:
          this.State_Popup();
          break;
        case DropItemEffect.State.MOVE:
          this.State_Move();
          break;
        case DropItemEffect.State.END:
          this.State_End();
          break;
        case DropItemEffect.State.DELETE:
          this.State_Delete();
          break;
      }
    }

    public void SetItem(Unit.DropItem item)
    {
      this.m_DropItem = item;
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

    private void Show()
    {
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
      ItemIcon component = (ItemIcon) ((Component) this).get_gameObject().GetComponent<ItemIcon>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) || !component.IsSecret || !UnityEngine.Object.op_Implicit((UnityEngine.Object) component.SecretAmount))
        return;
      component.SecretAmount.SetActive(false);
    }

    private void State_Setup()
    {
      GameObject gameObject = GameObjectID.FindGameObject("UI_TREASURE");
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        Debug.LogError((object) "UI_TREASUREが見つかりませんでした。");
      else
        this.m_TargetRect = gameObject.get_transform() as RectTransform;
      DataSource.Bind<ItemParam>(((Component) this).get_gameObject(), this.m_DropItem.param);
      if ((bool) this.m_DropItem.is_secret)
      {
        ItemIcon component = (ItemIcon) ((Component) this).get_gameObject().GetComponent<ItemIcon>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.IsSecret = true;
      }
      GameParameter.UpdateAll(((Component) this).get_gameObject());
      this.m_ItemIcon = (ItemIcon) ((Component) this).get_gameObject().GetComponent<ItemIcon>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ItemIcon, (UnityEngine.Object) null))
        this.m_ItemIcon.Num.set_text(this.m_DropItem.num.ToString());
      ((Component) this).get_transform().set_localScale(new Vector3(0.3f, 0.3f, 1f));
      ((Component) this).get_transform().set_position(new Vector3((float) ((Component) this).get_transform().get_position().x, (float) (((Component) this).get_transform().get_position().y + 25.0), (float) ((Component) this).get_transform().get_position().z));
      this.m_EndAnimator = (Animator) gameObject.GetComponent<Animator>();
      this.m_State = DropItemEffect.State.OPEN;
    }

    private void State_Open()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_TargetRect, (UnityEngine.Object) null))
      {
        SceneBattle.SimpleEvent.Send(SceneBattle.TreasureEvent.GROUP, "DropItemEffect.End", (object) this);
        UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this).get_gameObject());
      }
      else
      {
        this.OpenWait -= Time.get_deltaTime();
        if ((double) this.OpenWait > 0.0)
          return;
        this.Show();
        this.m_State = DropItemEffect.State.POPUP;
      }
    }

    private void State_Popup()
    {
      this.Delay -= Time.get_deltaTime();
      this.m_PopSpeed += this.PopSpeed * Time.get_deltaTime();
      if (1.0 > ((Component) this).get_transform().get_localScale().x + (double) this.m_PopSpeed)
      {
        Vector3 localScale = ((Component) this).get_transform().get_localScale();
        ((Component) this).get_transform().set_localScale(new Vector3((float) localScale.x + this.m_PopSpeed, (float) localScale.y + this.m_PopSpeed, (float) localScale.z));
        float num = this.m_PopSpeed * 100f;
        if ((double) num > 25.0)
          num = 25f;
        Vector3 localPosition = ((Component) this).get_transform().get_localPosition();
        ((Component) this).get_transform().set_localPosition(new Vector3((float) localPosition.x, (float) localPosition.y + num, (float) localPosition.z));
      }
      else
      {
        ((Component) this).get_transform().set_localScale(new Vector3(1f, 1f, 1f));
        if ((double) this.Delay >= 0.0)
          return;
        this.m_State = DropItemEffect.State.MOVE;
      }
    }

    private void State_Move()
    {
      this.m_Speed += this.Acceleration * Time.get_deltaTime();
      Vector3 position = ((Transform) this.m_TargetRect).get_position();
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector3& local1 = @position;
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      (^local1).x = (__Null) ((^local1).x - this.m_TargetRect.get_sizeDelta().x * 0.800000011920929);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector3& local2 = @position;
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      (^local2).y = (__Null) ((^local2).y + this.m_TargetRect.get_sizeDelta().y * 0.5);
      Vector3 vector3_1 = Vector3.op_Subtraction(position, ((Component) this).get_transform().get_position());
      // ISSUE: explicit reference operation
      Vector3 vector3_2 = Vector3.op_Multiply(((Vector3) @vector3_1).get_normalized(), this.m_Speed);
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      if ((double) ((Vector3) @vector3_2).get_sqrMagnitude() < (double) ((Vector3) @vector3_1).get_sqrMagnitude())
      {
        Transform transform = ((Component) this).get_transform();
        transform.set_position(Vector3.op_Addition(transform.get_position(), vector3_2));
        this.m_ScaleSpeed += 1f * Time.get_deltaTime();
        if (((Component) this).get_transform().get_localScale().x - (double) this.m_ScaleSpeed <= 0.5)
          return;
        ((Component) this).get_transform().set_localScale(new Vector3((float) ((Component) this).get_transform().get_localScale().x - this.m_ScaleSpeed, (float) ((Component) this).get_transform().get_localScale().y - this.m_ScaleSpeed, 1f));
      }
      else
      {
        ((Component) this).get_transform().set_position(position);
        this.Hide();
        this.m_State = DropItemEffect.State.END;
      }
    }

    private void State_End()
    {
      SceneBattle.SimpleEvent.Send(SceneBattle.TreasureEvent.GROUP, "DropItemEffect.End", (object) this);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_EndAnimator, (UnityEngine.Object) null))
      {
        if (!this.m_EndAnimator.GetBool("open"))
          this.m_EndAnimator.SetBool("open", true);
        else
          this.m_EndAnimator.Play("open", 0, 0.0f);
      }
      this.m_DeleteDelay = 0.1f;
      this.m_State = DropItemEffect.State.DELETE;
    }

    private void State_Delete()
    {
      this.m_DeleteDelay -= Time.get_deltaTime();
      if ((double) this.m_DeleteDelay >= 0.0)
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this).get_gameObject());
      this.m_State = DropItemEffect.State.NONE;
    }

    private enum State
    {
      NONE,
      SETUP,
      OPEN,
      POPUP,
      MOVE,
      END,
      DELETE,
    }
  }
}

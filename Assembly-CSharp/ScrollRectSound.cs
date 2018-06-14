// Decompiled with JetBrains decompiler
// Type: ScrollRectSound
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Audio/ScrollRect Sound")]
public class ScrollRectSound : MonoBehaviour
{
  public string cueID;
  private Vector2 mPos;
  private Vector2 mPosDif;
  private float mWait;
  private bool mInitPos;
  private IntVector2 mPosID;

  public ScrollRectSound()
  {
    base.\u002Ector();
  }

  private void Awake()
  {
  }

  private void OnEnable()
  {
  }

  private void OnDisable()
  {
  }

  private void Update()
  {
    if ((double) this.mWait <= 0.0)
      return;
    this.mWait -= Time.get_unscaledDeltaTime();
  }

  public void OnValueChanged()
  {
    if ((double) this.mWait > 0.0)
      return;
    this.mWait = 0.1f;
    ScrollRect component = (ScrollRect) ((Component) this).get_gameObject().GetComponent<ScrollRect>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    RectTransform content = component.get_content();
    if (Object.op_Equality((Object) content, (Object) null))
      return;
    int num = -1;
    for (int index = 0; index < ((Transform) content).get_childCount(); ++index)
    {
      Transform child = ((Transform) content).GetChild(index);
      if (((Component) child).get_gameObject().GetActive())
      {
        if (!Object.op_Equality((Object) ((Component) child).get_gameObject().GetComponent<LayoutElement>(), (Object) null))
          ;
        num = index;
        break;
      }
    }
    if (num < 0)
      return;
    Transform child1 = ((Transform) content).GetChild(num);
    Vector2 vector2_1;
    // ISSUE: explicit reference operation
    ((Vector2) @vector2_1).\u002Ector((float) ((Component) child1).get_transform().get_position().x, (float) ((Component) child1).get_transform().get_position().y);
    Vector2 vector2_2;
    // ISSUE: explicit reference operation
    ((Vector2) @vector2_2).\u002Ector((float) vector2_1.x, (float) vector2_1.y);
    for (int index = 0; index < ((Transform) content).get_childCount(); ++index)
    {
      if (index != num)
      {
        Transform child2 = ((Transform) content).GetChild(index);
        if (((Component) child2).get_gameObject().GetActive())
        {
          if (!Object.op_Equality((Object) ((Component) child2).get_gameObject().GetComponent<LayoutElement>(), (Object) null))
            ;
          Vector2 vector2_3;
          // ISSUE: explicit reference operation
          ((Vector2) @vector2_3).\u002Ector((float) ((Component) child2).get_transform().get_position().x, (float) ((Component) child2).get_transform().get_position().y);
          vector2_1.x = (__Null) (double) Math.Min((float) vector2_1.x, (float) vector2_3.x);
          vector2_1.y = (__Null) (double) Math.Min((float) vector2_1.y, (float) vector2_3.y);
          vector2_2.x = (__Null) (double) Math.Max((float) vector2_2.x, (float) vector2_3.x);
          vector2_2.y = (__Null) (double) Math.Max((float) vector2_2.y, (float) vector2_3.y);
        }
      }
    }
    Vector2 vector2_4;
    // ISSUE: explicit reference operation
    ((Vector2) @vector2_4).\u002Ector(Math.Abs((float) (vector2_2.x - vector2_1.x)), Math.Abs((float) (vector2_2.y - vector2_1.y)));
    if ((double) Math.Abs((float) (vector2_4.x - this.mPosDif.x)) >= 1.0 || (double) Math.Abs((float) (vector2_4.y - this.mPosDif.y)) >= 1.0 || !this.mInitPos)
    {
      this.mPosDif = vector2_4;
      this.mInitPos = false;
    }
    if (!this.mInitPos)
    {
      Transform child2 = ((Transform) content).GetChild(num);
      this.mPos = new Vector2((float) ((Component) child2).get_transform().get_position().x, (float) ((Component) child2).get_transform().get_position().y);
      this.mInitPos = true;
    }
    else
    {
      Vector2 vector2_3;
      // ISSUE: explicit reference operation
      ((Vector2) @vector2_3).\u002Ector(-1f, -1f);
      IntVector2 mPosId = this.mPosID;
      for (int index = 0; index < ((Transform) content).get_childCount(); ++index)
      {
        Transform child2 = ((Transform) content).GetChild(index);
        if (((Component) child2).get_gameObject().GetActive())
        {
          if (!Object.op_Equality((Object) ((Component) child2).get_gameObject().GetComponent<LayoutElement>(), (Object) null))
            ;
          Vector2 vector2_5;
          // ISSUE: explicit reference operation
          ((Vector2) @vector2_5).\u002Ector((float) ((Component) child2).get_transform().get_position().x, (float) ((Component) child2).get_transform().get_position().y);
          Vector2 vector2_6;
          // ISSUE: explicit reference operation
          ((Vector2) @vector2_6).\u002Ector(Math.Abs((float) (vector2_5.x - this.mPos.x)), Math.Abs((float) (vector2_5.y - this.mPos.y)));
          if (vector2_3.x < 0.0 || vector2_6.x < vector2_3.x)
          {
            vector2_3.x = vector2_6.x;
            mPosId.x = index;
          }
          if (vector2_3.y < 0.0 || vector2_6.y < vector2_3.y)
          {
            vector2_3.y = vector2_6.y;
            mPosId.y = index;
          }
        }
      }
      if (mPosId.x != this.mPosID.x && this.mPosID.x > 0 && component.get_horizontal())
        this.Play();
      else if (mPosId.y != this.mPosID.y && this.mPosID.y > 0 && component.get_vertical())
        this.Play();
      this.mPosID = mPosId;
    }
  }

  public void Play()
  {
    MonoSingleton<MySound>.Instance.PlaySEOneShot(this.cueID, 0.0f);
  }

  public void Reset()
  {
    this.mWait = 0.0f;
    this.mInitPos = false;
    this.mPos = Vector2.get_zero();
    this.mPosDif = Vector2.get_zero();
    this.mPosID = new IntVector2(0, 0);
  }
}

// Decompiled with JetBrains decompiler
// Type: SRPG.DamageDsgPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class DamageDsgPopup : MonoBehaviour
  {
    public Sprite[] DigitSprites;
    public Sprite BillionSprite;
    public Sprite TrillionSprite;
    public GameObject GoDispDigit;
    public float Spacing;
    public float SpacingUnit;
    public float DispTime;
    public float FadeTime;
    public float DelayTime;
    [SerializeField]
    private int mValue;
    [SerializeField]
    private Color mDigitColor;
    [SerializeField]
    private eDamageDispType mDamageDispType;
    private List<DamageDsgPopup.Digit> mDigitLists;
    private DamageDsgPopup.Digit mNumUnit;
    private float mPassedTime;
    private float mPassedFadeTime;
    private bool mIsInitialized;

    public DamageDsgPopup()
    {
      base.\u002Ector();
    }

    public void Setup(int value, Color color, eDamageDispType damage_disp_type)
    {
      if (!Object.op_Implicit((Object) this.GoDispDigit))
        return;
      this.GoDispDigit.SetActive(false);
      this.mValue = value;
      this.mDigitColor = color;
      this.mDamageDispType = damage_disp_type;
      int num1 = 1;
      int mValue1 = this.mValue;
      while (mValue1 >= 10)
      {
        ++num1;
        mValue1 /= 10;
      }
      this.mDigitLists.Clear();
      float num2 = (float) num1 * this.Spacing;
      Sprite sprite = (Sprite) null;
      switch (this.mDamageDispType)
      {
        case eDamageDispType.Billion:
          sprite = this.BillionSprite;
          break;
        case eDamageDispType.Trillion:
          sprite = this.TrillionSprite;
          break;
      }
      DamageDsgPopup.Digit digit1 = (DamageDsgPopup.Digit) null;
      if (Object.op_Inequality((Object) sprite, (Object) null))
      {
        GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.GoDispDigit);
        if (Object.op_Implicit((Object) gameObject))
        {
          digit1 = new DamageDsgPopup.Digit();
          digit1.mPosition = new Vector2(num2 * 0.5f, 0.0f);
          digit1.mTransform = gameObject.get_transform() as RectTransform;
          ((Transform) digit1.mTransform).SetParent(((Component) this).get_transform(), false);
          RectTransform mTransform = digit1.mTransform;
          Rect textureRect1 = sprite.get_textureRect();
          // ISSUE: explicit reference operation
          double width = (double) ((Rect) @textureRect1).get_width();
          Rect textureRect2 = sprite.get_textureRect();
          // ISSUE: explicit reference operation
          double height = (double) ((Rect) @textureRect2).get_height();
          Vector2 vector2 = new Vector2((float) width, (float) height);
          mTransform.set_sizeDelta(vector2);
          digit1.mTransform.set_anchoredPosition(digit1.mPosition);
          digit1.mImage = (Image) gameObject.GetComponent<Image>();
          if (Object.op_Implicit((Object) digit1.mImage))
            digit1.mImage.set_sprite(sprite);
          gameObject.SetActive(true);
        }
      }
      List<DamageDsgPopup.Digit> digitList = new List<DamageDsgPopup.Digit>();
      int mValue2 = this.mValue;
      for (int index = 0; index < num1; ++index)
      {
        GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.GoDispDigit);
        if (Object.op_Implicit((Object) gameObject))
        {
          Sprite digitSprite = this.DigitSprites[mValue2 % 10];
          DamageDsgPopup.Digit digit2 = new DamageDsgPopup.Digit();
          digit2.mPosition = new Vector2((float) ((double) num2 * 0.5 - (double) this.SpacingUnit * 0.5 - (double) this.Spacing * ((double) index + 0.5)), 0.0f);
          digit2.mTransform = gameObject.get_transform() as RectTransform;
          ((Transform) digit2.mTransform).SetParent(((Component) this).get_transform(), false);
          RectTransform mTransform = digit2.mTransform;
          Rect textureRect1 = digitSprite.get_textureRect();
          // ISSUE: explicit reference operation
          double width = (double) ((Rect) @textureRect1).get_width();
          Rect textureRect2 = digitSprite.get_textureRect();
          // ISSUE: explicit reference operation
          double height = (double) ((Rect) @textureRect2).get_height();
          Vector2 vector2 = new Vector2((float) width, (float) height);
          mTransform.set_sizeDelta(vector2);
          digit2.mTransform.set_anchoredPosition(digit2.mPosition);
          digit2.mImage = (Image) gameObject.GetComponent<Image>();
          if (Object.op_Implicit((Object) digit2.mImage))
          {
            digit2.mImage.set_sprite(digitSprite);
            ((Graphic) digit2.mImage).set_color(this.mDigitColor);
          }
          digitList.Add(digit2);
          gameObject.SetActive(true);
        }
        mValue2 /= 10;
      }
      for (int index = digitList.Count - 1; index >= 0; --index)
        this.mDigitLists.Add(digitList[index]);
      if (digit1 != null)
        this.mDigitLists.Add(digit1);
      this.mPassedTime = 0.0f;
      this.mPassedFadeTime = 0.0f;
      this.mIsInitialized = true;
      if (this.mDigitLists.Count == 0)
        return;
      using (List<DamageDsgPopup.Digit>.Enumerator enumerator = this.mDigitLists.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Animator component = (Animator) ((Component) enumerator.Current.mImage).get_gameObject().GetComponent<Animator>();
          if (Object.op_Implicit((Object) component))
            ((Behaviour) component).set_enabled(false);
        }
      }
      this.StartCoroutine(this.startDelayAnm());
    }

    [DebuggerHidden]
    private IEnumerator startDelayAnm()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new DamageDsgPopup.\u003CstartDelayAnm\u003Ec__Iterator2C()
      {
        \u003C\u003Ef__this = this
      };
    }

    private void Update()
    {
      if (!this.mIsInitialized)
        return;
      this.mPassedTime += Time.get_deltaTime();
      if ((double) this.mPassedTime < (double) this.DispTime)
        return;
      this.mPassedFadeTime += Time.get_deltaTime();
      if ((double) this.mPassedFadeTime >= (double) this.FadeTime)
      {
        Object.Destroy((Object) ((Component) this).get_gameObject());
      }
      else
      {
        float num = (float) (1.0 - (double) this.mPassedFadeTime / (double) this.FadeTime);
        for (int index = 0; index < this.mDigitLists.Count; ++index)
        {
          Color mDigitColor = this.mDigitColor;
          mDigitColor.a = (__Null) (double) num;
          ((Graphic) this.mDigitLists[index].mImage).set_color(mDigitColor);
        }
      }
    }

    private class Digit
    {
      public RectTransform mTransform;
      public Image mImage;
      public Vector2 mPosition;
    }
  }
}

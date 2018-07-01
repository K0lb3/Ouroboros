// Decompiled with JetBrains decompiler
// Type: SRPG.DamagePopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class DamagePopup : MonoBehaviour
  {
    public Sprite[] DigitSprites;
    public int Value;
    public float Spacing;
    public float PopMin;
    public float PopMax;
    public float Gravity;
    public float Resititution;
    public float KeepVisible;
    public float FadeTime;
    public Color DigitColor;
    private DamagePopup.Digit[] mDigits;
    private float mFadeTime;

    public DamagePopup()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      int length = 1;
      int num1 = this.Value;
      while (num1 >= 10)
      {
        ++length;
        num1 /= 10;
      }
      this.mDigits = new DamagePopup.Digit[length];
      float num2 = (float) length * this.Spacing;
      int num3 = this.Value;
      for (int index = 0; index < length; ++index)
      {
        GameObject gameObject = new GameObject("Number", new System.Type[2]
        {
          typeof (RectTransform),
          typeof (Image)
        });
        Sprite digitSprite = this.DigitSprites[num3 % 10];
        this.mDigits[index].Position = new Vector2((float) ((double) num2 * 0.5 - (double) this.Spacing * ((double) index + 0.5)), Random.Range(this.PopMin, this.PopMax));
        this.mDigits[index].Transform = gameObject.get_transform() as RectTransform;
        ((Transform) this.mDigits[index].Transform).SetParent(((Component) this).get_transform(), false);
        RectTransform transform = this.mDigits[index].Transform;
        Rect textureRect1 = digitSprite.get_textureRect();
        // ISSUE: explicit reference operation
        double width = (double) ((Rect) @textureRect1).get_width();
        Rect textureRect2 = digitSprite.get_textureRect();
        // ISSUE: explicit reference operation
        double height = (double) ((Rect) @textureRect2).get_height();
        Vector2 vector2 = new Vector2((float) width, (float) height);
        transform.set_sizeDelta(vector2);
        this.mDigits[index].Transform.set_anchoredPosition(this.mDigits[index].Position);
        this.mDigits[index].Image = (Image) gameObject.GetComponent<Image>();
        this.mDigits[index].Image.set_sprite(digitSprite);
        ((Graphic) this.mDigits[index].Image).set_color(this.DigitColor);
        num3 /= 10;
      }
    }

    private void Update()
    {
      bool flag = true;
      for (int index = 0; index < this.mDigits.Length; ++index)
      {
        this.mDigits[index].Velocity += this.Gravity * Time.get_deltaTime();
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local = @this.mDigits[index].Position;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local).y = (__Null) ((^local).y + (double) this.mDigits[index].Velocity * (double) Time.get_deltaTime());
        if (this.mDigits[index].Position.y <= 0.0)
        {
          this.mDigits[index].Position.y = (__Null) 0.0;
          this.mDigits[index].Velocity = -this.mDigits[index].Velocity * this.Resititution;
          if ((double) Mathf.Abs(this.mDigits[index].Velocity) <= 0.00999999977648258)
            this.mDigits[index].Velocity = 0.0f;
        }
        else
          flag = false;
        this.mDigits[index].Transform.set_anchoredPosition(this.mDigits[index].Position);
      }
      if (!flag)
        return;
      if ((double) this.KeepVisible > 0.0)
      {
        this.KeepVisible -= Time.get_deltaTime();
      }
      else
      {
        this.mFadeTime += Time.get_deltaTime();
        if ((double) this.mFadeTime >= (double) this.FadeTime)
        {
          UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this).get_gameObject());
        }
        else
        {
          float num = (float) (1.0 - (double) this.mFadeTime / (double) this.FadeTime);
          for (int index = 0; index < this.mDigits.Length; ++index)
          {
            Color digitColor = this.DigitColor;
            digitColor.a = (__Null) (double) num;
            ((Graphic) this.mDigits[index].Image).set_color(digitColor);
          }
        }
      }
    }

    private struct Digit
    {
      public RectTransform Transform;
      public Image Image;
      public Vector2 Position;
      public float Velocity;
    }
  }
}

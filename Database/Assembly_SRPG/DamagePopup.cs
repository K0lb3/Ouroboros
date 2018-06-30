namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

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
        private Digit[] mDigits;
        private float mFadeTime;

        public DamagePopup()
        {
            this.Spacing = 32f;
            this.PopMin = 0.1f;
            this.PopMax = 0.5f;
            this.Gravity = -10f;
            this.Resititution = 0.3f;
            this.KeepVisible = 0.5f;
            this.FadeTime = 1f;
            this.DigitColor = new Color(1f, 1f, 1f, 1f);
            base..ctor();
            return;
        }

        private unsafe void Start()
        {
            Type[] typeArray1;
            int num;
            int num2;
            float num3;
            int num4;
            GameObject obj2;
            Sprite sprite;
            Rect rect;
            Rect rect2;
            num = 1;
            num2 = this.Value;
            goto Label_0017;
        Label_000E:
            num += 1;
            num2 /= 10;
        Label_0017:
            if (num2 >= 10)
            {
                goto Label_000E;
            }
            this.mDigits = new Digit[num];
            num3 = ((float) num) * this.Spacing;
            num2 = this.Value;
            num4 = 0;
            goto Label_01AB;
        Label_0043:
            typeArray1 = new Type[] { typeof(RectTransform), typeof(Image) };
            obj2 = new GameObject("Number", typeArray1);
            sprite = this.DigitSprites[num2 % 10];
            &(this.mDigits[num4]).Position = new Vector2((num3 * 0.5f) - (this.Spacing * (((float) num4) + 0.5f)), Random.Range(this.PopMin, this.PopMax));
            &(this.mDigits[num4]).Transform = obj2.get_transform() as RectTransform;
            &(this.mDigits[num4]).Transform.SetParent(base.get_transform(), 0);
            &(this.mDigits[num4]).Transform.set_sizeDelta(new Vector2(&sprite.get_textureRect().get_width(), &sprite.get_textureRect().get_height()));
            &(this.mDigits[num4]).Transform.set_anchoredPosition(&(this.mDigits[num4]).Position);
            &(this.mDigits[num4]).Image = obj2.GetComponent<Image>();
            &(this.mDigits[num4]).Image.set_sprite(sprite);
            &(this.mDigits[num4]).Image.set_color(this.DigitColor);
            num2 /= 10;
            num4 += 1;
        Label_01AB:
            if (num4 < num)
            {
                goto Label_0043;
            }
            return;
        }

        private unsafe void Update()
        {
            bool flag;
            int num;
            float num2;
            int num3;
            Color color;
            flag = 1;
            num = 0;
            goto Label_012E;
        Label_0009:
            &(this.mDigits[num]).Velocity += this.Gravity * Time.get_deltaTime();
            &&(this.mDigits[num]).Position.y += &(this.mDigits[num]).Velocity * Time.get_deltaTime();
            if (&&(this.mDigits[num]).Position.y > 0f)
            {
                goto Label_0101;
            }
            &&(this.mDigits[num]).Position.y = 0f;
            &(this.mDigits[num]).Velocity = -&(this.mDigits[num]).Velocity * this.Resititution;
            if (Mathf.Abs(&(this.mDigits[num]).Velocity) > 0.01f)
            {
                goto Label_0103;
            }
            &(this.mDigits[num]).Velocity = 0f;
            goto Label_0103;
        Label_0101:
            flag = 0;
        Label_0103:
            &(this.mDigits[num]).Transform.set_anchoredPosition(&(this.mDigits[num]).Position);
            num += 1;
        Label_012E:
            if (num < ((int) this.mDigits.Length))
            {
                goto Label_0009;
            }
            if (flag == null)
            {
                goto Label_01E9;
            }
            if (this.KeepVisible <= 0f)
            {
                goto Label_0165;
            }
            this.KeepVisible -= Time.get_deltaTime();
            return;
        Label_0165:
            this.mFadeTime += Time.get_deltaTime();
            if (this.mFadeTime < this.FadeTime)
            {
                goto Label_0194;
            }
            Object.Destroy(base.get_gameObject());
            return;
        Label_0194:
            num2 = 1f - (this.mFadeTime / this.FadeTime);
            num3 = 0;
            goto Label_01DB;
        Label_01AF:
            color = this.DigitColor;
            &color.a = num2;
            &(this.mDigits[num3]).Image.set_color(color);
            num3 += 1;
        Label_01DB:
            if (num3 < ((int) this.mDigits.Length))
            {
                goto Label_01AF;
            }
        Label_01E9:
            return;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Digit
        {
            public RectTransform Transform;
            public UnityEngine.UI.Image Image;
            public Vector2 Position;
            public float Velocity;
        }
    }
}


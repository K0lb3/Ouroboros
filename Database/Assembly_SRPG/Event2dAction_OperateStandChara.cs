namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [EventActionInfo("New/立ち絵2/編集(2D)", "立ち絵2を編集します。", 0x555555, 0x444488)]
    public class Event2dAction_OperateStandChara : EventAction
    {
        [HideInInspector]
        public string[] CharaIDs;
        private bool CharaIDsFoldout;
        private List<EventStandCharaController2> mStandCharaList;
        private List<RectTransform> mStandCharaTransformList;
        private List<GameObject> charaList;
        private List<Color> BodyColorList;
        private List<Color> FaceColorList;
        [HideInInspector]
        public bool async;
        [HideInInspector]
        public bool Flip;
        [HideInInspector]
        public bool MoveEnabled;
        [HideInInspector]
        public float MoveTime;
        [HideInInspector]
        public AnimationCurve MoveCurve;
        [HideInInspector]
        public Vector2 MoveTo;
        [HideInInspector]
        public bool Relative;
        private bool MoveFoldout;
        private float MoveOffset;
        private List<Vector2> FromAnchorMinList;
        private List<Vector2> FromAnchorMaxList;
        private List<Vector2> mToAnchorList;
        [HideInInspector]
        public bool ScaleEnabled;
        [HideInInspector]
        public float ScaleTime;
        [HideInInspector]
        public AnimationCurve ScaleCurve;
        [HideInInspector]
        public Vector2 ScaleTo;
        private bool ScaleFoldout;
        private float ScaleOffset;
        private float mToWidth;
        private float mToHeght;
        private List<float> FromWidthList;
        private List<float> FromHeightList;
        [HideInInspector]
        public bool ColorEnabled;
        [HideInInspector]
        public float ColorTime;
        [HideInInspector]
        public AnimationCurve ColorCurve;
        [HideInInspector]
        public Color ColorTo;
        private bool ColorFoldout;
        private float ColorOffset;
        private Color mToColor;
        private bool mMoveEnabled;
        private bool mScaleEnabled;
        private bool mColorEnabled;
        public static Dictionary<string, Color> CharaColorDic;

        static Event2dAction_OperateStandChara()
        {
            CharaColorDic = new Dictionary<string, Color>();
            return;
        }

        public Event2dAction_OperateStandChara()
        {
            this.CharaIDsFoldout = 1;
            this.mStandCharaList = new List<EventStandCharaController2>();
            this.mStandCharaTransformList = new List<RectTransform>();
            this.charaList = new List<GameObject>();
            this.BodyColorList = new List<Color>();
            this.FaceColorList = new List<Color>();
            this.MoveEnabled = 1;
            this.MoveCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
            this.MoveTo = new Vector2(0f, 0f);
            this.MoveFoldout = 1;
            this.FromAnchorMinList = new List<Vector2>();
            this.FromAnchorMaxList = new List<Vector2>();
            this.mToAnchorList = new List<Vector2>();
            this.ScaleCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
            this.ScaleTo = new Vector2(1f, 1f);
            this.FromWidthList = new List<float>();
            this.FromHeightList = new List<float>();
            this.ColorCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
            this.ColorTo = Color.get_white();
            base..ctor();
            return;
        }

        private unsafe Vector2 convertPosition(Vector2 pos)
        {
            Vector2 vector;
            &vector..ctor(&pos.x + 1f, &pos.y * 2f);
            return Vector2.Scale(vector, new Vector2(0.5f, 0.5f));
        }

        public override unsafe void OnActivate()
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            string str;
            Vector3 vector;
            Vector3 vector2;
            if (this.mStandCharaList.Count > 0)
            {
                goto Label_0018;
            }
            base.ActivateNext();
            return;
        Label_0018:
            this.mMoveEnabled = this.MoveEnabled;
            this.mScaleEnabled = this.ScaleEnabled;
            this.mColorEnabled = this.ColorEnabled;
            num = 0;
            goto Label_007A;
        Label_0043:
            if ((this.mStandCharaList[num] != null) == null)
            {
                goto Label_0076;
            }
            this.mStandCharaTransformList.Add(this.mStandCharaList[num].GetComponent<RectTransform>());
        Label_0076:
            num += 1;
        Label_007A:
            if (num < this.mStandCharaList.Count)
            {
                goto Label_0043;
            }
            if (this.Flip == null)
            {
                goto Label_00D7;
            }
            num2 = 0;
            goto Label_00C6;
        Label_009D:
            this.mStandCharaTransformList[num2].Rotate(new Vector3(0f, 180f, 0f));
            num2 += 1;
        Label_00C6:
            if (num2 < this.mStandCharaTransformList.Count)
            {
                goto Label_009D;
            }
        Label_00D7:
            if (this.mMoveEnabled == null)
            {
                goto Label_01B3;
            }
            num3 = 0;
            goto Label_0187;
        Label_00E9:
            this.FromAnchorMinList.Add(this.mStandCharaTransformList[num3].get_anchorMin());
            this.FromAnchorMaxList.Add(this.mStandCharaTransformList[num3].get_anchorMax());
            if (this.Relative == null)
            {
                goto Label_016C;
            }
            this.mToAnchorList.Add(this.mStandCharaTransformList[num3].get_anchorMin() + Vector2.Scale(this.MoveTo, new Vector2(0.5f, 1f)));
            goto Label_0183;
        Label_016C:
            this.mToAnchorList.Add(this.convertPosition(this.MoveTo));
        Label_0183:
            num3 += 1;
        Label_0187:
            if (num3 < this.mStandCharaTransformList.Count)
            {
                goto Label_00E9;
            }
            if (this.MoveTime > 0f)
            {
                goto Label_01B3;
            }
            this.MoveOffset = 1f;
        Label_01B3:
            if (this.mScaleEnabled == null)
            {
                goto Label_0261;
            }
            num4 = 0;
            goto Label_0213;
        Label_01C5:
            this.FromWidthList.Add(&this.mStandCharaTransformList[num4].get_localScale().x);
            this.FromHeightList.Add(&this.mStandCharaTransformList[num4].get_localScale().y);
            num4 += 1;
        Label_0213:
            if (num4 < this.mStandCharaTransformList.Count)
            {
                goto Label_01C5;
            }
            this.mToWidth = &this.ScaleTo.x;
            this.mToHeght = &this.ScaleTo.y;
            if (this.ScaleTime > 0f)
            {
                goto Label_0261;
            }
            this.ScaleOffset = 1f;
        Label_0261:
            if (this.mColorEnabled == null)
            {
                goto Label_0430;
            }
            num5 = 0;
            goto Label_02AF;
        Label_0274:
            if ((this.mStandCharaList[num5] != null) == null)
            {
                goto Label_02A9;
            }
            this.charaList.AddRange(this.mStandCharaList[num5].StandCharaList);
        Label_02A9:
            num5 += 1;
        Label_02AF:
            if (num5 < this.mStandCharaList.Count)
            {
                goto Label_0274;
            }
            num6 = 0;
            goto Label_0395;
        Label_02C9:
            if ((this.charaList[num6].GetComponent<EventStandChara2>().BodyObject != null) == null)
            {
                goto Label_031C;
            }
            this.BodyColorList.Add(this.charaList[num6].GetComponent<EventStandChara2>().BodyObject.GetComponent<RawImage>().get_color());
            goto Label_032C;
        Label_031C:
            this.BodyColorList.Add(Color.get_white());
        Label_032C:
            if ((this.charaList[num6].GetComponent<EventStandChara2>().FaceObject != null) == null)
            {
                goto Label_037F;
            }
            this.FaceColorList.Add(this.charaList[num6].GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>().get_color());
            goto Label_038F;
        Label_037F:
            this.FaceColorList.Add(Color.get_white());
        Label_038F:
            num6 += 1;
        Label_0395:
            if (num6 < this.charaList.Count)
            {
                goto Label_02C9;
            }
            this.mToColor = this.ColorTo;
            num7 = 0;
            goto Label_0406;
        Label_03BB:
            str = this.CharaIDs[num7];
            if (CharaColorDic.ContainsKey(str) == null)
            {
                goto Label_03EE;
            }
            CharaColorDic[str] = this.ColorTo;
            goto Label_0400;
        Label_03EE:
            CharaColorDic.Add(str, this.ColorTo);
        Label_0400:
            num7 += 1;
        Label_0406:
            if (num7 < ((int) this.CharaIDs.Length))
            {
                goto Label_03BB;
            }
            if (this.ColorTime > 0f)
            {
                goto Label_0430;
            }
            this.ColorOffset = 1f;
        Label_0430:
            if (this.async == null)
            {
                goto Label_0442;
            }
            base.ActivateNext(1);
        Label_0442:
            return;
        }

        public override void PreStart()
        {
            int num;
            num = 0;
            goto Label_0023;
        Label_0007:
            this.mStandCharaList.Add(EventStandCharaController2.FindInstances(this.CharaIDs[num]));
            num += 1;
        Label_0023:
            if (num < ((int) this.CharaIDs.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public override unsafe void Update()
        {
            float num;
            int num2;
            float num3;
            int num4;
            Vector3 vector;
            float num5;
            int num6;
            Color color;
            Color color2;
            if (this.mMoveEnabled == null)
            {
                goto Label_010A;
            }
            if (this.MoveOffset < 1f)
            {
                goto Label_002D;
            }
            this.MoveOffset = 1f;
            this.mMoveEnabled = 0;
        Label_002D:
            num = this.MoveCurve.Evaluate(this.MoveOffset);
            num2 = 0;
            goto Label_00E0;
        Label_0046:
            this.mStandCharaTransformList[num2].set_anchorMin(this.FromAnchorMinList[num2] + Vector2.Scale(this.mToAnchorList[num2] - this.FromAnchorMinList[num2], new Vector2(num, num)));
            this.mStandCharaTransformList[num2].set_anchorMax(this.FromAnchorMaxList[num2] + Vector2.Scale(this.mToAnchorList[num2] - this.FromAnchorMaxList[num2], new Vector2(num, num)));
            num2 += 1;
        Label_00E0:
            if (num2 < this.mStandCharaTransformList.Count)
            {
                goto Label_0046;
            }
            this.MoveOffset += Time.get_deltaTime() / this.MoveTime;
        Label_010A:
            if (this.mScaleEnabled == null)
            {
                goto Label_01E1;
            }
            if (this.ScaleOffset < 1f)
            {
                goto Label_0137;
            }
            this.ScaleOffset = 1f;
            this.mScaleEnabled = 0;
        Label_0137:
            num3 = this.ScaleCurve.Evaluate(this.ScaleOffset);
            num4 = 0;
            goto Label_01B7;
        Label_0150:
            &vector..ctor(this.FromWidthList[num4] + ((this.mToWidth - this.FromWidthList[num4]) * num3), this.FromHeightList[num4] + ((this.mToHeght - this.FromHeightList[num4]) * num3), 1f);
            this.mStandCharaTransformList[num4].set_localScale(vector);
            num4 += 1;
        Label_01B7:
            if (num4 < this.mStandCharaTransformList.Count)
            {
                goto Label_0150;
            }
            this.ScaleOffset += Time.get_deltaTime() / this.ScaleTime;
        Label_01E1:
            if (this.mColorEnabled == null)
            {
                goto Label_031C;
            }
            if (this.ColorOffset < 1f)
            {
                goto Label_020E;
            }
            this.ColorOffset = 1f;
            this.mColorEnabled = 0;
        Label_020E:
            num5 = this.ColorCurve.Evaluate(this.ColorOffset);
            num6 = 0;
            goto Label_02F1;
        Label_0229:
            if ((this.charaList[num6].GetComponent<EventStandChara2>().BodyObject != null) == null)
            {
                goto Label_028A;
            }
            color = Color.Lerp(this.BodyColorList[num6], this.mToColor, num5);
            this.charaList[num6].GetComponent<EventStandChara2>().BodyObject.GetComponent<RawImage>().set_color(color);
        Label_028A:
            if ((this.charaList[num6].GetComponent<EventStandChara2>().FaceObject != null) == null)
            {
                goto Label_02EB;
            }
            color2 = Color.Lerp(this.FaceColorList[num6], this.mToColor, num5);
            this.charaList[num6].GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>().set_color(color2);
        Label_02EB:
            num6 += 1;
        Label_02F1:
            if (num6 < this.charaList.Count)
            {
                goto Label_0229;
            }
            this.ColorOffset += Time.get_deltaTime() / this.ColorTime;
        Label_031C:
            if (this.mMoveEnabled != null)
            {
                goto Label_035A;
            }
            if (this.mScaleEnabled != null)
            {
                goto Label_035A;
            }
            if (this.mColorEnabled != null)
            {
                goto Label_035A;
            }
            if (this.async == null)
            {
                goto Label_0354;
            }
            base.enabled = 0;
            goto Label_035A;
        Label_0354:
            base.ActivateNext();
        Label_035A:
            return;
        }
    }
}


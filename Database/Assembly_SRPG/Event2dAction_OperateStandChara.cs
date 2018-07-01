// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_OperateStandChara
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [EventActionInfo("New/立ち絵2/編集(2D)", "立ち絵2を編集します。", 5592405, 4473992)]
  public class Event2dAction_OperateStandChara : EventAction
  {
    public static Dictionary<string, Color> CharaColorDic = new Dictionary<string, Color>();
    private bool CharaIDsFoldout = true;
    private List<EventStandCharaController2> mStandCharaList = new List<EventStandCharaController2>();
    private List<RectTransform> mStandCharaTransformList = new List<RectTransform>();
    private List<GameObject> charaList = new List<GameObject>();
    private List<Color> BodyColorList = new List<Color>();
    private List<Color> FaceColorList = new List<Color>();
    [HideInInspector]
    public bool MoveEnabled = true;
    [HideInInspector]
    public AnimationCurve MoveCurve = AnimationCurve.Linear(0.0f, 0.0f, 1f, 1f);
    [HideInInspector]
    public Vector2 MoveTo = new Vector2(0.0f, 0.0f);
    private bool MoveFoldout = true;
    private List<Vector2> FromAnchorMinList = new List<Vector2>();
    private List<Vector2> FromAnchorMaxList = new List<Vector2>();
    private List<Vector2> mToAnchorList = new List<Vector2>();
    [HideInInspector]
    public AnimationCurve ScaleCurve = AnimationCurve.Linear(0.0f, 0.0f, 1f, 1f);
    [HideInInspector]
    public Vector2 ScaleTo = new Vector2(1f, 1f);
    private List<float> FromWidthList = new List<float>();
    private List<float> FromHeightList = new List<float>();
    [HideInInspector]
    public AnimationCurve ColorCurve = AnimationCurve.Linear(0.0f, 0.0f, 1f, 1f);
    [HideInInspector]
    public Color ColorTo = Color.get_white();
    [HideInInspector]
    public string[] CharaIDs;
    [HideInInspector]
    public bool async;
    [HideInInspector]
    public bool Flip;
    [HideInInspector]
    public float MoveTime;
    [HideInInspector]
    public bool Relative;
    private float MoveOffset;
    [HideInInspector]
    public bool ScaleEnabled;
    [HideInInspector]
    public float ScaleTime;
    private bool ScaleFoldout;
    private float ScaleOffset;
    private float mToWidth;
    private float mToHeght;
    [HideInInspector]
    public bool ColorEnabled;
    [HideInInspector]
    public float ColorTime;
    private bool ColorFoldout;
    private float ColorOffset;
    private Color mToColor;
    private bool mMoveEnabled;
    private bool mScaleEnabled;
    private bool mColorEnabled;

    public override void PreStart()
    {
      for (int index = 0; index < this.CharaIDs.Length; ++index)
        this.mStandCharaList.Add(EventStandCharaController2.FindInstances(this.CharaIDs[index]));
    }

    public override void OnActivate()
    {
      if (this.mStandCharaList.Count <= 0)
      {
        this.ActivateNext();
      }
      else
      {
        this.mMoveEnabled = this.MoveEnabled;
        this.mScaleEnabled = this.ScaleEnabled;
        this.mColorEnabled = this.ColorEnabled;
        for (int index = 0; index < this.mStandCharaList.Count; ++index)
        {
          if (Object.op_Inequality((Object) this.mStandCharaList[index], (Object) null))
            this.mStandCharaTransformList.Add((RectTransform) ((Component) this.mStandCharaList[index]).GetComponent<RectTransform>());
        }
        if (this.Flip)
        {
          for (int index = 0; index < this.mStandCharaTransformList.Count; ++index)
            ((Transform) this.mStandCharaTransformList[index]).Rotate(new Vector3(0.0f, 180f, 0.0f));
        }
        if (this.mMoveEnabled)
        {
          for (int index = 0; index < this.mStandCharaTransformList.Count; ++index)
          {
            this.FromAnchorMinList.Add(this.mStandCharaTransformList[index].get_anchorMin());
            this.FromAnchorMaxList.Add(this.mStandCharaTransformList[index].get_anchorMax());
            if (this.Relative)
              this.mToAnchorList.Add(Vector2.op_Addition(this.mStandCharaTransformList[index].get_anchorMin(), Vector2.Scale(this.MoveTo, new Vector2(0.5f, 1f))));
            else
              this.mToAnchorList.Add(this.convertPosition(this.MoveTo));
          }
          if ((double) this.MoveTime <= 0.0)
            this.MoveOffset = 1f;
        }
        if (this.mScaleEnabled)
        {
          for (int index = 0; index < this.mStandCharaTransformList.Count; ++index)
          {
            this.FromWidthList.Add((float) ((Transform) this.mStandCharaTransformList[index]).get_localScale().x);
            this.FromHeightList.Add((float) ((Transform) this.mStandCharaTransformList[index]).get_localScale().y);
          }
          this.mToWidth = (float) this.ScaleTo.x;
          this.mToHeght = (float) this.ScaleTo.y;
          if ((double) this.ScaleTime <= 0.0)
            this.ScaleOffset = 1f;
        }
        if (this.mColorEnabled)
        {
          for (int index = 0; index < this.mStandCharaList.Count; ++index)
          {
            if (Object.op_Inequality((Object) this.mStandCharaList[index], (Object) null))
              this.charaList.AddRange((IEnumerable<GameObject>) this.mStandCharaList[index].StandCharaList);
          }
          for (int index = 0; index < this.charaList.Count; ++index)
          {
            if (Object.op_Inequality((Object) ((EventStandChara2) this.charaList[index].GetComponent<EventStandChara2>()).BodyObject, (Object) null))
              this.BodyColorList.Add(((Graphic) ((EventStandChara2) this.charaList[index].GetComponent<EventStandChara2>()).BodyObject.GetComponent<RawImage>()).get_color());
            else
              this.BodyColorList.Add(Color.get_white());
            if (Object.op_Inequality((Object) ((EventStandChara2) this.charaList[index].GetComponent<EventStandChara2>()).FaceObject, (Object) null))
              this.FaceColorList.Add(((Graphic) ((EventStandChara2) this.charaList[index].GetComponent<EventStandChara2>()).FaceObject.GetComponent<RawImage>()).get_color());
            else
              this.FaceColorList.Add(Color.get_white());
          }
          this.mToColor = this.ColorTo;
          for (int index = 0; index < this.CharaIDs.Length; ++index)
          {
            string charaId = this.CharaIDs[index];
            if (Event2dAction_OperateStandChara.CharaColorDic.ContainsKey(charaId))
              Event2dAction_OperateStandChara.CharaColorDic[charaId] = this.ColorTo;
            else
              Event2dAction_OperateStandChara.CharaColorDic.Add(charaId, this.ColorTo);
          }
          if ((double) this.ColorTime <= 0.0)
            this.ColorOffset = 1f;
        }
        if (!this.async)
          return;
        this.ActivateNext(true);
      }
    }

    public override void Update()
    {
      if (this.mMoveEnabled)
      {
        if ((double) this.MoveOffset >= 1.0)
        {
          this.MoveOffset = 1f;
          this.mMoveEnabled = false;
        }
        float num = this.MoveCurve.Evaluate(this.MoveOffset);
        for (int index = 0; index < this.mStandCharaTransformList.Count; ++index)
        {
          this.mStandCharaTransformList[index].set_anchorMin(Vector2.op_Addition(this.FromAnchorMinList[index], Vector2.Scale(Vector2.op_Subtraction(this.mToAnchorList[index], this.FromAnchorMinList[index]), new Vector2(num, num))));
          this.mStandCharaTransformList[index].set_anchorMax(Vector2.op_Addition(this.FromAnchorMaxList[index], Vector2.Scale(Vector2.op_Subtraction(this.mToAnchorList[index], this.FromAnchorMaxList[index]), new Vector2(num, num))));
        }
        this.MoveOffset += Time.get_deltaTime() / this.MoveTime;
      }
      if (this.mScaleEnabled)
      {
        if ((double) this.ScaleOffset >= 1.0)
        {
          this.ScaleOffset = 1f;
          this.mScaleEnabled = false;
        }
        float num = this.ScaleCurve.Evaluate(this.ScaleOffset);
        for (int index = 0; index < this.mStandCharaTransformList.Count; ++index)
        {
          Vector3 vector3;
          // ISSUE: explicit reference operation
          ((Vector3) @vector3).\u002Ector(this.FromWidthList[index] + (this.mToWidth - this.FromWidthList[index]) * num, this.FromHeightList[index] + (this.mToHeght - this.FromHeightList[index]) * num, 1f);
          ((Transform) this.mStandCharaTransformList[index]).set_localScale(vector3);
        }
        this.ScaleOffset += Time.get_deltaTime() / this.ScaleTime;
      }
      if (this.mColorEnabled)
      {
        if ((double) this.ColorOffset >= 1.0)
        {
          this.ColorOffset = 1f;
          this.mColorEnabled = false;
        }
        float num = this.ColorCurve.Evaluate(this.ColorOffset);
        for (int index = 0; index < this.charaList.Count; ++index)
        {
          if (Object.op_Inequality((Object) ((EventStandChara2) this.charaList[index].GetComponent<EventStandChara2>()).BodyObject, (Object) null))
          {
            Color color = Color.Lerp(this.BodyColorList[index], this.mToColor, num);
            ((Graphic) ((EventStandChara2) this.charaList[index].GetComponent<EventStandChara2>()).BodyObject.GetComponent<RawImage>()).set_color(color);
          }
          if (Object.op_Inequality((Object) ((EventStandChara2) this.charaList[index].GetComponent<EventStandChara2>()).FaceObject, (Object) null))
          {
            Color color = Color.Lerp(this.FaceColorList[index], this.mToColor, num);
            ((Graphic) ((EventStandChara2) this.charaList[index].GetComponent<EventStandChara2>()).FaceObject.GetComponent<RawImage>()).set_color(color);
          }
        }
        this.ColorOffset += Time.get_deltaTime() / this.ColorTime;
      }
      if (this.mMoveEnabled || this.mScaleEnabled || this.mColorEnabled)
        return;
      if (this.async)
        this.enabled = false;
      else
        this.ActivateNext();
    }

    private Vector2 convertPosition(Vector2 pos)
    {
      Vector2 vector2;
      // ISSUE: explicit reference operation
      ((Vector2) @vector2).\u002Ector((float) (pos.x + 1.0), (float) (pos.y * 2.0));
      return Vector2.Scale(vector2, new Vector2(0.5f, 0.5f));
    }
  }
}

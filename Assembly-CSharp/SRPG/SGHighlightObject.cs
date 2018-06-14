// Decompiled with JetBrains decompiler
// Type: SRPG.SGHighlightObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class SGHighlightObject : MonoBehaviour
  {
    private static float smallHighlightValue = 0.7f;
    public GameObject highlightedObject;
    public Canvas canvas;
    public IntVector2 highlightedGrid;
    [SerializeField]
    private GameObject shadowTop;
    [SerializeField]
    private GameObject shadowRight;
    [SerializeField]
    private GameObject shadowLeft;
    [SerializeField]
    private GameObject shadowBottom;
    [SerializeField]
    private GameObject blockAll;
    [SerializeField]
    private GameObject highlightArrow;
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private Text noPortraitText;
    [SerializeField]
    private Text portraitText;
    [SerializeField]
    private EventDialogBubble dialogBubble;
    [SerializeField]
    private GameObject dialogGroup;
    private bool isHighlighted;
    private bool isPortraitVisible;
    private bool isSmallerHighlight;
    private bool isOverlayInteractable;
    private bool buttonDown;
    private bool floorValues;
    private float holdDownTime;
    private float elapsedAnimTime;
    private float animTime;
    private SGHighlightObject.OnActivateCallback onActivate;
    private LoadRequest mPortraitResource;
    private string mTextData;
    private UnitParam mUnit;
    private string TextID;
    private string UnitID;
    private Vector2 DialogSize;
    private Vector3 lastHighlightPosition;
    private EventDialogBubble.Anchors DialogBubbleAnchor;
    private Camera uiCam;
    private static SGHighlightObject instance;
    private UnityAction clickAction;
    private GraphicRaycaster rayCaster;

    public SGHighlightObject()
    {
      base.\u002Ector();
    }

    public static SGHighlightObject Instance()
    {
      return SGHighlightObject.instance;
    }

    public void Highlight(string unitID, string textID, SGHighlightObject.OnActivateCallback callback, EventDialogBubble.Anchors dialogBubbleAnchor, bool isPortraitShown = true, bool isInteractable = false, bool isSmaller = false)
    {
      RectTransform rectTransform = (RectTransform) null;
      if (Object.op_Inequality((Object) this.highlightedObject, (Object) null))
        rectTransform = (RectTransform) this.highlightedObject.GetComponent<RectTransform>();
      if (Object.op_Inequality((Object) rectTransform, (Object) null) || true)
      {
        this.isHighlighted = true;
        this.shadowTop.SetActive(false);
        this.shadowRight.SetActive(false);
        this.shadowLeft.SetActive(false);
        this.shadowBottom.SetActive(false);
        this.UnitID = unitID;
        this.TextID = textID;
        AnalyticsManager.TrackTutorialAnalyticsEvent(this.TextID);
        this.DialogBubbleAnchor = dialogBubbleAnchor == EventDialogBubble.Anchors.None ? EventDialogBubble.Anchors.None : dialogBubbleAnchor;
        this.onActivate = callback;
        this.uiCam = Camera.get_main();
        this.isPortraitVisible = isPortraitShown;
        this.isSmallerHighlight = isSmaller;
      }
      else
        this.HideHighlight();
      if (isInteractable)
      {
        this.onActivate = callback;
        this.isOverlayInteractable = true;
      }
      ((Component) this.nextButton).get_gameObject().SetActive(isInteractable);
      if (!string.IsNullOrEmpty(unitID))
        this.mUnit = MonoSingleton<GameManager>.Instance.GetUnitParam(unitID);
      this.elapsedAnimTime = 0.0f;
      this.StartCoroutine(this.LoadAssets());
      this.floorValues = false;
      if (MonoSingleton<GameManager>.Instance.GetNextTutorialStep() == "ShowCollectGachaMissionReward")
        this.floorValues = true;
      this.rayCaster = (GraphicRaycaster) ((Component) this).get_gameObject().GetComponent<GraphicRaycaster>();
    }

    private void HideHighlight()
    {
      Rect pixelRect1 = this.canvas.get_pixelRect();
      // ISSUE: explicit reference operation
      float width = ((Rect) @pixelRect1).get_width();
      Rect pixelRect2 = this.canvas.get_pixelRect();
      // ISSUE: explicit reference operation
      float height = ((Rect) @pixelRect2).get_height();
      this.isHighlighted = false;
      ((RectTransform) this.shadowTop.GetComponent<RectTransform>()).set_offsetMin(new Vector2(0.0f, 0.0f));
      ((RectTransform) this.shadowTop.GetComponent<RectTransform>()).set_offsetMax(new Vector2(0.0f, 0.0f));
      ((RectTransform) this.shadowRight.GetComponent<RectTransform>()).set_offsetMin(new Vector2(width, 0.0f));
      ((RectTransform) this.shadowRight.GetComponent<RectTransform>()).set_offsetMax(new Vector2(0.0f, 0.0f));
      ((RectTransform) this.shadowLeft.GetComponent<RectTransform>()).set_offsetMin(new Vector2(0.0f, height));
      ((RectTransform) this.shadowLeft.GetComponent<RectTransform>()).set_offsetMax(new Vector2(0.0f, 0.0f));
      ((RectTransform) this.shadowBottom.GetComponent<RectTransform>()).set_offsetMin(new Vector2(0.0f, height));
      ((RectTransform) this.shadowBottom.GetComponent<RectTransform>()).set_offsetMax(new Vector2(0.0f, 0.0f));
      this.shadowTop.SetActive(false);
      this.shadowRight.SetActive(false);
      this.shadowLeft.SetActive(false);
      this.shadowBottom.SetActive(false);
      this.blockAll.SetActive(true);
      this.highlightArrow.get_gameObject().SetActive(false);
    }

    private void Awake()
    {
      this.isHighlighted = false;
      this.uiCam = (Camera) null;
      SGHighlightObject.instance = this;
      this.canvas = (Canvas) ((Component) this).get_gameObject().GetComponent<Canvas>();
      // ISSUE: method pointer
      this.clickAction = new UnityAction((object) this, __methodptr(OnClick));
      ((UnityEvent) ((Button) ((Component) this.canvas).get_gameObject().AddComponent<Button>()).get_onClick()).AddListener(this.clickAction);
      if (Object.op_Inequality((Object) this.nextButton, (Object) null))
        ((UnityEvent) this.nextButton.get_onClick()).AddListener(this.clickAction);
      HoldGesture holdGesture = (HoldGesture) ((Component) this.canvas).get_gameObject().AddComponent<HoldGesture>();
      if (!Object.op_Inequality((Object) holdGesture, (Object) null))
        return;
      // ISSUE: method pointer
      UnityAction unityAction1 = new UnityAction((object) this, __methodptr(OnDown));
      holdGesture.OnHoldStart = new UnityEvent();
      holdGesture.OnHoldStart.AddListener(unityAction1);
      // ISSUE: method pointer
      UnityAction unityAction2 = new UnityAction((object) this, __methodptr(OnUp));
      holdGesture.OnHoldEnd = new UnityEvent();
      holdGesture.OnHoldEnd.AddListener(unityAction2);
    }

    private void OnDown()
    {
      this.buttonDown = true;
      this.holdDownTime = Time.get_time();
    }

    private void OnUp()
    {
      this.buttonDown = false;
    }

    private void OnClick()
    {
      if (!Object.op_Inequality((Object) this.dialogBubble, (Object) null))
        return;
      if (this.dialogBubble.Finished)
      {
        if (this.isOverlayInteractable && this.onActivate != null)
        {
          this.isOverlayInteractable = false;
          this.onActivate();
        }
        this.dialogBubble.Forward();
      }
      else
      {
        if (!this.dialogBubble.IsPrinting)
          return;
        this.dialogBubble.Skip();
      }
    }

    private void Start()
    {
      if (this.isHighlighted)
        return;
      this.Highlight((string) null, (string) null, (SGHighlightObject.OnActivateCallback) null, EventDialogBubble.Anchors.None, true, false, false);
    }

    private void Update()
    {
      if (!this.dialogBubble.Finished && this.buttonDown && (double) Time.get_time() - (double) this.holdDownTime > 0.200000002980232)
        this.dialogBubble.Skip();
      if (Object.op_Inequality((Object) this.rayCaster, (Object) null) && !((Behaviour) this.rayCaster).get_enabled())
        ((Behaviour) this.rayCaster).set_enabled(true);
      this.canvas.set_sortingOrder(50);
      if (!this.isHighlighted)
        return;
      if (this.DialogBubbleAnchor != EventDialogBubble.Anchors.None)
        this.dialogBubble.Anchor = this.DialogBubbleAnchor;
      if (Object.op_Inequality((Object) this.highlightedObject, (Object) null))
      {
        RectTransform component = (RectTransform) this.highlightedObject.GetComponent<RectTransform>();
        Rect rect1 = component.get_rect();
        // ISSUE: explicit reference operation
        float width = ((Rect) @rect1).get_width();
        // ISSUE: explicit reference operation
        float height = ((Rect) @rect1).get_height();
        if ((double) this.canvas.get_scaleFactor() != 0.0)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Rect& local1 = @rect1;
          ((Rect) local1).set_x(((Rect) local1).get_x() + (float) ((Transform) component).get_position().x / this.canvas.get_scaleFactor());
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Rect& local2 = @rect1;
          ((Rect) local2).set_y(((Rect) local2).get_y() + (float) ((Transform) component).get_position().y / this.canvas.get_scaleFactor());
          width *= this.canvas.get_scaleFactor();
          height *= this.canvas.get_scaleFactor();
        }
        this.HighlightArea(rect1);
        Vector3 position = ((Component) component).get_transform().get_position();
        Vector2 pivot = ((RectTransform) this.highlightArrow.get_transform()).get_pivot();
        float num1 = 0.0f;
        float num2 = 0.5f;
        float num3 = num1 + (float) (pivot.x - component.get_pivot().x);
        float num4 = num2 + (float) (pivot.y - component.get_pivot().y);
        Vector3 localScale = this.highlightArrow.get_transform().get_localScale();
        float num5 = 80f * this.canvas.get_scaleFactor();
        Rect rect2 = component.get_rect();
        // ISSUE: explicit reference operation
        float num6 = ((Rect) @rect2).get_height() * (float) (1.0 - component.get_pivot().y) * this.canvas.get_scaleFactor();
        float num7 = (float) position.y + num6;
        Rect pixelRect = this.canvas.get_pixelRect();
        // ISSUE: explicit reference operation
        if ((double) ((Rect) @pixelRect).get_height() - (double) num7 < (double) num5)
        {
          num4 *= -1f;
          localScale.y = (__Null) -1.0;
          this.highlightArrow.get_transform().set_localScale(localScale);
        }
        else
        {
          localScale.y = (__Null) 1.0;
          this.highlightArrow.get_transform().set_localScale(localScale);
        }
        if (!this.isSmallerHighlight)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector3& local1 = @position;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local1).y = (__Null) ((^local1).y + (double) height * (double) num4);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector3& local2 = @position;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local2).x = (__Null) ((^local2).x + (double) width * (double) num3);
        }
        else
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector3& local1 = @position;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local1).x = (__Null) ((^local1).x + (double) width * (double) num3 * (double) SGHighlightObject.smallHighlightValue);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector3& local2 = @position;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local2).y = (__Null) ((^local2).y + (double) height * (double) num4 * (double) SGHighlightObject.smallHighlightValue);
        }
        this.highlightArrow.get_transform().set_position(position);
      }
      else if (this.highlightedGrid.x != 0 && this.highlightedGrid.y != 0)
      {
        if (this.DialogBubbleAnchor != EventDialogBubble.Anchors.None)
          this.dialogBubble.Anchor = this.DialogBubbleAnchor;
        SceneBattle instance = SceneBattle.Instance;
        int x = this.highlightedGrid.x;
        int y = this.highlightedGrid.y;
        Vector3 vector3_1 = instance.CalcGridCenter(x - 1, y);
        Vector3 vector3_2 = instance.CalcGridCenter(x, y - 1);
        Vector3 vector3_3 = instance.CalcGridCenter(x, y + 1);
        Vector3 vector3_4 = instance.CalcGridCenter(x, y - 1);
        if (!Object.op_Inequality((Object) this.uiCam, (Object) null))
          return;
        Vector3 screenPoint1 = this.uiCam.WorldToScreenPoint(vector3_1);
        Vector3 screenPoint2 = this.uiCam.WorldToScreenPoint(vector3_2);
        Vector3 screenPoint3 = this.uiCam.WorldToScreenPoint(vector3_3);
        Vector3 screenPoint4 = this.uiCam.WorldToScreenPoint(vector3_4);
        Rect rect1 = (Rect) null;
        // ISSUE: explicit reference operation
        ((Rect) @rect1).set_position(new Vector2((float) screenPoint4.x, (float) screenPoint4.y));
        // ISSUE: explicit reference operation
        ((Rect) @rect1).set_width((float) (screenPoint3.x - screenPoint1.x));
        // ISSUE: explicit reference operation
        ((Rect) @rect1).set_height((float) -(screenPoint2.y - screenPoint1.y));
        Rect rect2 = rect1;
        if ((double) this.canvas.get_scaleFactor() != 0.0)
        {
          RectTransform transform = ((Component) this.canvas).get_transform() as RectTransform;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Rect& local1 = @rect2;
          // ISSUE: explicit reference operation
          double num1 = (double) ((Rect) @rect1).get_x() / (double) Screen.get_width();
          Rect rect3 = transform.get_rect();
          // ISSUE: explicit reference operation
          double width = (double) ((Rect) @rect3).get_width();
          double num2 = num1 * width;
          ((Rect) local1).set_x((float) num2);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Rect& local2 = @rect2;
          // ISSUE: explicit reference operation
          double num3 = (double) ((Rect) @rect1).get_y() / (double) Screen.get_height();
          Rect rect4 = transform.get_rect();
          // ISSUE: explicit reference operation
          double height = (double) ((Rect) @rect4).get_height();
          double num4 = num3 * height;
          ((Rect) local2).set_y((float) num4);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Rect& local3 = @rect2;
          ((Rect) local3).set_width(((Rect) local3).get_width() / this.canvas.get_scaleFactor());
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Rect& local4 = @rect2;
          ((Rect) local4).set_height(((Rect) local4).get_height() / this.canvas.get_scaleFactor());
        }
        this.HighlightArea(rect2);
        // ISSUE: explicit reference operation
        Vector3 vector3_5 = Vector2.op_Implicit(((Rect) @rect1).get_position());
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector3& local5 = @vector3_5;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local5).y = (__Null) ((^local5).y + (double) ((Rect) @rect1).get_height());
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector3& local6 = @vector3_5;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local6).x = (__Null) ((^local6).x + (double) ((Rect) @rect1).get_width() / 2.0);
        this.highlightArrow.get_transform().set_position(vector3_5);
      }
      else
      {
        this.HideHighlight();
        this.blockAll.SetActive(true);
      }
    }

    private void HighlightArea(Rect rect)
    {
      Rect pixelRect1 = this.canvas.get_pixelRect();
      // ISSUE: explicit reference operation
      float width = ((Rect) @pixelRect1).get_width();
      Rect pixelRect2 = this.canvas.get_pixelRect();
      // ISSUE: explicit reference operation
      float height = ((Rect) @pixelRect2).get_height();
      if (this.isSmallerHighlight)
      {
        float num = (float) ((1.0 - (double) SGHighlightObject.smallHighlightValue) / 2.0);
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Rect& local1 = @rect;
        // ISSUE: explicit reference operation
        ((Rect) local1).set_x(((Rect) local1).get_x() + ((Rect) @rect).get_width() * num);
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Rect& local2 = @rect;
        // ISSUE: explicit reference operation
        ((Rect) local2).set_y(((Rect) local2).get_y() + ((Rect) @rect).get_height() * num);
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Rect& local3 = @rect;
        ((Rect) local3).set_width(((Rect) local3).get_width() * SGHighlightObject.smallHighlightValue);
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Rect& local4 = @rect;
        ((Rect) local4).set_height(((Rect) local4).get_height() * SGHighlightObject.smallHighlightValue);
      }
      if ((double) this.canvas.get_scaleFactor() != 0.0)
      {
        width /= this.canvas.get_scaleFactor();
        height /= this.canvas.get_scaleFactor();
      }
      // ISSUE: explicit reference operation
      float num1 = ((Rect) @rect).get_yMax();
      // ISSUE: explicit reference operation
      float num2 = ((Rect) @rect).get_xMax();
      // ISSUE: explicit reference operation
      float num3 = (float) -((double) width - (double) ((Rect) @rect).get_xMin());
      // ISSUE: explicit reference operation
      float num4 = (float) -((double) height - (double) ((Rect) @rect).get_yMin());
      if ((double) this.elapsedAnimTime < (double) this.animTime)
      {
        this.blockAll.SetActive(true);
        if (this.dialogBubble.Finished)
          this.elapsedAnimTime += Time.get_smoothDeltaTime();
        float num5 = height;
        float num6 = width;
        float num7 = -width;
        float num8 = -height;
        // ISSUE: explicit reference operation
        float yMax = ((Rect) @rect).get_yMax();
        // ISSUE: explicit reference operation
        float xMax = ((Rect) @rect).get_xMax();
        // ISSUE: explicit reference operation
        float num9 = (float) -((double) width - (double) ((Rect) @rect).get_xMin());
        // ISSUE: explicit reference operation
        float num10 = (float) -((double) height - (double) ((Rect) @rect).get_yMin());
        num1 = Mathf.Lerp(num5, yMax, this.elapsedAnimTime / this.animTime);
        num2 = Mathf.Lerp(num6, xMax, this.elapsedAnimTime / this.animTime);
        num3 = Mathf.Lerp(num7, num9, this.elapsedAnimTime / this.animTime);
        num4 = Mathf.Lerp(num8, num10, this.elapsedAnimTime / this.animTime);
        this.highlightArrow.get_gameObject().SetActive(false);
      }
      else
      {
        this.highlightArrow.get_gameObject().SetActive(true);
        if (!((Component) this.nextButton).get_gameObject().GetActive())
          this.blockAll.SetActive(false);
      }
      // ISSUE: explicit reference operation
      float num11 = (float) -((double) height - (double) ((Rect) @rect).get_yMax());
      // ISSUE: explicit reference operation
      float yMin = ((Rect) @rect).get_yMin();
      ((RectTransform) this.shadowTop.GetComponent<RectTransform>()).set_offsetMin(new Vector2(0.0f, num1));
      ((RectTransform) this.shadowTop.GetComponent<RectTransform>()).set_offsetMax(new Vector2(0.0f, 0.0f));
      ((RectTransform) this.shadowRight.GetComponent<RectTransform>()).set_offsetMin(new Vector2(num2, 0.0f));
      ((RectTransform) this.shadowRight.GetComponent<RectTransform>()).set_offsetMax(new Vector2(0.0f, num11));
      ((RectTransform) this.shadowLeft.GetComponent<RectTransform>()).set_offsetMin(new Vector2(0.0f, yMin));
      ((RectTransform) this.shadowLeft.GetComponent<RectTransform>()).set_offsetMax(new Vector2(num3, num11));
      ((RectTransform) this.shadowBottom.GetComponent<RectTransform>()).set_offsetMin(new Vector2(0.0f, 0.0f));
      // ISSUE: explicit reference operation
      ((RectTransform) this.shadowBottom.GetComponent<RectTransform>()).set_offsetMax(new Vector2((float) -((double) width - (double) ((Rect) @rect).get_xMax()), num4));
    }

    public void Activated(int pinID)
    {
      if (pinID != 0 || this.onActivate == null)
        return;
      this.onActivate();
    }

    private void LoadTextData(string TextID)
    {
      if (!string.IsNullOrEmpty(TextID))
        this.mTextData = LocalizedText.Get(TextID).Split('\t')[0];
      else
        this.mTextData = (string) null;
    }

    [DebuggerHidden]
    private IEnumerator LoadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SGHighlightObject.\u003CLoadAssets\u003Ec__Iterator4E() { \u003C\u003Ef__this = this };
    }

    public void GridSelected(int gridX, int gridY)
    {
      if (this.onActivate == null)
        return;
      this.onActivate();
    }

    public delegate void OnActivateCallback();
  }
}

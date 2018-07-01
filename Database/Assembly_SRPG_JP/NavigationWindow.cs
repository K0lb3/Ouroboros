// Decompiled with JetBrains decompiler
// Type: SRPG.NavigationWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class NavigationWindow : MonoBehaviour
  {
    private const int CanvasPriority = 5000;
    private static NavigationWindow mCurrent;
    private static int mNumNavigations;
    private static GameObject mNavigationCanvas;
    public Text Text;
    public float DestroyDelay;
    public string HideTrigger;
    public Vector2 Margin;
    private Animator mAnimator;
    private RectTransform mRect;

    public NavigationWindow()
    {
      base.\u002Ector();
    }

    public static void DiscardCurrent()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) NavigationWindow.mCurrent, (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) NavigationWindow.mCurrent).get_gameObject(), NavigationWindow.mCurrent.DestroyDelay);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) NavigationWindow.mCurrent.mAnimator, (UnityEngine.Object) null) && !string.IsNullOrEmpty(NavigationWindow.mCurrent.HideTrigger))
        NavigationWindow.mCurrent.mAnimator.SetTrigger(NavigationWindow.mCurrent.HideTrigger);
      NavigationWindow.mCurrent = (NavigationWindow) null;
    }

    public static void Show(NavigationWindow template, string text, NavigationWindow.Alignment align)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) template, (UnityEngine.Object) null))
        return;
      if (NavigationWindow.mNumNavigations == 0)
      {
        NavigationWindow.mNavigationCanvas = new GameObject("NavigationCanvas", new System.Type[2]
        {
          typeof (Canvas),
          typeof (SRPG_CanvasScaler)
        });
        Canvas component = (Canvas) NavigationWindow.mNavigationCanvas.GetComponent<Canvas>();
        component.set_renderMode((RenderMode) 0);
        component.set_sortingOrder(5000);
        UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) NavigationWindow.mNavigationCanvas);
      }
      NavigationWindow navigationWindow = (NavigationWindow) UnityEngine.Object.Instantiate<NavigationWindow>((M0) template);
      navigationWindow.SetAlignment(align);
      navigationWindow.SetText(text);
      ((Component) navigationWindow).get_transform().SetParent(NavigationWindow.mNavigationCanvas.get_transform(), false);
      ++NavigationWindow.mNumNavigations;
    }

    private void Awake()
    {
      this.mRect = (RectTransform) ((Component) this).GetComponent<RectTransform>();
      this.mAnimator = (Animator) ((Component) this).GetComponent<Animator>();
    }

    private void Start()
    {
      NavigationWindow.DiscardCurrent();
      NavigationWindow.mCurrent = this;
    }

    private void OnDestroy()
    {
      --NavigationWindow.mNumNavigations;
      if (NavigationWindow.mNumNavigations != 0)
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) NavigationWindow.mNavigationCanvas.get_gameObject());
      NavigationWindow.mNavigationCanvas = (GameObject) null;
    }

    private void SetText(string text)
    {
      this.Text.set_text(text);
    }

    private void SetAlignment(NavigationWindow.Alignment alignment)
    {
      Vector2 zero1 = Vector2.get_zero();
      Vector2 zero2 = Vector2.get_zero();
      Vector2 zero3 = Vector2.get_zero();
      int num1 = (int) (alignment & (NavigationWindow.Alignment.Bottom | NavigationWindow.Alignment.BottomRight));
      int num2 = (int) alignment >> 2 & 3;
      switch (num1)
      {
        case 0:
          zero1.x = (__Null) 0.0;
          zero2.x = (__Null) 0.0;
          zero3.x = this.Margin.x;
          break;
        case 1:
          zero1.x = (__Null) 0.5;
          zero2.x = (__Null) 0.5;
          break;
        case 2:
          zero1.x = (__Null) 1.0;
          zero2.x = (__Null) 1.0;
          zero3.x = -this.Margin.x;
          break;
      }
      switch (num2)
      {
        case 0:
          zero1.y = (__Null) 0.0;
          zero2.y = (__Null) 0.0;
          zero3.y = this.Margin.y;
          break;
        case 1:
          zero1.y = (__Null) 0.5;
          zero2.y = (__Null) 0.5;
          break;
        case 2:
          zero1.y = (__Null) 1.0;
          zero2.y = (__Null) 1.0;
          zero3.y = -this.Margin.y;
          break;
      }
      RectTransform mRect = this.mRect;
      Vector2 vector2_1 = zero1;
      this.mRect.set_anchorMax(vector2_1);
      Vector2 vector2_2 = vector2_1;
      mRect.set_anchorMin(vector2_2);
      this.mRect.set_pivot(zero2);
      this.mRect.set_anchoredPosition(zero3);
    }

    public enum Alignment
    {
      BottomLeft = 0,
      Bottom = 1,
      BottomRight = 2,
      MiddleLeft = 4,
      Middle = 5,
      MiddleRight = 6,
      TopLeft = 8,
      Top = 9,
      TopRight = 10, // 0x0000000A
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: UIProjector
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("UI/Link UI Position")]
public class UIProjector : MonoBehaviour
{
  public Camera ProjectCamera;
  public RectTransform UIObject;
  public string UIObjectID;
  private Canvas mCanvas;
  public bool AutoDestroyUIObject;
  public Vector3 LocalOffset;

  public UIProjector()
  {
    base.\u002Ector();
  }

  public void SetCanvas(Canvas canvas)
  {
    this.mCanvas = canvas;
    if (!Object.op_Inequality((Object) this.UIObject, (Object) null))
      return;
    ((Transform) this.UIObject).SetParent(!Object.op_Inequality((Object) this.mCanvas, (Object) null) ? (Transform) null : ((Component) this.mCanvas).get_transform(), false);
  }

  protected virtual void Awake()
  {
    this.AutoDestroyUIObject = Object.op_Equality((Object) this.UIObject, (Object) null);
  }

  protected virtual void Start()
  {
    if (Object.op_Equality((Object) this.UIObject, (Object) null) && !string.IsNullOrEmpty(this.UIObjectID))
    {
      GameObject gameObject = GameObjectID.FindGameObject(this.UIObjectID);
      if (Object.op_Inequality((Object) gameObject, (Object) null))
        this.UIObject = (RectTransform) gameObject.GetComponent<RectTransform>();
    }
    if (Object.op_Equality((Object) this.mCanvas, (Object) null) && Object.op_Inequality((Object) this.UIObject, (Object) null))
      this.mCanvas = (Canvas) ((Component) this.UIObject).GetComponentInParent<Canvas>();
    if (Object.op_Inequality((Object) this.mCanvas, (Object) null) && Object.op_Equality((Object) this.UIObject, (Object) null))
    {
      this.UIObject = new GameObject(((Object) ((Component) this).get_gameObject()).get_name(), new System.Type[1]
      {
        typeof (RectTransform)
      }).get_transform() as RectTransform;
      ((Transform) this.UIObject).SetParent(((Component) this.mCanvas).get_transform(), false);
    }
    CameraHook.AddPreCullEventListener(new CameraHook.PreCullEvent(this.PreCull));
  }

  protected virtual void OnDestroy()
  {
    CameraHook.RemovePreCullEventListener(new CameraHook.PreCullEvent(this.PreCull));
    if (!this.AutoDestroyUIObject)
      return;
    GameUtility.DestroyGameObject((Component) this.UIObject);
  }

  public void PreCull(Camera camera)
  {
    if (Object.op_Inequality((Object) this.ProjectCamera, (Object) null) && Object.op_Inequality((Object) camera, (Object) this.ProjectCamera) || !Object.op_Inequality((Object) this.UIObject, (Object) null))
      return;
    Transform transform1 = ((Component) this).get_transform();
    Vector3 vector3 = Vector3.op_Addition(transform1.get_position(), Vector3.op_Addition(Vector3.op_Addition(Vector3.op_Multiply((float) this.LocalOffset.x, transform1.get_right()), Vector3.op_Multiply((float) this.LocalOffset.y, transform1.get_up())), Vector3.op_Multiply((float) this.LocalOffset.z, transform1.get_forward())));
    RectTransform transform2 = ((Component) this.mCanvas).get_transform() as RectTransform;
    Vector3 screenPoint = camera.WorldToScreenPoint(vector3);
    // ISSUE: explicit reference operation
    // ISSUE: variable of a reference type
    Vector3& local1 = @screenPoint;
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^local1).x = (__Null) ((^local1).x / (double) Screen.get_width());
    // ISSUE: explicit reference operation
    // ISSUE: variable of a reference type
    Vector3& local2 = @screenPoint;
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^local2).y = (__Null) ((^local2).y / (double) Screen.get_height());
    this.UIObject.set_anchorMin(Vector2.get_zero());
    this.UIObject.set_anchorMax(Vector2.get_zero());
    RectTransform uiObject = this.UIObject;
    // ISSUE: variable of the null type
    __Null x = screenPoint.x;
    Rect rect1 = transform2.get_rect();
    // ISSUE: explicit reference operation
    double width = (double) ((Rect) @rect1).get_width();
    double num1 = x * width;
    // ISSUE: variable of the null type
    __Null y = screenPoint.y;
    Rect rect2 = transform2.get_rect();
    // ISSUE: explicit reference operation
    double height = (double) ((Rect) @rect2).get_height();
    double num2 = y * height;
    Vector2 vector2 = new Vector2((float) num1, (float) num2);
    uiObject.set_anchoredPosition(vector2);
  }

  public void ReStart()
  {
    this.Start();
  }
}

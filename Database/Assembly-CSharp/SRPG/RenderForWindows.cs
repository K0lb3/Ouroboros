// Decompiled with JetBrains decompiler
// Type: SRPG.RenderForWindows
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class RenderForWindows : MonoBehaviour
  {
    private const string MATERIAL_SHADER_NAME = "Unlit/Texture";
    private const string RAW_IMAGE_NAME = "RawImage";
    private const string CANVAS_NAME = "Canvas";
    [SerializeField]
    private RenderForWindows.eTargetType target_type;
    [SerializeField]
    private Camera target_camera;
    private int canvas_sort_order;
    private bool is_enable;
    private RenderTexture render_texture;
    private Canvas canvas;
    private RawImage image;
    private RectTransform img_rect_transform;
    private bool is_dont_create_render_texture;

    public RenderForWindows()
    {
      base.\u002Ector();
    }

    public bool IsDontCreteRenderTexture
    {
      set
      {
        this.is_dont_create_render_texture = value;
      }
    }

    private void Awake()
    {
      if (!this.is_enable)
        return;
      this.canvas = this.CreateCanvas();
      this.image = this.CreateRawImage(((Component) this.canvas).get_transform(), (float) Screen.get_width(), (float) Screen.get_height());
      this.img_rect_transform = ((Component) this.image).get_transform() as RectTransform;
    }

    private void Start()
    {
      if (!this.is_enable)
        return;
      this.Init();
    }

    private void LateUpdate()
    {
      if (!this.is_enable || Object.op_Equality((Object) this.img_rect_transform, (Object) null) || this.img_rect_transform.get_sizeDelta().x == (double) Screen.get_width() && this.img_rect_transform.get_sizeDelta().y == (double) Screen.get_height())
        return;
      this.img_rect_transform.set_sizeDelta(new Vector2((float) Screen.get_width(), (float) Screen.get_height()));
    }

    public void SetTargetType(RenderForWindows.eTargetType _type, Camera _target_camera = null)
    {
      this.target_type = _type;
      if (this.target_type != RenderForWindows.eTargetType.SELECTED)
        return;
      this.target_camera = _target_camera;
    }

    public void SetRenderTexture(RenderTexture _render_texture)
    {
      this.render_texture = _render_texture;
      this.image.set_texture((Texture) this.render_texture);
    }

    private void Init()
    {
      if (!this.is_enable)
        return;
      switch (this.target_type)
      {
        case RenderForWindows.eTargetType.MAIN_CAMERA:
          this.target_camera = Camera.get_main();
          break;
        case RenderForWindows.eTargetType.SELECTED:
          this.target_camera = !Object.op_Inequality((Object) this.target_camera, (Object) null) ? Camera.get_main() : this.target_camera;
          break;
      }
      if (!Object.op_Equality((Object) this.render_texture, (Object) null) || this.is_dont_create_render_texture)
        return;
      this.render_texture = this.CreateRenderTexture(Screen.get_width(), Screen.get_height());
      this.target_camera.set_targetTexture(this.render_texture);
      this.image.set_texture((Texture) this.render_texture);
    }

    private RenderTexture CreateRenderTexture(int _width, int _height)
    {
      if (!this.is_enable)
        return (RenderTexture) null;
      RenderTexture renderTexture = new RenderTexture(_width, _height, 0);
      renderTexture.set_format((RenderTextureFormat) 0);
      renderTexture.set_depth(24);
      ((Texture) renderTexture).set_filterMode((FilterMode) 0);
      renderTexture.set_generateMips(false);
      renderTexture.set_useMipMap(false);
      if (renderTexture.Create())
        return renderTexture;
      DebugUtility.LogError("RenderTexture生成に失敗");
      return (RenderTexture) null;
    }

    private Canvas CreateCanvas()
    {
      if (!this.is_enable)
        return (Canvas) null;
      GameObject gameObject = new GameObject();
      ((Object) gameObject).set_name("Canvas");
      gameObject.get_transform().SetParent(((Component) this).get_transform().get_parent(), true);
      Canvas canvas = (Canvas) gameObject.AddComponent<Canvas>();
      canvas.set_renderMode((RenderMode) 0);
      canvas.set_sortingOrder(this.canvas_sort_order);
      return canvas;
    }

    private RawImage CreateRawImage(Transform _parent, float _width, float _height)
    {
      if (!this.is_enable)
        return (RawImage) null;
      GameObject gameObject = new GameObject();
      ((Object) gameObject).set_name("RawImage");
      gameObject.get_transform().SetParent(_parent, true);
      RawImage rawImage = (RawImage) gameObject.AddComponent<RawImage>();
      ((Graphic) rawImage).set_material(new Material(Shader.Find("Unlit/Texture")));
      RectTransform transform = ((Component) rawImage).get_transform() as RectTransform;
      transform.set_anchoredPosition(Vector2.get_zero());
      transform.set_sizeDelta(new Vector2(_width, _height));
      return rawImage;
    }

    public enum eTargetType
    {
      MAIN_CAMERA,
      SELECTED,
    }
  }
}

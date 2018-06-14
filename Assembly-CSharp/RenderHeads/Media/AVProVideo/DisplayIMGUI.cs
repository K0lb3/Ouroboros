// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.DisplayIMGUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
  [AddComponentMenu("AVPro Video/Display IMGUI", 200)]
  [ExecuteInEditMode]
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  public class DisplayIMGUI : MonoBehaviour
  {
    public MediaPlayer _mediaPlayer;
    public bool _displayInEditor;
    public ScaleMode _scaleMode;
    public Color _color;
    public bool _alphaBlend;
    [SerializeField]
    private bool _useDepth;
    public int _depth;
    public bool _fullScreen;
    [Range(0.0f, 1f)]
    public float _x;
    [Range(0.0f, 1f)]
    public float _y;
    [Range(0.0f, 1f)]
    public float _width;
    [Range(0.0f, 1f)]
    public float _height;
    private static int _propAlphaPack;
    private static int _propVertScale;
    private static int _propApplyGamma;
    private static Shader _shaderAlphaPacking;
    private Material _material;

    public DisplayIMGUI()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (DisplayIMGUI._propAlphaPack != 0)
        return;
      DisplayIMGUI._propAlphaPack = Shader.PropertyToID("AlphaPack");
      DisplayIMGUI._propVertScale = Shader.PropertyToID("_VertScale");
      DisplayIMGUI._propApplyGamma = Shader.PropertyToID("_ApplyGamma");
    }

    private void Start()
    {
      if (!this._useDepth)
        this.set_useGUILayout(false);
      if (!Object.op_Equality((Object) DisplayIMGUI._shaderAlphaPacking, (Object) null))
        return;
      DisplayIMGUI._shaderAlphaPacking = Shader.Find("AVProVideo/IMGUI/Texture Transparent");
      if (!Object.op_Equality((Object) DisplayIMGUI._shaderAlphaPacking, (Object) null))
        return;
      Debug.LogWarning((object) "[AVProVideo] Missing shader AVProVideo/IMGUI/Transparent Packed");
    }

    private void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this._material, (Object) null))
        return;
      Object.Destroy((Object) this._material);
      this._material = (Material) null;
    }

    private Shader GetRequiredShader()
    {
      Shader shader = (Shader) null;
      switch (this._mediaPlayer.m_AlphaPacking)
      {
        case AlphaPacking.TopBottom:
        case AlphaPacking.LeftRight:
          shader = DisplayIMGUI._shaderAlphaPacking;
          break;
      }
      if (Object.op_Equality((Object) shader, (Object) null) && this._mediaPlayer.TextureProducer != null && this._mediaPlayer.TextureProducer.GetTextureCount() == 2)
        shader = DisplayIMGUI._shaderAlphaPacking;
      return shader;
    }

    private void Update()
    {
      if (!Object.op_Inequality((Object) this._mediaPlayer, (Object) null))
        return;
      Shader shader = (Shader) null;
      if (Object.op_Inequality((Object) this._material, (Object) null))
        shader = this._material.get_shader();
      Shader requiredShader = this.GetRequiredShader();
      if (Object.op_Inequality((Object) shader, (Object) requiredShader))
      {
        if (Object.op_Inequality((Object) this._material, (Object) null))
        {
          Object.Destroy((Object) this._material);
          this._material = (Material) null;
        }
        if (Object.op_Inequality((Object) requiredShader, (Object) null))
          this._material = new Material(requiredShader);
      }
      if (!Object.op_Inequality((Object) this._material, (Object) null))
        return;
      if (this._material.HasProperty(DisplayIMGUI._propAlphaPack))
        Helper.SetupAlphaPackedMaterial(this._material, this._mediaPlayer.m_AlphaPacking);
      DisplayIMGUI._propApplyGamma |= 0;
    }

    private void OnGUI()
    {
      if (Object.op_Equality((Object) this._mediaPlayer, (Object) null))
        return;
      bool flag = false;
      Texture texture = (Texture) null;
      if (!this._displayInEditor)
        ;
      if (this._mediaPlayer.Info != null && !this._mediaPlayer.Info.HasVideo())
        texture = (Texture) null;
      if (this._mediaPlayer.TextureProducer != null)
      {
        if (Object.op_Inequality((Object) this._mediaPlayer.TextureProducer.GetTexture(0), (Object) null))
        {
          texture = this._mediaPlayer.TextureProducer.GetTexture(0);
          flag = this._mediaPlayer.TextureProducer.RequiresVerticalFlip();
        }
        if (this._mediaPlayer.TextureProducer.GetTextureCount() == 2 && Object.op_Inequality((Object) this._material, (Object) null))
        {
          this._material.SetTexture("_ChromaTex", this._mediaPlayer.TextureProducer.GetTexture(1));
          this._material.EnableKeyword("USE_YPCBCR");
        }
      }
      if (!Object.op_Inequality((Object) texture, (Object) null) || this._alphaBlend && this._color.a <= 0.0)
        return;
      GUI.set_depth(this._depth);
      GUI.set_color(this._color);
      Rect rect = this.GetRect();
      if (Object.op_Inequality((Object) this._material, (Object) null))
      {
        if (flag)
          this._material.SetFloat(DisplayIMGUI._propVertScale, -1f);
        else
          this._material.SetFloat(DisplayIMGUI._propVertScale, 1f);
        Helper.DrawTexture(rect, texture, this._scaleMode, this._mediaPlayer.m_AlphaPacking, this._material);
      }
      else
      {
        if (flag)
        {
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          GUIUtility.ScaleAroundPivot(new Vector2(1f, -1f), new Vector2(0.0f, ((Rect) @rect).get_y() + ((Rect) @rect).get_height() / 2f));
        }
        GUI.DrawTexture(rect, texture, this._scaleMode, this._alphaBlend);
      }
    }

    public Rect GetRect()
    {
      Rect rect;
      if (this._fullScreen)
      {
        // ISSUE: explicit reference operation
        ((Rect) @rect).\u002Ector(0.0f, 0.0f, (float) Screen.get_width(), (float) Screen.get_height());
      }
      else
      {
        // ISSUE: explicit reference operation
        ((Rect) @rect).\u002Ector(this._x * (float) (Screen.get_width() - 1), this._y * (float) (Screen.get_height() - 1), this._width * (float) Screen.get_width(), this._height * (float) Screen.get_height());
      }
      return rect;
    }
  }
}

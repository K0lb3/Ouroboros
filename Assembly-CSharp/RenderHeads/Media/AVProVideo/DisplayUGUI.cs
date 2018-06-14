// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.DisplayUGUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RenderHeads.Media.AVProVideo
{
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  [AddComponentMenu("AVPro Video/Display uGUI", 200)]
  [ExecuteInEditMode]
  public class DisplayUGUI : MaskableGraphic
  {
    private static List<int> QuadIndices = new List<int>((IEnumerable<int>) new int[6]{ 0, 1, 2, 2, 3, 0 });
    private const string PropChromaTexName = "_ChromaTex";
    [SerializeField]
    public MediaPlayer _mediaPlayer;
    [SerializeField]
    public Rect m_UVRect;
    [SerializeField]
    public bool _setNativeSize;
    [SerializeField]
    public ScaleMode _scaleMode;
    [SerializeField]
    public bool _noDefaultDisplay;
    [SerializeField]
    public bool _displayInEditor;
    [SerializeField]
    public Texture _defaultTexture;
    private int _lastWidth;
    private int _lastHeight;
    private bool _flipY;
    private Texture _lastTexture;
    private static Shader _shaderStereoPacking;
    private static Shader _shaderAlphaPacking;
    private static int _propAlphaPack;
    private static int _propVertScale;
    private static int _propStereo;
    private static int _propApplyGamma;
    private static int _propUseYpCbCr;
    private static int _propChromaTex;
    private bool _userMaterial;
    private Material _material;
    private List<UIVertex> _vertices;

    public DisplayUGUI()
    {
      base.\u002Ector();
    }

    protected virtual void Awake()
    {
      if (DisplayUGUI._propAlphaPack == 0)
      {
        DisplayUGUI._propStereo = Shader.PropertyToID("Stereo");
        DisplayUGUI._propAlphaPack = Shader.PropertyToID("AlphaPack");
        DisplayUGUI._propVertScale = Shader.PropertyToID("_VertScale");
        DisplayUGUI._propApplyGamma = Shader.PropertyToID("_ApplyGamma");
        DisplayUGUI._propUseYpCbCr = Shader.PropertyToID("_UseYpCbCr");
        DisplayUGUI._propChromaTex = Shader.PropertyToID("_ChromaTex");
      }
      if (Object.op_Equality((Object) DisplayUGUI._shaderAlphaPacking, (Object) null))
      {
        DisplayUGUI._shaderAlphaPacking = Shader.Find("AVProVideo/UI/Transparent Packed");
        if (Object.op_Equality((Object) DisplayUGUI._shaderAlphaPacking, (Object) null))
          Debug.LogWarning((object) "[AVProVideo] Missing shader AVProVideo/UI/Transparent Packed");
      }
      if (Object.op_Equality((Object) DisplayUGUI._shaderStereoPacking, (Object) null))
      {
        DisplayUGUI._shaderStereoPacking = Shader.Find("AVProVideo/UI/Stereo");
        if (Object.op_Equality((Object) DisplayUGUI._shaderStereoPacking, (Object) null))
          Debug.LogWarning((object) "[AVProVideo] Missing shader AVProVideo/UI/Stereo");
      }
      ((UIBehaviour) this).Awake();
    }

    protected virtual void Start()
    {
      this._userMaterial = Object.op_Inequality((Object) ((Graphic) this).m_Material, (Object) null);
      ((UIBehaviour) this).Start();
    }

    protected virtual void OnDestroy()
    {
      if (Object.op_Inequality((Object) this._material, (Object) null))
      {
        ((Graphic) this).set_material((Material) null);
        Object.Destroy((Object) this._material);
        this._material = (Material) null;
      }
      ((UIBehaviour) this).OnDestroy();
    }

    private Shader GetRequiredShader()
    {
      Shader shader = (Shader) null;
      switch (this._mediaPlayer.m_StereoPacking)
      {
        case StereoPacking.TopBottom:
        case StereoPacking.LeftRight:
          shader = DisplayUGUI._shaderStereoPacking;
          break;
      }
      switch (this._mediaPlayer.m_AlphaPacking)
      {
        case AlphaPacking.TopBottom:
        case AlphaPacking.LeftRight:
          shader = DisplayUGUI._shaderAlphaPacking;
          break;
      }
      if (Object.op_Equality((Object) shader, (Object) null) && this._mediaPlayer.TextureProducer != null && this._mediaPlayer.TextureProducer.GetTextureCount() == 2)
        shader = DisplayUGUI._shaderAlphaPacking;
      return shader;
    }

    public virtual Texture mainTexture
    {
      get
      {
        Texture texture = (Texture) Texture2D.get_whiteTexture();
        if (this.HasValidTexture())
          texture = this._mediaPlayer.TextureProducer.GetTexture(0);
        else if (this._noDefaultDisplay)
          texture = (Texture) null;
        else if (Object.op_Inequality((Object) this._defaultTexture, (Object) null))
          texture = this._defaultTexture;
        return texture;
      }
    }

    public bool HasValidTexture()
    {
      if (Object.op_Inequality((Object) this._mediaPlayer, (Object) null) && this._mediaPlayer.TextureProducer != null)
        return Object.op_Inequality((Object) this._mediaPlayer.TextureProducer.GetTexture(0), (Object) null);
      return false;
    }

    private void UpdateInternalMaterial()
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
          ((Graphic) this).set_material((Material) null);
          Object.Destroy((Object) this._material);
          this._material = (Material) null;
        }
        if (Object.op_Inequality((Object) requiredShader, (Object) null))
          this._material = new Material(requiredShader);
      }
      ((Graphic) this).set_material(this._material);
    }

    private void LateUpdate()
    {
      if (this._setNativeSize)
        this.SetNativeSize();
      if (Object.op_Inequality((Object) this._lastTexture, (Object) this.mainTexture))
      {
        this._lastTexture = this.mainTexture;
        ((Graphic) this).SetVerticesDirty();
      }
      if (this.HasValidTexture() && Object.op_Inequality((Object) this.mainTexture, (Object) null) && (this.mainTexture.get_width() != this._lastWidth || this.mainTexture.get_height() != this._lastHeight))
      {
        this._lastWidth = this.mainTexture.get_width();
        this._lastHeight = this.mainTexture.get_height();
        ((Graphic) this).SetVerticesDirty();
      }
      if (!this._userMaterial && Application.get_isPlaying())
        this.UpdateInternalMaterial();
      if (Object.op_Inequality((Object) ((Graphic) this).get_material(), (Object) null) && Object.op_Inequality((Object) this._mediaPlayer, (Object) null))
      {
        if (((Graphic) this).get_material().HasProperty(DisplayUGUI._propUseYpCbCr) && this._mediaPlayer.TextureProducer != null && this._mediaPlayer.TextureProducer.GetTextureCount() == 2)
        {
          ((Graphic) this).get_material().EnableKeyword("USE_YPCBCR");
          ((Graphic) this).get_material().SetTexture(DisplayUGUI._propChromaTex, this._mediaPlayer.TextureProducer.GetTexture(1));
        }
        if (((Graphic) this).get_material().HasProperty(DisplayUGUI._propAlphaPack))
        {
          Helper.SetupAlphaPackedMaterial(((Graphic) this).get_material(), this._mediaPlayer.m_AlphaPacking);
          if (this._flipY && this._mediaPlayer.m_AlphaPacking != AlphaPacking.None)
            ((Graphic) this).get_material().SetFloat(DisplayUGUI._propVertScale, -1f);
          else
            ((Graphic) this).get_material().SetFloat(DisplayUGUI._propVertScale, 1f);
        }
        if (((Graphic) this).get_material().HasProperty(DisplayUGUI._propStereo))
          Helper.SetupStereoMaterial(((Graphic) this).get_material(), this._mediaPlayer.m_StereoPacking, this._mediaPlayer.m_DisplayDebugStereoColorTint);
        DisplayUGUI._propApplyGamma |= 0;
      }
      ((Graphic) this).SetMaterialDirty();
    }

    public MediaPlayer CurrentMediaPlayer
    {
      get
      {
        return this._mediaPlayer;
      }
      set
      {
        if (!Object.op_Inequality((Object) this._mediaPlayer, (Object) value))
          return;
        this._mediaPlayer = value;
        ((Graphic) this).SetMaterialDirty();
      }
    }

    public Rect uvRect
    {
      get
      {
        return this.m_UVRect;
      }
      set
      {
        if (Rect.op_Equality(this.m_UVRect, value))
          return;
        this.m_UVRect = value;
        ((Graphic) this).SetVerticesDirty();
      }
    }

    [ContextMenu("Set Native Size")]
    public virtual void SetNativeSize()
    {
      Texture mainTexture = this.mainTexture;
      if (!Object.op_Inequality((Object) mainTexture, (Object) null))
        return;
      double width1 = (double) mainTexture.get_width();
      Rect uvRect1 = this.uvRect;
      // ISSUE: explicit reference operation
      double width2 = (double) ((Rect) @uvRect1).get_width();
      int num1 = Mathf.RoundToInt((float) (width1 * width2));
      double height1 = (double) mainTexture.get_height();
      Rect uvRect2 = this.uvRect;
      // ISSUE: explicit reference operation
      double height2 = (double) ((Rect) @uvRect2).get_height();
      int num2 = Mathf.RoundToInt((float) (height1 * height2));
      if (Object.op_Inequality((Object) this._mediaPlayer, (Object) null))
      {
        if (this._mediaPlayer.m_AlphaPacking == AlphaPacking.LeftRight || this._mediaPlayer.m_StereoPacking == StereoPacking.LeftRight)
          num1 /= 2;
        else if (this._mediaPlayer.m_AlphaPacking == AlphaPacking.TopBottom || this._mediaPlayer.m_StereoPacking == StereoPacking.TopBottom)
          num2 /= 2;
      }
      ((Graphic) this).get_rectTransform().set_anchorMax(((Graphic) this).get_rectTransform().get_anchorMin());
      ((Graphic) this).get_rectTransform().set_sizeDelta(new Vector2((float) num1, (float) num2));
    }

    protected virtual void OnPopulateMesh(VertexHelper vh)
    {
      vh.Clear();
      this._OnFillVBO(this._vertices);
      vh.AddUIVertexStream(this._vertices, DisplayUGUI.QuadIndices);
    }

    [Obsolete("This method is not called from Unity 5.2 and above")]
    protected virtual void OnFillVBO(List<UIVertex> vbo)
    {
      this._OnFillVBO(vbo);
    }

    private void _OnFillVBO(List<UIVertex> vbo)
    {
      this._flipY = false;
      if (this.HasValidTexture())
        this._flipY = this._mediaPlayer.TextureProducer.RequiresVerticalFlip();
      Rect uvRect = this.m_UVRect;
      Vector4 drawingDimensions = this.GetDrawingDimensions(this._scaleMode, ref uvRect);
      vbo.Clear();
      UIVertex simpleVert = (UIVertex) UIVertex.simpleVert;
      simpleVert.color = (__Null) Color32.op_Implicit(((Graphic) this).get_color());
      simpleVert.position = (__Null) Vector2.op_Implicit(new Vector2((float) drawingDimensions.x, (float) drawingDimensions.y));
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      simpleVert.uv0 = (__Null) new Vector2(((Rect) @uvRect).get_xMin(), ((Rect) @uvRect).get_yMin());
      if (this._flipY)
      {
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        simpleVert.uv0 = (__Null) new Vector2(((Rect) @uvRect).get_xMin(), 1f - ((Rect) @uvRect).get_yMin());
      }
      vbo.Add(simpleVert);
      simpleVert.position = (__Null) Vector2.op_Implicit(new Vector2((float) drawingDimensions.x, (float) drawingDimensions.w));
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      simpleVert.uv0 = (__Null) new Vector2(((Rect) @uvRect).get_xMin(), ((Rect) @uvRect).get_yMax());
      if (this._flipY)
      {
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        simpleVert.uv0 = (__Null) new Vector2(((Rect) @uvRect).get_xMin(), 1f - ((Rect) @uvRect).get_yMax());
      }
      vbo.Add(simpleVert);
      simpleVert.position = (__Null) Vector2.op_Implicit(new Vector2((float) drawingDimensions.z, (float) drawingDimensions.w));
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      simpleVert.uv0 = (__Null) new Vector2(((Rect) @uvRect).get_xMax(), ((Rect) @uvRect).get_yMax());
      if (this._flipY)
      {
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        simpleVert.uv0 = (__Null) new Vector2(((Rect) @uvRect).get_xMax(), 1f - ((Rect) @uvRect).get_yMax());
      }
      vbo.Add(simpleVert);
      simpleVert.position = (__Null) Vector2.op_Implicit(new Vector2((float) drawingDimensions.z, (float) drawingDimensions.y));
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      simpleVert.uv0 = (__Null) new Vector2(((Rect) @uvRect).get_xMax(), ((Rect) @uvRect).get_yMin());
      if (this._flipY)
      {
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        simpleVert.uv0 = (__Null) new Vector2(((Rect) @uvRect).get_xMax(), 1f - ((Rect) @uvRect).get_yMin());
      }
      vbo.Add(simpleVert);
    }

    private Vector4 GetDrawingDimensions(ScaleMode scaleMode, ref Rect uvRect)
    {
      Vector4 zero1 = Vector4.get_zero();
      if (Object.op_Inequality((Object) this.mainTexture, (Object) null))
      {
        Vector4 zero2 = Vector4.get_zero();
        Vector2 vector2;
        // ISSUE: explicit reference operation
        ((Vector2) @vector2).\u002Ector((float) this.mainTexture.get_width(), (float) this.mainTexture.get_height());
        if (Object.op_Inequality((Object) this._mediaPlayer, (Object) null))
        {
          if (this._mediaPlayer.m_AlphaPacking == AlphaPacking.LeftRight || this._mediaPlayer.m_StereoPacking == StereoPacking.LeftRight)
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Vector2& local = @vector2;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local).x = (__Null) ((^local).x / 2.0);
          }
          else if (this._mediaPlayer.m_AlphaPacking == AlphaPacking.TopBottom || this._mediaPlayer.m_StereoPacking == StereoPacking.TopBottom)
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Vector2& local = @vector2;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local).y = (__Null) ((^local).y / 2.0);
          }
        }
        Rect pixelAdjustedRect = ((Graphic) this).GetPixelAdjustedRect();
        int num1 = Mathf.RoundToInt((float) vector2.x);
        int num2 = Mathf.RoundToInt((float) vector2.y);
        Vector4 vector4;
        // ISSUE: explicit reference operation
        ((Vector4) @vector4).\u002Ector((float) zero2.x / (float) num1, (float) zero2.y / (float) num2, ((float) num1 - (float) zero2.z) / (float) num1, ((float) num2 - (float) zero2.w) / (float) num2);
        // ISSUE: explicit reference operation
        if ((double) ((Vector2) @vector2).get_sqrMagnitude() > 0.0)
        {
          if (scaleMode == 2)
          {
            float num3 = (float) (vector2.x / vector2.y);
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            float num4 = ((Rect) @pixelAdjustedRect).get_width() / ((Rect) @pixelAdjustedRect).get_height();
            if ((double) num3 > (double) num4)
            {
              // ISSUE: explicit reference operation
              float height = ((Rect) @pixelAdjustedRect).get_height();
              // ISSUE: explicit reference operation
              // ISSUE: explicit reference operation
              ((Rect) @pixelAdjustedRect).set_height(((Rect) @pixelAdjustedRect).get_width() * (1f / num3));
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              Rect& local = @pixelAdjustedRect;
              // ISSUE: explicit reference operation
              ((Rect) local).set_y(((Rect) local).get_y() + (float) (((double) height - (double) ((Rect) @pixelAdjustedRect).get_height()) * ((Graphic) this).get_rectTransform().get_pivot().y));
            }
            else
            {
              // ISSUE: explicit reference operation
              float width = ((Rect) @pixelAdjustedRect).get_width();
              // ISSUE: explicit reference operation
              // ISSUE: explicit reference operation
              ((Rect) @pixelAdjustedRect).set_width(((Rect) @pixelAdjustedRect).get_height() * num3);
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              Rect& local = @pixelAdjustedRect;
              // ISSUE: explicit reference operation
              ((Rect) local).set_x(((Rect) local).get_x() + (float) (((double) width - (double) ((Rect) @pixelAdjustedRect).get_width()) * ((Graphic) this).get_rectTransform().get_pivot().x));
            }
          }
          else if (scaleMode == 1)
          {
            float num3 = (float) (vector2.x / vector2.y);
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            float num4 = ((Rect) @pixelAdjustedRect).get_width() / ((Rect) @pixelAdjustedRect).get_height();
            if ((double) num4 > (double) num3)
            {
              float num5 = num3 / num4;
              // ISSUE: explicit reference operation
              ((Rect) @uvRect).\u002Ector(0.0f, (float) ((1.0 - (double) num5) * 0.5), 1f, num5);
            }
            else
            {
              float num5 = num4 / num3;
              // ISSUE: explicit reference operation
              ((Rect) @uvRect).\u002Ector((float) (0.5 - (double) num5 * 0.5), 0.0f, num5, 1f);
            }
          }
        }
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ((Vector4) @zero1).\u002Ector(((Rect) @pixelAdjustedRect).get_x() + ((Rect) @pixelAdjustedRect).get_width() * (float) vector4.x, ((Rect) @pixelAdjustedRect).get_y() + ((Rect) @pixelAdjustedRect).get_height() * (float) vector4.y, ((Rect) @pixelAdjustedRect).get_x() + ((Rect) @pixelAdjustedRect).get_width() * (float) vector4.z, ((Rect) @pixelAdjustedRect).get_y() + ((Rect) @pixelAdjustedRect).get_height() * (float) vector4.w);
      }
      return zero1;
    }
  }
}

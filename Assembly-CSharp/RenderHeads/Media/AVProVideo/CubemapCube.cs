// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.CubemapCube
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
  [RequireComponent(typeof (MeshRenderer))]
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  [AddComponentMenu("AVPro Video/Cubemap Cube (VR)", 400)]
  [RequireComponent(typeof (MeshFilter))]
  public class CubemapCube : MonoBehaviour
  {
    private const string PropChromaTexName = "_ChromaTex";
    private Mesh _mesh;
    protected MeshRenderer _renderer;
    [SerializeField]
    protected Material _material;
    [SerializeField]
    private MediaPlayer _mediaPlayer;
    [SerializeField]
    private float expansion_coeff;
    private Texture _texture;
    private bool _verticalFlip;
    private int _textureWidth;
    private int _textureHeight;
    private static int _propApplyGamma;
    private static int _propUseYpCbCr;
    private static int _propChromaTex;

    public CubemapCube()
    {
      base.\u002Ector();
    }

    public MediaPlayer Player
    {
      set
      {
        this._mediaPlayer = value;
      }
      get
      {
        return this._mediaPlayer;
      }
    }

    private void Awake()
    {
      if (CubemapCube._propApplyGamma == 0)
        CubemapCube._propApplyGamma = Shader.PropertyToID("_ApplyGamma");
      if (CubemapCube._propUseYpCbCr == 0)
        CubemapCube._propUseYpCbCr = Shader.PropertyToID("_UseYpCbCr");
      if (CubemapCube._propChromaTex != 0)
        return;
      CubemapCube._propChromaTex = Shader.PropertyToID("_ChromaTex");
    }

    private void Start()
    {
      if (!Object.op_Equality((Object) this._mesh, (Object) null))
        return;
      this._mesh = new Mesh();
      this._mesh.MarkDynamic();
      MeshFilter component = (MeshFilter) ((Component) this).GetComponent<MeshFilter>();
      if (Object.op_Inequality((Object) component, (Object) null))
        component.set_mesh(this._mesh);
      this._renderer = (MeshRenderer) ((Component) this).GetComponent<MeshRenderer>();
      if (Object.op_Inequality((Object) this._renderer, (Object) null))
        ((Renderer) this._renderer).set_material(this._material);
      this.BuildMesh();
    }

    private void OnDestroy()
    {
      if (Object.op_Inequality((Object) this._mesh, (Object) null))
      {
        MeshFilter component = (MeshFilter) ((Component) this).GetComponent<MeshFilter>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.set_mesh((Mesh) null);
        Object.Destroy((Object) this._mesh);
        this._mesh = (Mesh) null;
      }
      if (!Object.op_Inequality((Object) this._renderer, (Object) null))
        return;
      ((Renderer) this._renderer).set_material((Material) null);
      this._renderer = (MeshRenderer) null;
    }

    private void LateUpdate()
    {
      if (!Application.get_isPlaying())
        return;
      if (Object.op_Inequality((Object) this._mediaPlayer, (Object) null) && this._mediaPlayer.Control != null)
      {
        if (this._mediaPlayer.TextureProducer != null)
        {
          Texture texture = this._mediaPlayer.TextureProducer.GetTexture(0);
          bool flipY = this._mediaPlayer.TextureProducer.RequiresVerticalFlip();
          if (Object.op_Inequality((Object) this._texture, (Object) texture) || this._verticalFlip != flipY || Object.op_Inequality((Object) texture, (Object) null) && (this._textureWidth != texture.get_width() || this._textureHeight != texture.get_height()))
          {
            this._texture = texture;
            if (Object.op_Inequality((Object) texture, (Object) null))
              this.UpdateMeshUV(texture.get_width(), texture.get_height(), flipY);
          }
          if (((Renderer) this._renderer).get_material().HasProperty(CubemapCube._propUseYpCbCr) && this._mediaPlayer.TextureProducer.GetTextureCount() == 2)
          {
            ((Renderer) this._renderer).get_material().EnableKeyword("USE_YPCBCR");
            ((Renderer) this._renderer).get_material().SetTexture(CubemapCube._propChromaTex, this._mediaPlayer.TextureProducer.GetTexture(1));
          }
        }
        ((Renderer) this._renderer).get_material().set_mainTexture(this._texture);
      }
      else
        ((Renderer) this._renderer).get_material().set_mainTexture((Texture) null);
    }

    private void BuildMesh()
    {
      Vector3 vector3;
      // ISSUE: explicit reference operation
      ((Vector3) @vector3).\u002Ector(-0.5f, -0.5f, -0.5f);
      Vector3[] vector3Array = new Vector3[24]{ Vector3.op_Subtraction(new Vector3(0.0f, -1f, 0.0f), vector3), Vector3.op_Subtraction(new Vector3(0.0f, 0.0f, 0.0f), vector3), Vector3.op_Subtraction(new Vector3(0.0f, 0.0f, -1f), vector3), Vector3.op_Subtraction(new Vector3(0.0f, -1f, -1f), vector3), Vector3.op_Subtraction(new Vector3(0.0f, 0.0f, 0.0f), vector3), Vector3.op_Subtraction(new Vector3(-1f, 0.0f, 0.0f), vector3), Vector3.op_Subtraction(new Vector3(-1f, 0.0f, -1f), vector3), Vector3.op_Subtraction(new Vector3(0.0f, 0.0f, -1f), vector3), Vector3.op_Subtraction(new Vector3(-1f, 0.0f, 0.0f), vector3), Vector3.op_Subtraction(new Vector3(-1f, -1f, 0.0f), vector3), Vector3.op_Subtraction(new Vector3(-1f, -1f, -1f), vector3), Vector3.op_Subtraction(new Vector3(-1f, 0.0f, -1f), vector3), Vector3.op_Subtraction(new Vector3(-1f, -1f, 0.0f), vector3), Vector3.op_Subtraction(new Vector3(0.0f, -1f, 0.0f), vector3), Vector3.op_Subtraction(new Vector3(0.0f, -1f, -1f), vector3), Vector3.op_Subtraction(new Vector3(-1f, -1f, -1f), vector3), Vector3.op_Subtraction(new Vector3(0.0f, -1f, -1f), vector3), Vector3.op_Subtraction(new Vector3(0.0f, 0.0f, -1f), vector3), Vector3.op_Subtraction(new Vector3(-1f, 0.0f, -1f), vector3), Vector3.op_Subtraction(new Vector3(-1f, -1f, -1f), vector3), Vector3.op_Subtraction(new Vector3(-1f, -1f, 0.0f), vector3), Vector3.op_Subtraction(new Vector3(-1f, 0.0f, 0.0f), vector3), Vector3.op_Subtraction(new Vector3(0.0f, 0.0f, 0.0f), vector3), Vector3.op_Subtraction(new Vector3(0.0f, -1f, 0.0f), vector3) };
      Matrix4x4 matrix4x4 = Matrix4x4.TRS(Vector3.get_zero(), Quaternion.AngleAxis(-90f, Vector3.get_right()), Vector3.get_one());
      for (int index = 0; index < vector3Array.Length; ++index)
      {
        // ISSUE: explicit reference operation
        vector3Array[index] = ((Matrix4x4) @matrix4x4).MultiplyPoint(vector3Array[index]);
      }
      this._mesh.set_vertices(vector3Array);
      this._mesh.set_triangles(new int[36]
      {
        0,
        1,
        2,
        0,
        2,
        3,
        4,
        5,
        6,
        4,
        6,
        7,
        8,
        9,
        10,
        8,
        10,
        11,
        12,
        13,
        14,
        12,
        14,
        15,
        16,
        17,
        18,
        16,
        18,
        19,
        20,
        21,
        22,
        20,
        22,
        23
      });
      this._mesh.set_normals(new Vector3[24]
      {
        new Vector3(-1f, 0.0f, 0.0f),
        new Vector3(-1f, 0.0f, 0.0f),
        new Vector3(-1f, 0.0f, 0.0f),
        new Vector3(-1f, 0.0f, 0.0f),
        new Vector3(0.0f, -1f, 0.0f),
        new Vector3(0.0f, -1f, 0.0f),
        new Vector3(0.0f, -1f, 0.0f),
        new Vector3(0.0f, -1f, 0.0f),
        new Vector3(1f, 0.0f, 0.0f),
        new Vector3(1f, 0.0f, 0.0f),
        new Vector3(1f, 0.0f, 0.0f),
        new Vector3(1f, 0.0f, 0.0f),
        new Vector3(0.0f, 1f, 0.0f),
        new Vector3(0.0f, 1f, 0.0f),
        new Vector3(0.0f, 1f, 0.0f),
        new Vector3(0.0f, 1f, 0.0f),
        new Vector3(0.0f, 0.0f, 1f),
        new Vector3(0.0f, 0.0f, 1f),
        new Vector3(0.0f, 0.0f, 1f),
        new Vector3(0.0f, 0.0f, 1f),
        new Vector3(0.0f, 0.0f, -1f),
        new Vector3(0.0f, 0.0f, -1f),
        new Vector3(0.0f, 0.0f, -1f),
        new Vector3(0.0f, 0.0f, -1f)
      });
      this.UpdateMeshUV(512, 512, false);
    }

    private void UpdateMeshUV(int textureWidth, int textureHeight, bool flipY)
    {
      this._textureWidth = textureWidth;
      this._textureHeight = textureHeight;
      this._verticalFlip = flipY;
      float num1 = (float) textureWidth;
      float num2 = (float) textureHeight;
      float num3 = num1 / 3f;
      float num4 = Mathf.Floor((float) (((double) this.expansion_coeff * (double) num3 - (double) num3) / 2.0));
      float num5 = num4 / num1;
      float num6 = num4 / num2;
      Vector2[] vector2Array = new Vector2[24]{ new Vector2(0.3333333f + num5, 1f - num6), new Vector2(0.6666667f - num5, 1f - num6), new Vector2(0.6666667f - num5, 0.5f + num6), new Vector2(0.3333333f + num5, 0.5f + num6), new Vector2(0.3333333f + num5, 0.5f - num6), new Vector2(0.6666667f - num5, 0.5f - num6), new Vector2(0.6666667f - num5, 0.0f + num6), new Vector2(0.3333333f + num5, 0.0f + num6), new Vector2(0.0f + num5, 1f - num6), new Vector2(0.3333333f - num5, 1f - num6), new Vector2(0.3333333f - num5, 0.5f + num6), new Vector2(0.0f + num5, 0.5f + num6), new Vector2(0.6666667f + num5, 0.5f - num6), new Vector2(1f - num5, 0.5f - num6), new Vector2(1f - num5, 0.0f + num6), new Vector2(0.6666667f + num5, 0.0f + num6), new Vector2(0.0f + num5, 0.0f + num6), new Vector2(0.0f + num5, 0.5f - num6), new Vector2(0.3333333f - num5, 0.5f - num6), new Vector2(0.3333333f - num5, 0.0f + num6), new Vector2(1f - num5, 1f - num6), new Vector2(1f - num5, 0.5f + num6), new Vector2(0.6666667f + num5, 0.5f + num6), new Vector2(0.6666667f + num5, 1f - num6) };
      if (flipY)
      {
        for (int index = 0; index < vector2Array.Length; ++index)
          vector2Array[index].y = (__Null) (1.0 - vector2Array[index].y);
      }
      this._mesh.set_uv(vector2Array);
      this._mesh.UploadMeshData(false);
    }
  }
}

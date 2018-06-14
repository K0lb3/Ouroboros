// Decompiled with JetBrains decompiler
// Type: MoveByKeys
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Photon;
using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class MoveByKeys : MonoBehaviour
{
  public float Speed = 10f;
  public float JumpForce = 200f;
  public float JumpTimeout = 0.5f;
  private bool isSprite;
  private float jumpingTime;
  private Rigidbody body;
  private Rigidbody2D body2d;

  public void Start()
  {
    this.isSprite = Object.op_Inequality((Object) ((Component) this).GetComponent<SpriteRenderer>(), (Object) null);
    this.body2d = (Rigidbody2D) ((Component) this).GetComponent<Rigidbody2D>();
    this.body = (Rigidbody) ((Component) this).GetComponent<Rigidbody>();
  }

  public void FixedUpdate()
  {
    if (!this.photonView.isMine)
      return;
    if ((double) Input.GetAxisRaw("Horizontal") < -0.100000001490116 || (double) Input.GetAxisRaw("Horizontal") > 0.100000001490116)
    {
      Transform transform = ((Component) this).get_transform();
      transform.set_position(Vector3.op_Addition(transform.get_position(), Vector3.op_Multiply(Vector3.op_Multiply(Vector3.get_right(), this.Speed * Time.get_deltaTime()), Input.GetAxisRaw("Horizontal"))));
    }
    if ((double) this.jumpingTime <= 0.0)
    {
      if ((Object.op_Inequality((Object) this.body, (Object) null) || Object.op_Inequality((Object) this.body2d, (Object) null)) && Input.GetKey((KeyCode) 32))
      {
        this.jumpingTime = this.JumpTimeout;
        Vector2 vector2 = Vector2.op_Multiply(Vector2.get_up(), this.JumpForce);
        if (Object.op_Inequality((Object) this.body2d, (Object) null))
          this.body2d.AddForce(vector2);
        else if (Object.op_Inequality((Object) this.body, (Object) null))
          this.body.AddForce(Vector2.op_Implicit(vector2));
      }
    }
    else
      this.jumpingTime -= Time.get_deltaTime();
    if (this.isSprite || (double) Input.GetAxisRaw("Vertical") >= -0.100000001490116 && (double) Input.GetAxisRaw("Vertical") <= 0.100000001490116)
      return;
    Transform transform1 = ((Component) this).get_transform();
    transform1.set_position(Vector3.op_Addition(transform1.get_position(), Vector3.op_Multiply(Vector3.op_Multiply(Vector3.get_forward(), this.Speed * Time.get_deltaTime()), Input.GetAxisRaw("Vertical"))));
  }
}

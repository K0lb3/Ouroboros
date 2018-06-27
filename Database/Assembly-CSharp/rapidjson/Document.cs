// Decompiled with JetBrains decompiler
// Type: rapidjson.Document
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace rapidjson
{
  public class Document : IDisposable
  {
    private IntPtr ptr;
    private bool disposed;
    public readonly Value Root;

    private Document(IntPtr ptr)
    {
      this.ptr = ptr;
      this.Root = new Value(this, ref ptr);
    }

    public static Document Parse(byte[] bytes)
    {
      IntPtr document;
      if (!DLL._rapidjson_new_document_from_memory_bytes(bytes, (uint) bytes.Length, out document))
        throw new DocumentParseError();
      return new Document(document);
    }

    public static Document Parse(string text)
    {
      IntPtr document;
      if (!DLL._rapidjson_new_document_from_memory_string(text, out document))
        throw new DocumentParseError();
      return new Document(document);
    }

    public static Document ParseFromFile(string filepath)
    {
      IntPtr document;
      if (!DLL._rapidjson_new_document_from_file(filepath, out document))
        throw new DocumentParseError();
      return new Document(document);
    }

    ~Document()
    {
      this.Dispose();
    }

    public void Dispose()
    {
      if (!(this.ptr != IntPtr.Zero))
        return;
      this.disposed = true;
      DLL._rapidjson_delete_document(out this.ptr);
    }

    public void CheckDisposed()
    {
      if (this.disposed)
        throw new AlreadyDisposedDocumentError();
    }
  }
}

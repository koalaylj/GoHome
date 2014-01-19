using UnityEngine;
using System;
using System.Collections;

public enum CanvasOperationType
{
    Draw,
    Erase
}

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class Canvas : MonoBehaviour
{
    public Texture2D penTexture;
    private Texture2D _drawingBoard;
    private Color[] penPixels;
    private int penSizeX;
    private int penSizeY;
    private int penOffsetX;
    private int penOffsetY;
    private CanvasOperationType operation = CanvasOperationType.Draw;

    public Texture2D DrawingBoard
    {
        get { return _drawingBoard; }
        private set { _drawingBoard = value; }
    }

    void Awake()
    {
        CreateMesh();

        _drawingBoard = renderer.material.mainTexture as Texture2D;

        penPixels = penTexture.GetPixels();
        penSizeX = Convert.ToInt32(penTexture.width);
        penSizeY = Convert.ToInt32(penTexture.height);
        penOffsetX = Convert.ToInt32(-0.5f * penSizeX);
        penOffsetY = Convert.ToInt32(-0.5f * penSizeY);

        //Clean();

    }

    //    public Vector2 Size {
    //        get {
    //            //return GetComponent<MeshFilter>().mesh.bounds.size;
    //            return gameObject.GetComponent<MeshCollider> ().bounds.size;
    //        }
    //    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
            operation = CanvasOperationType.Erase;
        }
        else if (Input.GetKeyUp(KeyCode.Y))
        {
            operation = CanvasOperationType.Draw;
        }
    }

    void CreateMesh()
    {
        Vector3 lt = new Vector3(-Screen.width * 0.5f, Screen.height * 0.5f);
        Vector3 rt = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f);
        Vector3 lb = new Vector3(-Screen.width * 0.5f, -Screen.height * 0.5f);
        Vector3 rb = new Vector3(Screen.width * 0.5f, -Screen.height * 0.5f);

        Vector3[] verts = new Vector3[] { lt, rt, lb, rb };

        Vector2[] uv = new Vector2[] {
                new Vector2(0.0f, 1.0f),
                new Vector2(1.0f, 1.0f),
                new Vector2(0.0f, 0.0f),
                new Vector2(1.0f, 0.0f)
            };

        int[] triangles = new int[] {
            0, 1, 2, 1, 3, 2
        };

        Mesh mesh = new Mesh();
        mesh.vertices = verts;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        MeshFilter filter = GetComponent<MeshFilter>();
        filter.mesh = mesh;
        MeshCollider col = gameObject.AddComponent<MeshCollider>();
        col.sharedMesh = mesh;
        col.isTrigger = true;
    }

    void DrawPath(CanvasOperationType op)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        // Debug.DrawRay(ray.origin, ray.direction);

        //Debug.DrawLine(ray.origin, new Vector3(ray.origin.x, ray.origin.y, ray.origin.z + 100), Color.green);

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        //RaycastHit2D hit2 = Physics2D.GetRayIntersection (ray, 1000);

        if (hit.transform != null)
        {
            Debug.Log("hit:" + hit.transform.gameObject.name);
        }

        if (collider.Raycast(ray, out hitInfo, 100))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red);

            if (!Physics.Raycast(ray, hitInfo.distance - 0.01f))
            {
                Vector2 texCoord = hitInfo.textureCoord;
                if (hitInfo.textureCoord.x >= 0 && hitInfo.textureCoord.x <= 1.0f
                    && hitInfo.textureCoord.y >= 0 && hitInfo.textureCoord.y <= 1.0f)
                {
                    int drawPosX = Convert.ToInt32(hitInfo.textureCoord.x * _drawingBoard.width + penOffsetX);
                    int drawPosY = Convert.ToInt32(hitInfo.textureCoord.y * _drawingBoard.height + penOffsetY);
                    /*
                    _canvas.SetPixels( drawPosX, drawPosY, penSizeX, penSizeY, penPixels );
                    _canvas.Apply();
                    */
                    for (int i = 0; i < penSizeX; i++)
                    {
                        for (int k = 0; k < penSizeY; k++)
                        {
                            Color sc = _drawingBoard.GetPixel(drawPosX + i, drawPosY + k);
                            Color dc = penTexture.GetPixel(i, k);
                            if (op == CanvasOperationType.Draw)
                            {
                                if (sc.a < 0.9f && dc.a > 0.9f)
                                {
                                    _drawingBoard.SetPixel(drawPosX + i, drawPosY + k, dc);
                                }
                            }
                            else
                            {
                                if (sc.a > 0.9f && dc.a > 0.9f)
                                {
                                    _drawingBoard.SetPixel(drawPosX + i, drawPosY + k, Color.clear);
                                }
                            }
                        }
                    }
                    _drawingBoard.Apply();
                }
            }
        }
    }

    void Clean()
    {
        for (int i = 0; i < _drawingBoard.width; i++)
        {
            for (int k = 0; k < _drawingBoard.height; k++)
            {
                _drawingBoard.SetPixel(i, k, Color.clear);

            }
        }

        _drawingBoard.Apply();
    }

    void OnMouseDown()
    {
        DrawPath(operation);
    }

    void OnMouseDrag()
    {
        DrawPath(operation);
    }

    void OnDestroy()
    {
        Resources.UnloadUnusedAssets();
    }
}
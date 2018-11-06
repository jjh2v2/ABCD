using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal static class MakeFxMash
{
    public static Mesh MakeMesh()
    {
        List<Vector3> _Vertex = new List<Vector3>();
        List<int> _Tri = new List<int>();
        List<Vector2> _UV = new List<Vector2>();
        Mesh mesh = new Mesh(); ;

        //Cell간의 Gap입니다. 5by5짜리 텍스쳐니까 간단히 1/5 = 0.2!
        float _cellGap = 0.2f;
        //cell들의 Index를 저장합니다.
        List<Vector2> _cell = new List<Vector2>();

        //mesh = GetComponent<MeshFilter>().mesh;

        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;


        _Vertex.Add(new Vector3(x, y, z));
        _Vertex.Add(new Vector3(x + 1, y, z));
        _Vertex.Add(new Vector3(x + 1, y - 1, z));
        _Vertex.Add(new Vector3(x, y - 1, z));

        _Tri.Add(0);
        _Tri.Add(1);
        _Tri.Add(3);
        _Tri.Add(1);
        _Tri.Add(2);
        _Tri.Add(3);

        //셀은 2개만 사용할겁니다. 0,0에 있는 검은색과 0,1에 있는 흰색입니다.
        _cell.Add(new Vector2(0, 0));
        _cell.Add(new Vector2(0, 1));

        //텍스쳐의 좌표를 UV에 넣습니다.
        //만약 List 인덱스 0번이라면 (0,0), (0.2,0), (0, 0.2), (0.2, 0.2)의 값을 가지겠죠?
        _UV.Add(new Vector2(_cellGap * _cell[1].x, _cellGap * _cell[1].y + _cellGap));
        _UV.Add(new Vector2(_cellGap * _cell[1].x + _cellGap, _cellGap * _cell[1].y + _cellGap));
        _UV.Add(new Vector2(_cellGap * _cell[1].x + _cellGap, _cellGap * _cell[1].y));
        _UV.Add(new Vector2(_cellGap * _cell[1].x, _cellGap * _cell[1].y));

        mesh.Clear();
        mesh.vertices = _Vertex.ToArray();
        mesh.triangles = _Tri.ToArray();
        //UV값을 넣습니다.
        mesh.uv = _UV.ToArray();
        //mesh.Optimize();
        mesh.RecalculateNormals();

        return mesh;
    }




    //public static void MakeFxObject(GameObject HitBg)
    //{
    //    Mesh result = new Mesh();

    //    // 정점
    //    int Length = 0;
    //    Vector3[] newVertices = new Vector3[Length];
    //    for (int i = 0; i < Length; i++)
    //    {
    //        //newVertices[i] = (Vector3)flatVertices[i];
    //    }
    //    result.vertices = newVertices;

    //    // 삼각형
    //    result.triangles = GenerateConvexPolygonTrianglesFromVertices(newVertices);

    //    // UV
    //    Mesh original = HitBg.GetComponent<Mesh>();
    //    result.uv = GenerateProportionalUVs(newVertices, original);

    //    result.RecalculateNormals();

    //    //return result;
    //}

    //static int[] GenerateConvexPolygonTrianglesFromVertices(Vector3[] vertices)
    //{
    //    if (vertices.Length == 3)
    //    {
    //        return new int[] { 0, 1, 2 };
    //    }

    //    List<int> result = new List<int>();
    //    for (int i = 2; i < vertices.Length; i++)
    //    {
    //        result.Add(0);
    //        result.Add(i - 1);
    //        result.Add(i);
    //    }

    //    return result.ToArray();
    //}

    //static Vector2[] GenerateProportionalUVs(Vector3[] vertices, Mesh original)
    //{
    //    Vector2[] result = new Vector2[vertices.Length];

    //    int vertexIndexToCalculateDiff = 0;
    //    for (int i = 1; i < original.vertexCount; i++)
    //    {
    //        if (original.vertices[0].x != original.vertices[i].x &&
    //             original.vertices[0].y != original.vertices[i].y)
    //        {
    //            vertexIndexToCalculateDiff = i;
    //            break;
    //        }
    //    }
    //    if (vertexIndexToCalculateDiff == 0)
    //    {
    //        throw new System.Exception("Couldn't find vertexes with different x and y coordinates!");
    //    }

    //    Vector3 twoFirstVerticesDiff = original.vertices[vertexIndexToCalculateDiff] - original.vertices[0];
    //    Vector2 twoFirstUVsDiff = original.uv[vertexIndexToCalculateDiff] - original.uv[0];
    //    Vector2 distanceToUVMap = new Vector2();
    //    distanceToUVMap.x = twoFirstUVsDiff.x / twoFirstVerticesDiff.x;
    //    distanceToUVMap.y = twoFirstUVsDiff.y / twoFirstVerticesDiff.y;

    //    for (int i = 0; i < vertices.Length; i++)
    //    {
    //        result[i] = (vertices[i] - original.vertices[0]);
    //        result[i] = new Vector2(result[i].x * distanceToUVMap.x,
    //                                   result[i].y * distanceToUVMap.y);
    //        result[i] += original.uv[0];
    //    }

    //    return result;
    //}
}
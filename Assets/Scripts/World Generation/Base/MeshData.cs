using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshData
{
    public List<Vector3> vertices = new List<Vector3>();
    public List<int> triangles = new List<int>();
    public List<Vector3> colVertices = new List<Vector3>();
    public List<int> colTriangles = new List<int>();

    public List<Vector2> uv = new List<Vector2>();

    public bool hasColision;

    /**
     * Constructorul clasei.
     */
    public MeshData()
    {

    }

    /**
     * Functie ajutatoare ce adauga triunghiurile necesare la ultima fata adaugata in mesh.
     * Daca coliziunea este true pentru mesh-ul curent, adaugam triunghiurile pentru coliziune.
     */
    public void AddQuadTriangles()
    {
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 2);

        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 1);

        if (hasColision)
        {
            colTriangles.Add(colVertices.Count - 4);
            colTriangles.Add(colVertices.Count - 3);
            colTriangles.Add(colVertices.Count - 2);

            colTriangles.Add(colVertices.Count - 4);
            colTriangles.Add(colVertices.Count - 2);
            colTriangles.Add(colVertices.Count - 1);
        }
    }

    /**
     * Adauga varfurile pentru randare si pentru coliziune daca este setata.
     */
    public void AddVertex(Vector3 vertex)
    {
        vertices.Add(vertex);
        if (hasColision)
        {
            colVertices.Add(vertex);
        }
    }

    /**
     * Adauga triunghiruile poligonale pentru randare si pentru coliziune daca este setata.
     */
    public void AddTriangle(int tri)
    {
        triangles.Add(tri);
        if (hasColision)
        {
            colTriangles.Add(tri - (vertices.Count - colVertices.Count));
        }
    }
}

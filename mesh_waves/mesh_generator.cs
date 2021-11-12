using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
public class mesh_generator : MonoBehaviour
{

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    float t = 0;

    public int xsize = 20 ; 
    public int zsize = 20 ;
    public float a = 20 ;
    public float w = 20 ;
    float xsin = 0 ,zsin = 0;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        

    }

    private void Update()
    {
        CreateShape() ;
        UpdateMesh();
    }
    
    void CreateShape(){
        vertices = new Vector3[ (xsize+1) * (zsize+1) ] ;
        t += Time.fixedDeltaTime ;
        xsin = Mathf.Sin( w * t);
        zsin = Mathf.Sin( w * t);
        int i = 0 ;
        for (int z = 0 ; z <= zsize ; z++ ){
            for ( int x = 0 ; x<= xsize; x++ ){
                float y = Mathf.PerlinNoise( x * xsin ,z * zsin ) * a;
                vertices[i] = new Vector3(x,y,z);
                i++;
            }
        }


        int vert = 0 ;
        int tris = 0 ;
        triangles = new int[xsize * zsize * 6];

        for (int z = 0 ; z < zsize ; z++ ){
            for (int x = 0 ; x < xsize ; x++ ){
                
                triangles[tris + 0]= vert + 0 ;
                triangles[tris + 1]= vert + xsize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xsize + 1 ;
                triangles[tris + 5] = vert + xsize + 2 ;
                
                vert++;
                tris += 6;
        
            }
            vert++;
        }
        

    }

    // Update is called once per frame
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
    }

}
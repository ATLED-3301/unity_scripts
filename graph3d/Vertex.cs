using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex : MonoBehaviour
{
    public GameObject[] ConnectedList;
    public GameObject obj;
    public ConnectPointsWithCylinderMesh edges ;
    public Material edgeMat;
    public Mesh edgeMesh;
    public float radius = 0.05f;
    
    void Start()
    {
        obj = this.gameObject;
        ConnectPointsWithCylinderMesh edges =  obj.AddComponent<ConnectPointsWithCylinderMesh>();
        edges.mainPoint = obj;
        edges.points = ConnectedList;
        edges.radius = radius;
        edges.lineMat = edgeMat;
        edges.cylinderMesh = edgeMesh;


    }


    void FixedUpdate()
    {   

    }


}


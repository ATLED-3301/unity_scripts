using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_up : MonoBehaviour
{
    // Start is called before the first frame update
    public float value = 0;
    private Color c;

    public List<GameObject> Objs ;
    private Renderer rend;
    private Material mat;
    public float damp = 0.97f;
    public int depth = 5;
    public GameObject go;

    void Start(){
        go = this.gameObject;
        rend = go.GetComponent<Renderer>();
        rend.material.EnableKeyword("_EMISSION");
        mat = rend.material;
        c = GetComponent<Renderer>().material.color;
    }

    void OnMouseDown()
    {
        value = 5f;// this object was clicked - do something
        SparkNext(value, 0);
    }
    // Update is called once per frame

    public void UpdateList(GameObject go1,GameObject go2,GameObject go3,GameObject go4,GameObject go5,GameObject go6){
        List<GameObject> TmpObjs = new List<GameObject>();
        TmpObjs.Add(go1);
        TmpObjs.Add(go2);
        TmpObjs.Add(go3);
        TmpObjs.Add(go4);
        TmpObjs.Add(go5);
        TmpObjs.Add(go6);
        Objs = TmpObjs;
    }
    public void SparkNext(float v, float d)
    {
        if (d + 1 <= depth){
        for (int i = 0; i < Objs.Count; i++){
            light_up sc = Objs[i].GetComponent<light_up>();
            float nv = v * damp;
            sc.SparkNext(nv, d + 1);
            }
        }
    }


    void FixedUpdate()
    {
        value = value * damp;
        mat.SetColor("_EmissionColor", new Color(c[0], c[1], c[2], 1f) * value);// make the alpha max.

    }
}

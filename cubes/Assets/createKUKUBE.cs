using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createKUKUBE : MonoBehaviour
{
    public GameObject baseObject;
    private  GameObject [,,] cubes ;
    public int N = 9 ;
    public float spacing = 2f ; 
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        int j = 0;
        int k = 0;
        GameObject[,,] tempcubes = new GameObject[N,N,N];
        float delta = N * spacing / 2 ;
        for (i = 0 ; i <N ; i++ ){
            for (j = 0; j < N; j++){
                for (k = 0; k < N; k++){
                GameObject instantiatedObject = Instantiate(baseObject);
                instantiatedObject.transform.parent = this.transform;
                instantiatedObject.transform.position = new Vector3(i * spacing - delta , j * spacing - delta , k * spacing - delta );
                instantiatedObject.AddComponent<light_up>();
                tempcubes[i, j, k] = instantiatedObject;
                }
            }
        }
        cubes = tempcubes;

        for (i = 1; i < N-1; i++)
        {
            for (j = 1; j < N-1; j++)
            {
                for (k = 1; k < N-1; k++)
                {
                    GameObject cube = cubes[i,j,k];
                    light_up scr = cube.GetComponent<light_up>();
                    List<GameObject> lst = new List<GameObject>();
                    GameObject cubeup = cubes[i + 1, j, k];
                    GameObject cubedown = cubes[i - 1, j, k];
                    GameObject cubeleft = cubes[i, j + 1, k];
                    GameObject cuberight = cubes[i, j - 1, k];
                    GameObject cubeforward = cubes[i, j, k + 1];
                    GameObject cubebackward = cubes[i, j, k - 1];
                    scr.UpdateList(cubeup,cubedown,cubeleft,cuberight,cubeforward,cubebackward);
                    

            }
        }
    }
    

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

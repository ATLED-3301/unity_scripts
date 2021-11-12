using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{

    public Rigidbody rb;
    public float G ;
    public static List<Attractor> Attractors;
    void FixedUpdate()
    {
        foreach (Attractor attractor in Attractors)
        {
            if (attractor != this)


                Attract(attractor);
        }

    }
    void OnEnable() {
        if (Attractors == null)
            Attractors = new List<Attractor>();

        Attractors.Add(this);
    }

    void Attract(Attractor objtoattract)
    {

        Rigidbody rbtoattract = objtoattract.rb;
        Vector3 direction = rb.position - rbtoattract.position;
        float distance = direction.magnitude;

        float forceMagnitude = G * (rb.mass * rbtoattract.mass / Mathf.Pow(distance, 2));
        Vector3 force = direction.normalized * forceMagnitude;

        rbtoattract.AddForce(force);

    }

}
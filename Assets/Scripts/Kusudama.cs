using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Kusudama : MonoBehaviour
{
    private int n = 0;
    private Shell shell;
    private List<Particle> particles = new List<Particle>();

    void Awake()
    {
        shell = Shell.New(new Vector3(0, 3.5f, 0));
        for (int i = 0; i < 1000; ++i)
        {
            var pos = shell.transform.position + new Vector3(Normal.Next(0, 0.5f), Normal.Next(0, 0.5f));
            particles.Add(Particle.New(shell.transform.position, UnityEngine.Random.rotation));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (n == 0)
        {
            shell.Open();
            ++n;
        }
        foreach(var p in particles)
        {
            p.Drop(Time.deltaTime);
        }
    }
}

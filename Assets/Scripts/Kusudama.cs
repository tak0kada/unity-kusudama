using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Kusudama : MonoBehaviour
{
    private float timeElapsed = 0;
    private Shell shell;
    private List<Particle> particles = new List<Particle>();


    private void Awake()
    {
        shell = Shell.New(new Vector3(0, 3.5f, 0));
        for (int i = 0; i < 4000; ++i)
        {
            var pos = shell.transform.position + new Vector3(Normal.Next(0, 0.2f), Normal.Next(0, 0.2f));
            particles.Add(Particle.New(pos, UnityEngine.Random.rotation));
        }
    }

    private void Start()
    {
        StartCoroutine(ParticleCoroutine());
    }

    private IEnumerator ParticleCoroutine()
    {
        while (true)
        {
            foreach (var p in particles)
            {
                p.Drop(Time.deltaTime);
            }
            yield return null;
        }
    }

    void Update()
    {
        if (timeElapsed < 0.1f)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > 0.1f)
            {
                shell.Open();
            }
        }
        // foreach(var p in particles)
        // {
        //     p.Drop(Time.deltaTime);
        // }
    }
}

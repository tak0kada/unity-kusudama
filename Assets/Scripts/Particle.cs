using System;
using System.Collections;
using UnityEngine;



class Particle : MonoBehaviour
{
    private const float size = 0.4f;
    private static readonly Color[] colors = new Color[5]{Color.red, Color.blue, Color.yellow, Color.green, Color.gray};

    private float ax;
    private float ay;
    private Quaternion q;

    private GameObject cube;
    new private Renderer renderer;
    private static System.Random rnd = new System.Random();


    private void Awake()
    {
        gameObject.SetActive(false);
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = transform;
        cube.transform.localPosition = Vector3.zero;
        transform.localScale = new Vector3(size, size, 0.01f);
        renderer = cube.GetComponent<Renderer>();
    }

    public static Particle New(Vector3 position, Quaternion q)
    {
        var g = new GameObject("Particle");
        var particle = g.AddComponent<Particle>();
        particle.renderer.material.color = colors[UnityEngine.Random.Range(0, 5)];
        g.transform.position = position;
        particle.q = q;
        particle.transform.rotation = q;

        particle.gameObject.SetActive(true);
        return particle;
    }

    public void Drop(float time)
    {
        float r = 0.2f;
        ax += 1.1f * Normal.Next(0, r);
        ay += Normal.Next(0, r) - 0.1f;
        transform.position += new Vector3(ax * time, ay * time, 0);
        transform.rotation *= Quaternion.Lerp(Quaternion.identity, q, 0.08f);
    }

    public bool OutOfScreen()
    {
        return !renderer.isVisible;
    }
}

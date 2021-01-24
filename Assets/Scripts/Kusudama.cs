using System;
using System.Collections.Generic;
using UnityEngine;


public class Kusudama : MonoBehaviour
{
    private GameObject left;
    private GameObject right;
    private Quaternion lorigin;
    private Quaternion rorigin;
    private bool opening = false;
    private float angle = 0;


    public static Kusudama Create(Vector3 position, float radius = 1.0f)
    {
        var g = new GameObject("Kusudama");
        var k = g.AddComponent<Kusudama>() as Kusudama;
        {
            k.left = new GameObject("LeftHemisphere");
            k.left.transform.parent = g.transform;
            k.left.AddComponent<LeftHemisphere>();
            k.lorigin = k.left.transform.rotation;

            k.right = new GameObject("RightHemisphere");
            k.right.transform.parent = g.transform;
            k.right.AddComponent<LeftHemisphere>();
            k.right.transform.Rotate(0, 180, 0);
            k.rorigin = k.right.transform.rotation;
        }

        g.transform.position = position;
        g.transform.localScale = Vector3.one * radius;
        return k;
    }

    public void Open()
    {
        opening = true;
    }

    public void Close()
    {
        left.transform.localPosition = Vector3.zero;
        left.transform.rotation = lorigin;
        right.transform.localPosition = Vector3.zero;
        right.transform.rotation = rorigin;

        angle = 0;
        opening = false;
    }

    private void Update()
    {
        float dangle = 150 * Time.deltaTime;
        if (opening)
        {
            angle += dangle;
            if (angle > 60)
            {
                opening = false;
            }

            left.transform.RotateAround(transform.position + new Vector3(0, 1, 0), new Vector3(0, 0, 1), -dangle);
            right.transform.RotateAround(transform.position + new Vector3(0, 1, 0), new Vector3(0, 0, 1), dangle);
        }
    }
}


public class LeftHemisphere : MonoBehaviour
{
    private const int n = 30;
    private List<GameObject> voxels = new List<GameObject>();


    private void Awake()
    {
        float d = 2.0f / n;

        for (int xi = 0; xi < n / 2; ++xi)
        {
            for (int yi = 0; yi < n; ++yi)
            {
                foreach (var sign in new int[]{-1, 1})
                {
                    float x = -1.0f + 0.5f * d + xi * d;
                    float y = -1.0f + 0.5f * d + yi * d;

                    if (x*x + y*y > 1)
                    {
                        continue;
                    }
                    float z = Mathf.Sqrt(1 - x*x - y*y);

                    var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(x, y, z * sign);
                    cube.transform.localScale = new Vector3(2.0f / n, 2.0f / n, 2.0f / n);
                    cube.GetComponent<Renderer>().material.color = Color.yellow;
                    cube.transform.parent = gameObject.transform;

                    voxels.Add(cube);
                }
            }
        }
    }
}

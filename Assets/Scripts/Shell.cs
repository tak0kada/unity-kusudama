using System;
using System.Collections.Generic;
using UnityEngine;


public class Shell : MonoBehaviour
{
    private GameObject left;
    private GameObject right;
    private Quaternion lorigin;
    private Quaternion rorigin;
    private bool opening = false;
    private float angle = 0;


    public static Shell New(Vector3 position, float radius = 1.0f)
    {
        var g = new GameObject("Shell");
        var s = g.AddComponent<Shell>() as Shell;
        {
            s.left = new GameObject("LeftHemisphere");
            s.left.transform.parent = g.transform;
            s.left.AddComponent<LeftHemisphere>();
            s.lorigin = s.left.transform.rotation;

            s.right = new GameObject("RightHemisphere");
            s.right.transform.parent = g.transform;
            s.right.AddComponent<LeftHemisphere>();
            s.right.transform.Rotate(0, 180, 0);
            s.rorigin = s.right.transform.rotation;
        }

        g.transform.position = position;
        g.transform.localScale = Vector3.one * radius;
        return s;
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
                    AddCube(x, y, z * sign);
                }
            }
        }

        // fill center part
        for (int zi = -6; zi < 6 + 1; ++zi)
        {
            for (int yi = 0; yi < n; ++yi)
            {
                float y = -1.0f + 0.5f * d + yi * d;
                float z = zi * d;

                if (y*y + z*z > 1)
                {
                    continue;
                }
                float x = -Mathf.Sqrt(1 - y*y - z*z);
                AddCube(x, y, z);
            }
        }
    }

    private void AddCube(float x, float y, float z)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(x, y, z);
        cube.transform.localScale = new Vector3(2.1f / n, 2.1f / n, 2.1f / n);
        cube.GetComponent<Renderer>().material.color = Color.yellow;
        cube.transform.parent = gameObject.transform;

        voxels.Add(cube);
    }
}

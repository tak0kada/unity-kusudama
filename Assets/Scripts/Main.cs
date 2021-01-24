using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private int n = 0;
    private Kusudama kusudama;

    void Awake()
    {
        kusudama = Kusudama.Create(new Vector3(0, 4, 0));
        // kusudama.Close();
    }

    // Update is called once per frame
    void Update()
    {
        if (n == 0)
        {
            kusudama.Open();
            ++n;
        }
    }
}

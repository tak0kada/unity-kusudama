using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private int n = 0;
    private Shell shell;

    void Awake()
    {
        shell = Shell.New(new Vector3(0, 3.5f, 0));
        // shell.Close();
    }

    // Update is called once per frame
    void Update()
    {
        if (n == 0)
        {
            shell.Open();
            ++n;
        }
    }
}

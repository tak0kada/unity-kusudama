using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var kusudama = Kusudama.Create(new Vector3(0, 4, 0));
        // kusudama.Open();
        // kusudama.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

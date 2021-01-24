using System;
using UnityEngine;


public class Normal
{
    private static System.Random rnd = new System.Random();


    public static float Next(float mean, float std)
    {
        // https://stackoverflow.com/questions/218060/random-gaussian-variables
        float u1 = 1.0f - (float)rnd.NextDouble();
        u1 = u1 < 0.00001f ? 0.00001f : u1;
        float u2 = 1.0f - (float)rnd.NextDouble();
        return  mean + std * Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);
    }
}

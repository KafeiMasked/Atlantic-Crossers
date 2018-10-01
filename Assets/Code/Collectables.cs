using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Programmer: Noah Schwartz

public class Collectables : MonoBehaviour {

    private GameObject collect;
    private double probability;

    public Collectables()
    {
        collect = null;
        probability = 1f;
    }
    public Collectables(GameObject collect, double probability)
    {
        this.collect = collect;
        this.probability = probability;
    }
    public Collectables(string collect, double probability)
    {
        this.collect = (GameObject)Resources.Load(collect, typeof(GameObject));

        if("" + this.collect == "")
        {
            this.collect = null;
            Debug.Log("GAMEOBJECT " + collect + " COLLECT NOT FOUND IN RESOURCES");
        }
        this.probability = probability;
    }

    public GameObject GetCollect()
    {
        return collect;
    }
    public double GetProbability()
    {
        return probability;
    }

    public override string ToString()
    {
        return "GameObject " + collect + "\n" +
               "Probability " + probability + "\n";
    }
}


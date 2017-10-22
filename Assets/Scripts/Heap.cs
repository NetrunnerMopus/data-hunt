using UnityEngine;

public class Heap
{
    public GameObject Zone { get; private set; }

    public Heap(GameObject heapZone)
    {
        this.Zone = heapZone;
    }
}

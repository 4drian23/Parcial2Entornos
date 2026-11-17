using System;
using System.Linq;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Outline[] objetosAgarrables;

    void Start()
    {
        int count = objetosAgarrables.Count();
        for (int i = 0; i < count; i++)
        {
            objetosAgarrables[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}

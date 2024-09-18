using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    public List<GameObject> Units;

    private void Awake()
    {
        Instance = this;

        Units = new List<GameObject>();
  
        Units = GameObject.FindGameObjectsWithTag("Unit").ToList();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

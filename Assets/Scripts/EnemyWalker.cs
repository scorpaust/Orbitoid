using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : Enemy //INHERITANCE
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        Patrol();
    }

}

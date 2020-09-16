using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearBehaviour : ItemBase
{
    private PlayerBehaviour player;
    public SpearBehaviour Lanca;

    // Start is called before the first frame update
    private void Start()
    {
        player = FindObjectOfType(typeof(PlayerBehaviour)) as PlayerBehaviour;

        
    }

}

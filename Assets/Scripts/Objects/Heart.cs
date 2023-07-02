using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Powerup
{

    public FloatValue playerHelth;
    public FloatValue hearContainers;
    public float amoutToIncrease;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {

            playerHelth.RuntimeValue += amoutToIncrease;
            if (playerHelth.initialValue > hearContainers.RuntimeValue * 2f)
            {
                playerHelth.initialValue = hearContainers.RuntimeValue * 2f;
            }
            powerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }

}

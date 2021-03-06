using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellBurn : MonoBehaviour
{
    public float waitTimeToAttack = 3;
    public Animator[] anim;
    private int numBurn;
    private int cont = 0;
    // Start is called before the first frame update
    void Start()
    {
        numBurn = 0;
        cont = 0;
        PatronBurn();
    }
    private void PatronBurn()
    {      
        if (cont == 0)
        {
            anim[numBurn].Play("Burn");
            numBurn++;
            if (numBurn == 2)
            {
                cont++;
            }
        }
        else if (cont == 1)
        {
            anim[numBurn].Play("Burn");
            numBurn--;
            if (numBurn == 0)
            {
                cont++;
            }
        }
        else
        {
            anim[0].Play("Burn");
            anim[2].Play("Burn");
            cont = 0;
        }
        Invoke("PatronBurn", waitTimeToAttack);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEnd : MonoBehaviour
{
    public StartEnd startend;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startend.End)
        {
            AudioManager.PlayGroundMove();
            Destroy(gameObject,.5f);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class Singleton<Stats> : MonoBehaviour where Stats : MonoBehaviour{

    // Check to see if we're about to be destroyed.
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    private static Stats instance;

    /// <summary>
    /// Access singleton instance through this propriety.s
    /// </summary>
    public static Stats Instance
    {
        get
        {
            return instance;
        }
    }
    
    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    

    private void OnApplicationQuit()
    {
        m_ShuttingDown = true;
    }


    private void OnDestroy()
    {
        m_ShuttingDown = true;
    }
}
*/
public class Stats : MonoBehaviour
{
    public float life = 50;
    public float speed = 4f;
    //for later use - floating comportement
    [Range(0, 10)]
    public float pertinence = 5;

    private void Update()
    {
        if (life == 0)
        {
            Destroy(gameObject);
        }
    }
}


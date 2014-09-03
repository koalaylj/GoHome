using UnityEngine;
using System.Collections;

public class DataManager
{

    private DataManager _instance;

    public DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DataManager();
            }
            return _instance;
        }
    }

    private DataManager()
    {

    }



}

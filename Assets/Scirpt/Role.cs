using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour
{


    public enum role 
    {
        Enemy,
        Player
    }

    [SerializeField] public role _currentrole;
}

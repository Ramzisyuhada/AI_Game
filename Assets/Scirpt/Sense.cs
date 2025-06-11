using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sense : MonoBehaviour
{


    [SerializeField] public GameObject Head;
    protected Role.role role1 =  Role.role.Player;
    protected virtual void SenseInit() { }

    protected virtual void SenseUpdate() { }


    void Start()
    {
        SenseInit();

    }

    // Update is called once per frame
    void Update()
    {
        SenseUpdate();
      
    }



}

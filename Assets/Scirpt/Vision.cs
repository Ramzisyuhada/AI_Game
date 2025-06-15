using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Vision : Sense
{
    private float fov = 45f;
    private float distance = 100f;

    private GameObject player;

/*    [SerializeField] private UnityEditor.Animations.AnimatorController animatorController;
*/    protected override void SenseInit() {
        player = GameObject.FindWithTag("Player");
    }
    
    protected override void SenseUpdate() {
        RaycastHit hit;
        
        Vector3 dir = player.transform.position- transform.position ;
        if (Vector3.Angle(dir, transform.forward) < fov)
        {
            if (Physics.Raycast(transform.position, dir, out hit, distance))
            {
                Role role2 = hit.transform.transform.GetComponent<Role>();
                Debug.DrawRay(transform.position, dir, Color.red);
                Debug.DrawRay(transform.position, transform.forward, Color.red);
                if (role2 != null) {

                    if (role2._currentrole == role1)
                    {
                        Move();

                      
                    }
                }

            }
           

        }

        draw();

    }

    private void draw()
    {
        Vector3 frontRayPoint = transform.position +
        (transform.forward * distance);
        Vector3 leftRayPoint = Quaternion.Euler(0, fov * 0.5f, 0) *
frontRayPoint;
        Vector3 rightRayPoint = Quaternion.Euler(0, -fov * 0.5f, 0) *
 frontRayPoint;


        Debug.DrawLine(transform.position, frontRayPoint, Color.green);
        Debug.DrawLine(transform.position, leftRayPoint, Color.green);
        Debug.DrawLine(transform.position, rightRayPoint, Color.green);
    }
    public void Move()
    {

        Vector3 direction = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z);
        Quaternion targerrotasi = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, targerrotasi, Time.deltaTime *2f);
        transform.Translate(Vector3.forward*Time.deltaTime * 2f);
    }
}

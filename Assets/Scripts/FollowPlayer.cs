using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField]
    private Transform target;
  

   
    private Vector3 offsetPosition;
   

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;

    private void Start()
    {
        offsetPosition = new Vector3(0, 3, -2);
        
    }

    private void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (target == null)
        {
            Debug.LogWarning("Missing target ref !", this);

            return;
        }

        // compute position
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = target.TransformPoint(offsetPosition);
        }
        else
        {
            transform.position = target.position + offsetPosition;
        }

        // compute rotation
        if (lookAt)
        {
            transform.LookAt(target);
        }
        else
        {
            transform.rotation = target.rotation;
        }
    }

    /*public Transform player;
    private Vector3 offset;
    public float smoothSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position;
        
        
    }
    // Update is called once per frame

    
    //------------------
    //in update function if we update the objects locatipn and camera at the same time we can lose from smoothness so we use late update
    // which is the update after the update function
    void LateUpdate()
    {
        Vector3 newPos = player.position + offset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothSpeed);

        //transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y, 0);
    }*/

}

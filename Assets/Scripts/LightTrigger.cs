using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightTrigger : MonoBehaviour
{
    [SerializeField] public ChildBehaviourUnityEvent childFound;
    [SerializeField] string myIdentity;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Child")
        {
            ChildBehaviour childBehaviour = collision.gameObject.GetComponent<ChildBehaviour>();
            if(childBehaviour.found == false)
            {
                //childBehaviour.SetFound(myIdentity);
                //childFound?.Invoke(childBehaviour);

            }
        }
    }
}

[System.Serializable]
public class ChildBehaviourUnityEvent : UnityEvent<ChildBehaviour> { }

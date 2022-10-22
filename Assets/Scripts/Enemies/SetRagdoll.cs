using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRagdoll : MonoBehaviour
{
    [SerializeField] private GameObject Parent;
    [SerializeField] private Animation _animation;
    [SerializeField] private GameObject[] Limbs;
    void Start()
    {
        SetState(false);
    }

    public void SetState(bool state)
    {
        Parent.GetComponent<CapsuleCollider>().enabled = !state;
        Parent.GetComponent<Rigidbody>().detectCollisions = !state;
        Parent.GetComponent<Rigidbody>().isKinematic = state;
        Parent.GetComponent<Rigidbody>().useGravity = !state;
        _animation.enabled = !state;
        
        foreach (var limb in Limbs)
        {
            if (limb.TryGetComponent(out CapsuleCollider capsule))
            {
                capsule.enabled = state;
            }
            else if (limb.TryGetComponent(out SphereCollider sphere))
            {
                sphere.enabled = state;
            }
            else
            {
                limb.GetComponent<BoxCollider>().enabled = state;
            }

            if (limb.TryGetComponent(out CharacterJoint joint))
            {
                joint.enableCollision = state;
            }
            
            limb.GetComponent<Rigidbody>().detectCollisions = state;
            limb.GetComponent<Rigidbody>().isKinematic = !state;
            limb.GetComponent<Rigidbody>().useGravity = state;
        }
    }
}

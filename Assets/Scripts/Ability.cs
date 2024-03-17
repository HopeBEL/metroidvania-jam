using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public GameObject objectToInstantiate;
    public bool hasAbility = false;
    public bool isAbilityActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() 
    {
        
    }

    public virtual void AbilityAcquired() {}

    public virtual void LaunchAbility() {}
}

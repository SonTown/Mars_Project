using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character_Function {
    public class Build_Function
    {
        GameObject empty;
        Ray targetingRay;
        public bool BuildPosition(GameObject Player,out Vector3 Position, float rayLength, string layer)
        {
            targetingRay=new Ray(Player.transform.position,Player.transform.forward);
            if(Utilites.isTargetAvailable(Player,out empty, null, rayLength, layer))
            {
                Position= empty.transform.position;
                //some code to make a tooltip;
                return false;
            }
            Position= targetingRay.GetPoint(rayLength);
            return true;
        }
    }
}


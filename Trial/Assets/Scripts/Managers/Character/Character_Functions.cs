using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Structs;

namespace Character_Function {
    public class Build_Function
    {
        GameObject empty;
        Ray targetingRay;
        public bool BuildPosition(GameObject Player, out Vector3 Position, out GameObject Target, float rayLength, string layer)
        {
            targetingRay=new Ray(Player.transform.position,Player.transform.forward);
            if(Utilites.isTargetAvailable(Player,out empty, null, rayLength, layer))
            {
                Position= empty.transform.position;
                Target = empty;
                //some code to make a tooltip;
                return true;
            }
            Position= targetingRay.GetPoint(rayLength);
            Target = null;
            return false;
        }
    }
}


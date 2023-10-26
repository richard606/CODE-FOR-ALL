using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISupplyNeedObject 
{
    
    Vector3 GetPositionPlayerMove();
    Quaternion GetRotationPlayerInteraction();   
    void ExitInteraction(AIController controller);
    void EnterInteraction(AIController controller);
}

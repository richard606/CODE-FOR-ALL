using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class DDMPanelExtension 
{    

    public static void SetParentTransformToObject(this DragDropManager ddm, int indexObject, int indexPanel)
    {
        ddm.AllObjects[indexObject].GetComponent<RectTransform>().SetParent(ddm.AllPanels[indexPanel].GetComponent<RectTransform>());
        
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(DragDropManager))]
public class DDMFinder : MonoBehaviour
{
    [SerializeField] DragDropManager dragDropManager;

    ObjectSettings[] objectSettings;
    PanelSettings[] panelSettings;

    public bool findPanels;
    public bool findObjects;

    public int GetPanelCount() {
        return panelSettings.Length;
    }

    private void Awake()
    {
        dragDropManager = GetComponent<DragDropManager>();

        if (findObjects) 
        { 
            objectSettings = FindObjectsOfType<ObjectSettings>();
        
            int i = 0;
            foreach (ObjectSettings objectSettings in objectSettings)
            {
                objectSettings.Id = "Object" + i;
                i++;
            }
            dragDropManager.AllObjects = objectSettings;
        }

        if (findPanels)
        {
            panelSettings = FindObjectsOfType<PanelSettings>();
            int x = 0;
            foreach (PanelSettings panelSettings in panelSettings)
            {
                panelSettings.Id = "Panel" + x;
                x++;
            }
            dragDropManager.AllPanels = panelSettings;
        }

        
        
    }




   
}

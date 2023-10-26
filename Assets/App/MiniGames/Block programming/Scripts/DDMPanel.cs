using CodeForAll.APP.MiniGames.BlocksProgramming.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DDMPanel : MonoBehaviour, IPointerUpHandler, IBeginDragHandler, IEndDragHandler,IPointerDownHandler,IDragHandler
{
    private PanelSettings panelSettings;
    DragDropManager dragDropManager;


    private bool startCorrutine=false;
    private bool isBeginDrag=false;

    private void Awake()
    {
        panelSettings = GetComponent<PanelSettings>();
        dragDropManager = FindObjectOfType<DragDropManager>();
    }
    void Start()
    {
        panelSettings.OnObjectDropped.AddListener(OnObjectDropped2);
    }

    private void OnObjectDropped2()
    {   
            for (int i = 0; i < dragDropManager.AllObjects.Length; i++)
            {
                for (int j = 0; j < dragDropManager.AllPanels.Length; j++)
                {
                    // check if the object is on any panel
                    if (RectTransformUtility.RectangleContainsScreenPoint(dragDropManager.AllPanels[j].GetComponent<RectTransform>(), dragDropManager.AllObjects[i].GetComponent<RectTransform>().position))
                    {
                        if (dragDropManager.AllObjects[i].ScaleOnDropped)
                        {
                        dragDropManager.AllObjects[i].GetComponent<RectTransform>().localScale = dragDropManager.AllObjects[i].DropScale;
                        }

                    // Setting the Id of object for Panel Object detection
                    dragDropManager.SetPanelObject(j, dragDropManager.AllObjects[i].Id);
                    dragDropManager.AllObjects[i].Dropped = true;
                    if(!startCorrutine)
                        StartCoroutine(ParentRoutine(dragDropManager.AllObjects[i].GetComponent<RectTransform>(), dragDropManager.AllPanels[j].GetComponent<RectTransform>(),j));

                    MoveCommandObject moveCommandObject = dragDropManager.AllObjects[i].GetComponent<MoveCommandObject>();
                    moveCommandObject?.AddCommand(j);

                }
                }            
                       
            }
        
    }

    private IEnumerator ParentRoutine(RectTransform rectTransform, RectTransform rectTransform1,int indexPnl)
    {        
        yield return new WaitForSeconds(0.1f);
        rectTransform.SetParent(rectTransform1);
        rectTransform.SetAsFirstSibling();
    }

    private void OnObjectDropped()
    {
        Debug.Log("AJA 0");

        for (int i = 0; i < panelSettings.PanelIdManager.Count; i++)
        {
            for (int j = 0; j < dragDropManager.AllObjects.Length; j++)
            {

                if (dragDropManager.AllObjects[j].Id == panelSettings.PanelIdManager[i])
                {
                    Debug.Log("AJA 1");
                    dragDropManager.AllObjects[j].GetComponent<RectTransform>().SetAsLastSibling();

                    for (int k = 0; k < dragDropManager.AllPanels.Length; k++)
                    {
                        if (dragDropManager.AllPanels[k].Id == panelSettings.Id)
                        {
                            dragDropManager.SetPanelObject(k, dragDropManager.AllObjects[j].Id);
                            

                            dragDropManager.SetParentTransformToObject(k , Int32.Parse ( dragDropManager.AllObjects[j].Id));
                        }
                    }
                }
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        if (isBeginDrag) return;
        for (int i = 0; i < dragDropManager.AllObjects.Length; i++)
        {

            // check if the object is on any panel
            if (RectTransformUtility.RectangleContainsScreenPoint(dragDropManager.AllObjects[i].GetComponent<RectTransform>(), Input.mousePosition,Camera.main))
            {

                // Setting the Id of object for Panel Object detection

                MoveCommandObject moveCommandObject = dragDropManager.AllObjects[i].GetComponent<MoveCommandObject>();
                for (int x = 0; x < dragDropManager.AllPanels.Length; x++)
                {
                    if (RectTransformUtility.RectangleContainsScreenPoint(dragDropManager.AllObjects[i].GetComponent<RectTransform>(), dragDropManager.AllPanels[x].GetComponent<RectTransform>().position, Camera.main))
                    {
                        print("Coommaassfv  " + x);
                        moveCommandObject.RemoveCommand(x);
                        break;
                    }
                }

                dragDropManager.AllObjects[i].Dropped = false;
                dragDropManager.AllObjects[i].GetComponent<RectTransform>().position = dragDropManager.AllObjects[i].FirstPos;
                dragDropManager.AllObjects[i].GetComponent<RectTransform>().SetParent(dragDropManager.FirstCanvas.GetComponent<RectTransform>());




                break;


            }


        }
    }



    public void OnBeginDrag(PointerEventData eventData)
    {
        print("Empezo el drag");
        isBeginDrag = true;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("Se termino  drageadio");
        isBeginDrag = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("Hoalalala jajsjd");
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("Se esta drageadio");
    }
}

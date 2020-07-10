using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public GameObject m_Menu;
    public Transform menuHolder;
    public Transform camHolder;
    public Camera cam;
    public bool clicked = false;

    public void Manager()
    {

        if (OVRInput.GetDown(OVRInput.Button.Back))
        {
            //this.gameObject.transform.localPosition = cam.transform.localPosition + (cam.transform.forward * 2);
            //this.gameObject.transform.rotation = cam.transform.rotation;
            menuHolder.transform.localPosition = camHolder.transform.position;
            menuHolder.transform.rotation = camHolder.transform.rotation;
            Debug.Log("<-----------Back Button Pressed---------->");
        }
    }
}

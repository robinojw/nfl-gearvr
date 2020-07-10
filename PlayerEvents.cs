using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour
{

    #region Events
    public static UnityAction OnTouchpadUp = null;
    public static UnityAction onTouchpadDown = null;
    public static UnityAction onTriggerUp = null;
    public static UnityAction onTriggerDown = null;
    public static UnityAction onBackButtonUp = null;
    public static UnityAction onBackButtonDown = null;
    public static UnityAction<OVRInput.Controller, GameObject> OnControllerSource = null;
    #endregion

    #region Anchors
    public GameObject m_LeftAnchor;
    public GameObject m_RightAnchor;
    public GameObject m_HeadAnchor;
    #endregion

    #region Input
    private Dictionary<OVRInput.Controller, GameObject> m_ControllerSets = null;
    private OVRInput.Controller m_InputSource = OVRInput.Controller.None;
    private OVRInput.Controller m_Controller = OVRInput.Controller.None;
    private bool m_InputActive = true;
    #endregion

    public GameObject m_Menu;
    public float dist;

    private void Awake()
    {
        OVRManager.HMDMounted += PlayerFound;
        OVRManager.HMDUnmounted += PlayerLost;

        m_ControllerSets = CreateControllerSets();
    }

    private void OnDestroy()
    {
        OVRManager.HMDMounted -= PlayerFound;
        OVRManager.HMDUnmounted -= PlayerLost;
    }

    // Update is called once per frame
    void Update()
    {
        //Check for Active Input
        if (!m_InputActive)
            return;
        

        //Check if controller exists
        CheckForController();

            
            //Check for input source
            CheckInputSource();

        //Check for actual input
        Input();

    }

    private void CheckForController()
    {
        OVRInput.Controller controllerCheck = m_Controller;

        //Right remote
        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
            controllerCheck = OVRInput.Controller.RTrackedRemote;

        //Left remote
        if (OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote))
            controllerCheck = OVRInput.Controller.LTrackedRemote;

        //If no controllers are connected, switch to headset input
        if (OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote) &&
        OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
            controllerCheck = OVRInput.Controller.Touchpad;

        //Update
        m_Controller = UpdateSource(controllerCheck, m_Controller);
    }

    private void CheckInputSource()
    {
        ////Left remote
        //if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.LTrackedRemote)) {
        //    Debug.Log("Left Input");
        //    Debug.Log("Controller Check: " + m_Controller);
        //}

        ////Right remote
        //if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.RTrackedRemote))
        //{
        //    Debug.Log("Right Input");
        //    Debug.Log("Controller Check: " + m_Controller);
        //}

        ////Headset
        //if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.Touchpad))
        //{
        //    Debug.Log("Headset Input");
        //    Debug.Log("Controller Check: " + m_Controller);
        //}

        //Update
        m_InputSource = UpdateSource(OVRInput.GetActiveController(), m_InputSource);
    }

    private void Input()
    {
        //Touchpad down
        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        {
            if (onTouchpadDown != null)
                onTouchpadDown();
        }

        //Touchpad up
        if (OVRInput.GetUp(OVRInput.Button.PrimaryTouchpad))
        {
            if (OnTouchpadUp != null)
                OnTouchpadUp();
        }
        //Trigger Down
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (onTriggerDown != null)
                onTriggerDown();
        }

        //Trigger Up
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (onTriggerUp != null)
                onTriggerUp();
        }

        //Back button down
        if (OVRInput.GetDown(OVRInput.Button.Back))
        {
            if (onBackButtonDown != null)
                onBackButtonDown();
        }

        //Back button up
        if (OVRInput.GetUp(OVRInput.Button.Back))
        {
            if (onBackButtonUp != null)
                onBackButtonUp();
        }
    }



    private OVRInput.Controller UpdateSource(OVRInput.Controller check, OVRInput.Controller previous)
    {
        //If values are the same, return
        if (check == previous)
            return previous;

        //Get controller object
        GameObject controllerObject = null;
        m_ControllerSets.TryGetValue(check, out controllerObject);

        //If no controller object, set to the head
        if (controllerObject == null)
            controllerObject = m_HeadAnchor;

        //Send out the event
        if (OnControllerSource != null)
            OnControllerSource(check, controllerObject);

        //Return new value
        return check;
    }

    //public void CheckMenuDistance()
    //{
    //    dist = Vector3.Distance(m_Menu.transform.position, m_HeadAnchor.transform.position);
    //    Debug.Log("Distance between the camera and the menu" + dist);

    //    //Check the distance between the camera and the menu, if it's greater than 
    //    //the default, reset the menu position and rotation

    //    if(dist > 317.3021)
    //    {
    //        dist = 317.3021f;
    //        m_Menu.transform.position = m_HeadAnchor.transform.position;
    //        dist = Vector3.Distance(m_Menu.transform.position, m_HeadAnchor.transform.position);
    //        m_HeadAnchor.transform.rotation = m_Menu.transform.rotation;
    //    }
    //}

    private void PlayerFound() { m_InputActive = true;}

    private void PlayerLost() { m_InputActive = false; }

    private Dictionary<OVRInput.Controller, GameObject> CreateControllerSets() {
        Dictionary<OVRInput.Controller, GameObject> newSets = new Dictionary<OVRInput.Controller, GameObject>()
        {
            { OVRInput.Controller.LTrackedRemote, m_LeftAnchor },
            { OVRInput.Controller.RTrackedRemote, m_RightAnchor},
            { OVRInput.Controller.Touchpad, m_HeadAnchor }
         };

        return newSets;
    }
}

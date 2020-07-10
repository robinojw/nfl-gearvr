using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Pointer : MonoBehaviour
{
    public float m_Distance = 10.0f;
    public LineRenderer m_LineRenderer = null;
    public LayerMask m_EverythingMask = 0;
    public LayerMask m_InteractableMask = 0;
    public UnityAction<Vector3, GameObject> OnPointerUpdate = null;


    private Transform m_CurrentOrigin = null;
    private GameObject m_CurrentObject = null;
    private Camera m_Camera = null;

    //Menu Switching
    public GameObject m_Menu;
    private bool menuActive = true;

    private void Awake()
    {
        PlayerEvents.OnControllerSource += UpdateOrigin;
        PlayerEvents.onTouchpadDown += ProcessTouchPadDown;
        PlayerEvents.onTriggerDown += ProcessTriggerDown;
        PlayerEvents.onBackButtonDown += ProcessBackButton;

        m_Camera = Camera.main;
    }

    private void Start()
    {
        SetLineColour();
    }

    private void OnDestroy()
    {
        PlayerEvents.OnControllerSource -= UpdateOrigin;
        PlayerEvents.onTouchpadDown -= ProcessTouchPadDown;
        PlayerEvents.onTriggerDown -= ProcessTriggerDown;
        PlayerEvents.onBackButtonDown -= ProcessBackButton;
    }

    private void Update()
    {
        Vector3 hitPoint = UpdateLine();

        m_CurrentObject = UpdatePointerStatus();

       //TODO add glow to a UI object when hovering over it.

        if (OnPointerUpdate != null)
            OnPointerUpdate(hitPoint, m_CurrentObject);
    }

    private Vector3 UpdateLine()
    {
        //Create ray
        RaycastHit hit = CreateRaycast(m_EverythingMask);

        //Defualt end
        Vector3 endPosition = m_CurrentOrigin.position + (m_CurrentOrigin.forward * m_Distance);

        //Check hit
        if (hit.collider != null)
        {
            endPosition = hit.point;
        }


        //Set Position
        m_LineRenderer.SetPosition(0, m_CurrentOrigin.position);
        m_LineRenderer.SetPosition(1, endPosition);

        return endPosition;
    }

    private void UpdateOrigin(OVRInput.Controller controller, GameObject controllerObject) {
        //Set origin of pointer 
        m_CurrentOrigin = controllerObject.transform;

        //Is the laser visible
        if(controller == OVRInput.Controller.Touchpad)
        {
            m_LineRenderer.enabled = false;
        }
        else
        {
            m_LineRenderer.enabled = true;
        }

    }

    private GameObject UpdatePointerStatus()
    {
        //Create ray
        RaycastHit hit = CreateRaycast(m_InteractableMask);

        //Check hit
        if (hit.collider)
            return hit.collider.gameObject;

        //return
        return null;
    }

    private RaycastHit CreateRaycast(int layer) {
        RaycastHit hit;
        Ray ray = new Ray(m_CurrentOrigin.position, m_CurrentOrigin.forward);
        Physics.Raycast(ray, out hit, m_Distance, layer);

        return hit;
      }

    private void SetLineColour()
    {
        if (!m_LineRenderer)
            return;

        Color endColor = Color.white;
        endColor.a = 0.0f;

        m_LineRenderer.endColor = endColor;

    }

    //If touch pad is pressed, what happens?
    private void ProcessTouchPadDown()
    {
        //Don't check whether over interactable object, show/hide menu func
        if(menuActive == true)
        {
            menuActive = false;
            m_Menu.SetActive(false);
        }
        else
        {
            menuActive = true;
            m_Menu.SetActive(true);
            //Instantiate(m_Menu);
            m_Camera.transform.localPosition = m_Menu.transform.localPosition;

            Debug.Log("<-------------Menu Active---------------->");
        }

    }

    private void ProcessBackButton()
    {
        m_Menu.GetComponent<MenuManager>().Manager();
    }

    //If trigger is pressed, what happens?
    private void ProcessTriggerDown()
    {
        //If the current object is not interactable, return
        if (!m_CurrentObject)
            return;

        Interactable interactable = m_CurrentObject.GetComponent<Interactable>();
        interactable.Pressed();
    }
}

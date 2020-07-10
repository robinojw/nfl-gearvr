using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] public PlayLoader loader;
    [SerializeField] public Material Inactive; //These can be assigned in the editor to different materials depending on the button
    [SerializeField] public Material Active;

    private bool active = false;

    private Renderer rend;
    public PlayLoader pLoader;
    public GameObject CameraRig;
    public GameObject m_Menu;
    public PlayerEvents playerEvents;
    public PlaySwitcher playReference;
    public PlayLoader playloader;

    public void Pressed()
    {

        //Play/Pause Button
        if (this.gameObject.name == "Play")
        {

            if (active == true)
            {
                active = false;
                loader.Pause();
                this.gameObject.GetComponent<Renderer>().material = Inactive;
            }
            else
            {
                loader.Play();
                active = true;
                this.gameObject.GetComponent<Renderer>().material = Active;
            }
        }

        //Rewind Button
        if (this.gameObject.name == "Rewind")
            pLoader.Rewind();

        //Fast-Forward Button
        if (this.gameObject.name == "Fastforward")
            pLoader.Forward();

        //Reset Button
        if (this.gameObject.name == "event")
            pLoader.Reset();


        //Teleport Home
        if (this.gameObject.name == "Home")
        {
            CameraRig.transform.position = GameObject.Find("HomeLocation").transform.position;
            CameraRig.transform.rotation = Quaternion.Euler(0, 90, 0);
            //playerEvents.CheckMenuDistance();
        }

        //Teleport Right
        if (this.gameObject.name == "Right")
        {
            CameraRig.transform.position = GameObject.Find("EastLocation").transform.position;
            CameraRig.transform.rotation = Quaternion.Euler(0, 0, 0);
            //playerEvents.CheckMenuDistance();
            //m_Menu.transform.position = CameraRig.transform.position;
            //m_Menu.transform.rotation = CameraRig.transform.rotation;
        }

        //Teleport Left
        if (this.gameObject.name == "Left")
        {
            CameraRig.transform.position = GameObject.Find("WestLocation").transform.position;
            CameraRig.transform.rotation = Quaternion.Euler(0, 180, 0);
            //playerEvents.CheckMenuDistance();
        }

        //Teleport Far
        if (this.gameObject.name == "Far")
        {
            CameraRig.transform.position = GameObject.Find("NorthLocation").transform.position;
            CameraRig.transform.rotation = Quaternion.Euler(0, 260, 0);
            //playerEvents.CheckMenuDistance();
        }

        //Teleport Field
        if (this.gameObject.name == "Field")
        { 
            CameraRig.transform.position = GameObject.Find("FieldLocation").transform.position;
            CameraRig.transform.rotation = Quaternion.Euler(0, -90, 0);
            //playerEvents.CheckMenuDistance();
        }

        //Switch to play 1
        if (this.gameObject.name == "Play1")
            playloader.SetPlay("play1");

        //Switch to play 2
        if (this.gameObject.name == "Play2")
            playloader.SetPlay("play2");

        //Switch to play 3
        if (this.gameObject.name == "Play3")
            playloader.SetPlay("play3");

        //Switch to play 4 
        if (this.gameObject.name == "Play4")
            playloader.SetPlay("play4");

        //Switch to play 5
        if (this.gameObject.name == "Play5")
            playloader.SetPlay("play5");

        //Switch to play 6
        if (this.gameObject.name == "Play6")
            playloader.SetPlay("play6");
    }

}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using UnityEngine.SceneManagement;
//using VRStandardAssets.Utils;

//namespace VRStandardAssets.Menu
//{ 
//    public class End : MonoBehaviour
//        {   

//        public event Action<Restart> OnButtonSelected;

//        [SerializeField] private PlayLoader m_Playloader;
//        // Start is called before the first frame update
//        [SerializeField] private SelectionRadial m_SelectionRadial;         // This controls when the selection is complete.
//        [SerializeField] private VRInteractiveItem m_InteractiveItem;       // The interactive item for where the user should click to load the level.


//        private bool m_GazeOver;
     

//        private void OnEnable()
//        {
//            m_InteractiveItem.OnOver += HandleOver;
//            m_InteractiveItem.OnOut += HandleOut;
//            m_InteractiveItem.OnClick += HandleSelectionComplete;
//        }


//        private void OnDisable()
//        {
//            m_InteractiveItem.OnOver -= HandleOver;
//            m_InteractiveItem.OnOut -= HandleOut;
//            m_InteractiveItem.OnClick -= HandleSelectionComplete;
//        }


//        private void HandleOver()
//        {
//            // When the user looks at the rendering of the scene, show the radial.
//            //m_SelectionRadial.Show();

//            m_GazeOver = true;
//        }

//        private void HandleOut()
//        {
//            // When the user looks away from the rendering of the scene, hide the radial.
//            //m_SelectionRadial.Hide();

//            m_GazeOver = false;
//        }


//        private void HandleSelectionComplete()
//        {
//            // If the user is looking at the rendering of the scene when the radial's selection finishes, activate the button.
//            if (m_GazeOver)
//                //StartCoroutine (ActivateButton());
//                ActivateButton();
//        }


//        private void ActivateButton()
//        {
//            m_Playloader.End();
//        }
//    }
//}


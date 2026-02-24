using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorkingTelevisions
{
    public class tvRemote : MonoBehaviour
    {
        public GameObject targetTV;
        //------------------------------------------------------------------------------------------------------------------------------------------
        void Start()
        {
        }
        void Update()
        {
        }
        private void OnTriggerEnter(Collider other){
            if (other.gameObject.tag == "Player"){ targetTV.GetComponent<televisionCode>().inRange = true; }
        }
        private void OnTriggerExit(Collider other){
            if (other.gameObject.tag == "Player"){ targetTV.GetComponent<televisionCode>().inRange = false; }
        }
    }
}
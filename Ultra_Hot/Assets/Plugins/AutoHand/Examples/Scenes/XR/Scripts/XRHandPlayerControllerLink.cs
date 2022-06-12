using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Autohand.Demo {
public class XRHandPlayerControllerLink : MonoBehaviour{
        public XRHandControllerLink moveController;
        public XRHandControllerLink turnController;
        public AutoHandPlayer player;
        public GameObject tp;

        private bool GodMode = false;

        [Header("Input")]
        public Common2DAxis moveAxis;
        public Common2DAxis turnAxis;

        
        void Update(){
            if (Input.GetKeyDown(KeyCode.G))
            {
                GodMode = !GodMode;
                tp.SetActive(GodMode);
            }
            if (GodMode)
                player.Move(moveController.GetAxis2D(moveAxis));
            player.Turn(turnController.GetAxis2D(turnAxis).x);
        }
        void FixedUpdate(){
            if (GodMode)
                player.Move(moveController.GetAxis2D(moveAxis));
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VRInput : BaseInput
{
    public Camera eventCamera = null; // to keep track of the cursor (a dummy camera that attached to the hand)
                                      // (cursor position is just the middle of the camera)
    // variable for inputs
    public OVRInput.Button clickButton = OVRInput.Button.PrimaryIndexTrigger;
    public OVRInput.Controller controller = OVRInput.Controller.All;

    protected void Awake()
    {
        // attached to the event system
        GetComponent<BaseInputModule>().inputOverride = this;
    }

    // get inputs from the button that gets clicked
    public override bool GetMouseButton(int button)
    {
        return OVRInput.Get(clickButton, controller);
    }

    public override bool GetMouseButtonDown(int button)
    {
        return OVRInput.GetDown(clickButton, controller);
    }

    public override bool GetMouseButtonUp(int button)
    {
        return OVRInput.GetUp(clickButton, controller);
    }

    public override Vector2 mousePosition
    {
        // getter
        get
        {
            // middle of the eventCamera
            return new Vector2(eventCamera.pixelWidth / 2, eventCamera.pixelHeight / 2);
        }
    }
}

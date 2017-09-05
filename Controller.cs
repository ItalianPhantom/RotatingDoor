using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to each controller

public class Controller : MonoBehaviour
{

    public SteamVR_Controller.Device controller
    {
        get
        {
            // Retrieves controller information to avoid typing this out every time
            // Attached to both controllers
            return SteamVR_Controller.Input((int)GetComponent<SteamVR_TrackedObject>().index);
        }
    }

    public TouchPosition CurrentTouchPosition()
    {
        Vector2 pos = controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad); // Gets the position of finger on the touchpad (-1 to 1 on each axis)
        bool isTop = pos.y >= 0;
        bool isRight = pos.x >= 0;

        // Determines what part of the touchpad is being touched. Returns OFF if not being touched at all
        if (isTop)
        {
            if (isRight)
            {
                if (pos.y > pos.x)
                {
                    return TouchPosition.Up;
                }
                else if (pos.y < pos.x)
                {
                    return TouchPosition.Right;
                }
            }
            else
            {
                if (pos.y > -pos.x)
                {
                    return TouchPosition.Up;
                }
                else if (pos.y < -pos.x)
                {
                    return TouchPosition.Left;
                }
            }
        }
        else
        {
            if (isRight)
            {
                if (-pos.y > pos.x)
                {
                    return TouchPosition.Down;
                }
                else if (-pos.y < pos.x)
                {
                    return TouchPosition.Right;
                }
            }
            else
            {
                if (-pos.y > -pos.x)
                {
                    return TouchPosition.Down;
                }
                else if (-pos.y < -pos.x)
                {
                    return TouchPosition.Left;
                }
            }
        }

        return TouchPosition.Off;
    }

    // Create enums for all possible positions on touchpad
    public enum TouchPosition
    {
        Off, Up, Down, Left, Right
    }
}
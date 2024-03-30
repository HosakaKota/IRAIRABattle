using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Grab;

public class CheckIsMoved : MonoBehaviour
{

    public bool painterIsMoved;

    public void IsMoved()
    {
        painterIsMoved = true;
    }

}

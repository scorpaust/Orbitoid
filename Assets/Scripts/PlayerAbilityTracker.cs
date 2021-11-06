using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityTracker : MonoBehaviour
{
    private bool canDoubleJump, canDash, canBecomeBall, canDropBomb;

    public bool CanDoubleJump { get { return canDoubleJump; } set { canDoubleJump = value; } } // ENCAPSULATION

    public bool CanDash { get { return canDash; } set { canDash = value; } } // ENCAPSULATION

    public bool CanBecomeBall { get { return canBecomeBall; } set { canBecomeBall = value; } } // ENCAPSULATION

    public bool CanDropBomb { get { return canDropBomb; } set { canDropBomb = value; } } // ENCAPSULATION
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityTracker : MonoBehaviour
{
    private bool canDoubleJump, canDash, canBecomeBall, canDropBomb;

    public bool CanDoubleJump { get { return canDoubleJump; } set { canDoubleJump = value; } }

    public bool CanDash { get { return canDash; } set { canDash = value; } }

    public bool CanBecomeBall { get { return canBecomeBall; } set { canBecomeBall = value; } }

    public bool CanDropBomb { get { return canDropBomb; } set { canDropBomb = value; } }
}

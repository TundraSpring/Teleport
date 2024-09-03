using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teleport
{
    public enum ObjectEvent
    {
        HitboxEntered,
        HitboxExited,
        AttackHit,
        Spawn,
        Despawn,
        Knockout,
        Respawn,
        Jump,
        Land,
        Interact,
        Teleport
    }
}

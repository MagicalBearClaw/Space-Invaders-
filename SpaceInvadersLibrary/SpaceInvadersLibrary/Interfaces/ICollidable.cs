using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersLibrary
{
    /// <summary>
    /// Every class that wants to be collidable 
    /// may want to implement this interface.
    /// </summary>
    public interface ICollidable
    {
        Rectangle BoundingBox { get; }
        bool IsAlive { get; set; }
        int Points { get;}

    }
}

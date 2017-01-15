using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace SpaceInvadersLibrary
{
    /// <summary>
    /// Every class That will contain a collection of Icollidable 
    /// may want to implement this interface.
    /// </summary>
    public interface ICollidableCollection
    {
        Rectangle BoundingBox { get;}
        ICollidable this[int i] {get;}
        int GetLength { get; }
    }
}

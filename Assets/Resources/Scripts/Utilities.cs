using UnityEngine;
using System.Collections;

namespace Game_Utils
{
    public interface ISceneObject
    {
        // Called by the GameBrain on scene switch.
        void Initialize();
        // Called by the GameBrain's Update function.
        void ObjectUpdate();
    }
    public static class Utilities
    {

    }
}


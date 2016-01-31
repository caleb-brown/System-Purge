using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// This will store away all of our utility functions during this little game jam of mine.
/// </summary>
namespace GameUtils
{
    /// <summary>
    /// Collision point class containing relevant collision data: point(world space), contact normal, intensity (how hard?)
    /// </summary>
    public class CollisionPoint : IComparable<CollisionPoint>
    {
        public Vector3 point, normal;
        public float intensity;

        public int CompareTo(CollisionPoint other)
        {
            // Sort by whichever contact normal is closest to pointing straight up.
            return Vector3.Angle(normal, Vector3.up).CompareTo(Vector3.Angle(other.normal, Vector3.up));
        }
    }

    public interface ISceneObject
    {
        void ObjectUpdate();
        void Initialize();
    }

    public enum GameInput
    {
        JUMP // Add more as needed
    }

    public static class Utilities
    {
        public static Vector2 Vec2(Vector3 input)
        {
            return new Vector2(input.x, input.y);
        }
    }
    

    // Game State Enumerator
    public enum GameState
    {
        TITLE, // index 0
        MAINMENU, // index 1
        TLEVELONE,
        TLEVELTWO,
        LEVELONE,
        LEVELTWO,
        LEVELTHREE,
        LEVELFOUR
        // ... this goes on to define levels, most useful if kept in the same loading order that is to be used during build.
    }
}

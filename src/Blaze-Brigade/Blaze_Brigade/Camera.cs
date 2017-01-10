using Microsoft.Xna.Framework;

namespace View
{
    /// <summary>
    /// The camera class for making the scrollable camera.
    /// </summary>
    public class Camera
    {

        /**
        Constructor for the camera. Initial location is at 0,0.
        */
        public Camera()
        {
            Position = new Vector2(0, 0);
        }

        /**
        Sets and gets the camera position.
        */
        public Vector2 Position { get; set; }

        /**
        Returns the transformation matrix that specifies the camera position.
        This is the heart of the camera that enables camera movement.
        */
        public Matrix TransformMatrix
        {
            get
            {
                return Matrix.CreateTranslation(Position.X, Position.Y, 0);
            }
        }
    }
}

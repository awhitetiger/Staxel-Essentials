using Plukit.Base;

namespace StaxelEssentials
{
    class StaxelEssentialsHolder
    {
        private static Vector3D _backPosition;
        static public Vector3D GetBack()
        {
            return StaxelEssentialsHolder._backPosition;
        }

        static public void SetBack(Vector3D back)
        {
            StaxelEssentialsHolder._backPosition = back;
        }
    }
}

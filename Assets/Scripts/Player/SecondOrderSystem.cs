using System;
using UnityEngine;

namespace ProceduralFPS
{
    public class SecondOrderSystem
    {
        #region Private Variables

        public Vector3 xp;
        public Vector3 y, yd;
        public float _w, _z, _d, k1, k2, k3;

        #endregion

        #region General Functions

        public Vector3 Update(float deltaTime, Vector3 target, Vector3? xd = null)
        {
            if (xd == null)
            {
                xd = (target - xp) / deltaTime;
                xp = target;
            }

            float k1_Stable, k2_Stable;

            if (_w * deltaTime < _z)
            {
                k1_Stable = k1;
                k2_Stable = Mathf.Max(k2, deltaTime * deltaTime / 2 + deltaTime * k1 / 2, deltaTime * k1);
            }
            else
            {
                float t1 = Mathf.Exp(-_z * _w * deltaTime);
                float alpha = 2 * t1 * (_z <= 1 ? Mathf.Cos(deltaTime * _d) : (float)MathF.Cosh(deltaTime * _d));
                float beta = t1 * t1;
                float t2 = deltaTime / (1 + beta - alpha);
                k1_Stable = (1 - beta) * t2;
                k2_Stable = deltaTime * t2;
            }

            y = y + deltaTime * yd;
            yd = (Vector3)(yd + deltaTime * (target + k3 * xd - y - k1 * yd) / k2_Stable);

            return y;
        }

        public void UpdateSettings(Settings settings)
        {
            _w = 2 * Mathf.PI * settings.frequency;
            _z = settings.damping;
            _d = _w * Mathf.Sqrt(Mathf.Abs(settings.damping * settings.damping - 1));
            k1 = settings.damping / (Mathf.PI * settings.frequency);
            k2 = 1 / (_w * _w);
            k3 = settings.response * settings.damping / _w;
        }

        #endregion

        #region Public Classes

        [Serializable]
        public class Settings
        {
            public float frequency = 4.0f;
            public float damping = 0.5f;
            public float response = -4.0f;
        }

        #endregion
    }
}
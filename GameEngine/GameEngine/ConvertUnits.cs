using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    public static class ConvertUnits
    {
        private static float _displayUnitsToSimUnitsRatio = 100f;
        private static float _simUnitsToDisplayUnitsRatio = 1 / _displayUnitsToSimUnitsRatio;

        /// <summary>
        /// Ajusta las medidas para el display de Farseer a XNA.
        /// </summary>
        /// <param name="displayUnitsPerSimUnit">Medida de pixeles a KMS.</param>
        public static void SetDisplayUnitToSimUnitRatio(float displayUnitsPerSimUnit)
        {
            _displayUnitsToSimUnitsRatio = displayUnitsPerSimUnit;
            _simUnitsToDisplayUnitsRatio = 1 / displayUnitsPerSimUnit;
        }

        /// <summary>
        /// Convierte de unidades de Farseer a XNA.
        /// </summary>
        /// <param name="simUnits">Cantidad en unidades de Farseer.</param>
        /// <returns>Unidad en XNA.</returns>
        public static float ToDisplayUnits(float simUnits)
        {
            return simUnits * _displayUnitsToSimUnitsRatio;
        }

        /// <summary>
        /// Convierte de unidades de Farseer a XNA.
        /// </summary>
        /// <param name="simUnits">Cantidad en unidades de Farseer.</param>
        /// <returns>Unidad en XNA.</returns>
        public static float ToDisplayUnits(int simUnits)
        {
            return simUnits * _displayUnitsToSimUnitsRatio;
        }

        /// <summary>
        /// Convierte de unidades de Farseer a XNA.
        /// </summary>
        /// <param name="simUnits">Cantidad en unidades de Farseer.</param>
        /// <returns>Unidad en XNA.</returns>
        public static Vector2 ToDisplayUnits(Vector2 simUnits)
        {
            return simUnits * _displayUnitsToSimUnitsRatio;
        }

        /// <summary>
        /// Convierte de unidades de Farseer a XNA.
        /// </summary>
        /// <param name="simUnits">Cantidad en unidades de Farseer.</param>
        /// <returns>Unidad en XNA.</returns>
        public static void ToDisplayUnits(ref Vector2 simUnits, out Vector2 displayUnits)
        {
            Vector2.Multiply(ref simUnits, _displayUnitsToSimUnitsRatio, out displayUnits);
        }

        /// <summary>
        /// Convierte de unidades de Farseer a XNA.
        /// </summary>
        /// <param name="simUnits">Cantidad en unidades de Farseer.</param>
        /// <returns>Unidad en XNA.</returns>
        public static Vector3 ToDisplayUnits(Vector3 simUnits)
        {
            return simUnits * _displayUnitsToSimUnitsRatio;
        }

        /// <summary>
        /// Genera un nuevo Vector2 tomando (X, Y) por separado de Farseer a XNA.
        /// </summary>
        /// <param name="x">Medida X en unidades de Farseer.</param>
        /// <param name="y">Medida Y en unidades de Farseer.</param>
        /// <returns>Vector2 en unidades de XNA.</returns>
        public static Vector2 ToDisplayUnits(float x, float y)
        {
            return new Vector2(x, y) * _displayUnitsToSimUnitsRatio;
        }

        public static void ToDisplayUnits(float x, float y, out Vector2 displayUnits)
        {
            displayUnits = Vector2.Zero;
            displayUnits.X = x * _displayUnitsToSimUnitsRatio;
            displayUnits.Y = y * _displayUnitsToSimUnitsRatio;
        }

        /// <summary>
        /// Convierte de unidades de XNA a Farseer.
        /// </summary>
        /// <param name="simUnits">Cantidad en unidades de XNA.</param>
        /// <returns>Unidad en Farseer.</returns>
        public static float ToSimUnits(float displayUnits)
        {
            return displayUnits * _simUnitsToDisplayUnitsRatio;
        }

        /// <summary>
        /// Convierte de unidades de XNA a Farseer.
        /// </summary>
        /// <param name="simUnits">Cantidad en unidades de XNA.</param>
        /// <returns>Unidad en Farseer.</returns>
        public static float ToSimUnits(double displayUnits)
        {
            return (float)displayUnits * _simUnitsToDisplayUnitsRatio;
        }

        /// <summary>
        /// Convierte de unidades de XNA a Farseer.
        /// </summary>
        /// <param name="simUnits">Cantidad en unidades de XNA.</param>
        /// <returns>Unidad en Farseer.</returns>
        public static float ToSimUnits(int displayUnits)
        {
            return displayUnits * _simUnitsToDisplayUnitsRatio;
        }

        /// <summary>
        /// Convierte de unidades de XNA a Farseer.
        /// </summary>
        /// <param name="simUnits">Cantidad en unidades de XNA.</param>
        /// <returns>Unidad en Farseer.</returns>
        public static Vector2 ToSimUnits(Vector2 displayUnits)
        {
            return displayUnits * _simUnitsToDisplayUnitsRatio;
        }

        /// <summary>
        /// Convierte de unidades de XNA a Farseer.
        /// </summary>
        /// <param name="simUnits">Cantidad en unidades de XNA.</param>
        /// <returns>Unidad en Farseer.</returns>
        public static Vector3 ToSimUnits(Vector3 displayUnits)
        {
            return displayUnits * _simUnitsToDisplayUnitsRatio;
        }

        public static void ToSimUnits(ref Vector2 displayUnits, out Vector2 simUnits)
        {
            Vector2.Multiply(ref displayUnits, _simUnitsToDisplayUnitsRatio, out simUnits);
        }

        public static Vector2 ToSimUnits(float x, float y)
        {
            return new Vector2(x, y) * _simUnitsToDisplayUnitsRatio;
        }

        public static Vector2 ToSimUnits(double x, double y)
        {
            return new Vector2((float)x, (float)y) * _simUnitsToDisplayUnitsRatio;
        }

        public static void ToSimUnits(float x, float y, out Vector2 simUnits)
        {
            simUnits = Vector2.Zero;
            simUnits.X = x * _simUnitsToDisplayUnitsRatio;
            simUnits.Y = y * _simUnitsToDisplayUnitsRatio;
        }
    }
}

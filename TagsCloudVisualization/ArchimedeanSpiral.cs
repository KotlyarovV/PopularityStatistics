using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class ArchimedeanSpiral : ISpiral
    {
        /*
         * Realization of http://hijos.ru/2011/03/09/arximedova-spiral/ formulas
        */

        private readonly double spiralRadius;
        private Point center;
        private readonly double angleStep;
        private double spiralAngle;

        public ArchimedeanSpiral(double angleStep = 0.01, double spiralRadius = 1)
        {
            this.angleStep = angleStep;
            this.spiralRadius = spiralRadius;
        }

        public PointF GetPoint()
        {
            spiralAngle += angleStep;
            
            var x = (float) (center.X + spiralRadius * spiralAngle * Math.Cos(spiralAngle));
            var y = (float) (center.Y + spiralRadius * spiralAngle * Math.Sin(spiralAngle));

            return new PointF(x, y);
        }

        public void PrepareSpiral(Point center)
        {
            spiralAngle = 0;
            this.center = center;
        }
    }
}

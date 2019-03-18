using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private List<Rectangle> rectangles;
        private readonly ISpiral spiral;
        
        public CircularCloudLayouter(ISpiral spiral)
        {
            this.spiral = spiral;       
        }

        private Point BalancePoint(PointF point) =>
            new Point((int)Math.Floor(point.X), (int)Math.Ceiling(point.Y));

        public void PrepareLayouter(Point center)
        {
            spiral.PrepareSpiral(center);
            rectangles = new List<Rectangle>();
        }

        private Point ChoosePoint()
        {
            Point point;
            do
            {
                var pointOnSpiral = spiral.GetPoint();
                point = BalancePoint(pointOnSpiral);
            }
            while (rectangles.ContainPoint(point));
            return point;
        }

        private Rectangle ChooseRectangle(Size size, Point point)
        {
            var rectangle = new Rectangle(point, size);
            while (rectangles.IntersectRectangle(rectangle))
            {
                point = ChoosePoint();
                rectangle = new Rectangle(point, size);
            }
            return rectangle;
        }
        
        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0) 
                throw new ArgumentException("Size have to be positive non zero number!");

            var point = ChoosePoint();
            var rectangle = ChooseRectangle(rectangleSize, point);
            rectangles.Add(rectangle);
            return rectangle;
        }        
    }
}

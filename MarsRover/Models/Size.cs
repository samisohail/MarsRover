using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace MarsRover.Models
{
    public class GridSize
    {
        public int Width { get; }
        public int Height { get; }
        public GridSize(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}

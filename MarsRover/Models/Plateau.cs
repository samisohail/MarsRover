using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MarsRover.Models
{
    public interface IPlateau
    {
        GridSize Size { get; }
        void DefineSurface(int width, int height);
    }
    public class Plateau : IPlateau
    {
        public GridSize Size { get; private set; }
      
        public void DefineSurface(int width, int height)
        {
            Size = new GridSize(width, height);
        }
    }
}

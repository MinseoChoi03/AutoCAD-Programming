using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AutoCAD_14
{
    class A4TitleBlock : TBlock
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public A4TitleBlock()
        {
            Height = 210;
            Width = 297;
        }
    }
}

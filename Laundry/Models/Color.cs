using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lavanderia.Models
{
    public class Color
    {
         public int idColor { get; set; }
        public string nombreColor { get; set; }
        public string valorColor { get; set; }
        
                
        public  Color() { }

        public Color(int idcolor, string nombrecolor, string valorcolor)
        {
            this.idColor = idcolor;
            this.nombreColor= nombrecolor;
            this.valorColor=valorcolor;
            

        }
    }
}

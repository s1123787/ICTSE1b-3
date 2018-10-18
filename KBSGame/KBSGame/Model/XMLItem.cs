using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Xml.Serialization;

namespace KBSGame.Model
{
    public class XMLItem
    {
        
        //List filled with Obstakels from XML
        public List<XMLObstakel> XMLItems { get; set; }

        public XMLItem(){}
    }
}

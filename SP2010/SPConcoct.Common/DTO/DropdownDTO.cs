using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPConcoct.Common
{
    [Serializable()]
    public class DropDownDTO
    {
        public object Id { get; set; }
        public object Data { get; set; }
        public const string TextFieldColumnName = "Data";
        public const string ValueFieldColumnName = "Id";


        //Default Constructor        
        public DropDownDTO()
        {

        }

        public DropDownDTO(object id, object data)
        {
            this.Id = id;
            this.Data = data;
        }
    }
}

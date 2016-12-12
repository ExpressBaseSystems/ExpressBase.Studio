using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressBase.Studio
{
    public class Form
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Bytea { get; set; }

        public Form() { }
        public Form(int id, string name, byte[] bytea)
        {
            Id = id;
            this.Name = name;
            this.Bytea = bytea;
        }
    }

    public class FormResponse
    {
        public List<Form> Forms { get; set; }
    }
}

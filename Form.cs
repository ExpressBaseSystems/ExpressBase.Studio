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
        public List<Form> Data { get; set; }
    }

    public class View
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Sql { get; set; }

        public View() { }
        public View(int id, string name, string sql)
        {
            Id = id;
            this.Name = name;
            this.Sql = sql;
        }
    }

    //public class ViewResponse
    //{
    //    public RowColletion Data { get; set; }
    //}
}

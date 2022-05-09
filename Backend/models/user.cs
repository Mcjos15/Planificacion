using System;
namespace Backend.models
{
    public class user
    {
        private int id { get; set; }
        private string name { get; set; }
        private string correo { get; set; }
        public user(int id, string name, string correo)
        {
            this.id = id;
            this.name = name;
            this.correo = correo;
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace FcNet.DataHelper.Demo.Entity
{
    public class Persons
    {
        [Key]
        public int id;
        public string name;
    }
}

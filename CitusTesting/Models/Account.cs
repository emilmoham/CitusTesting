using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitusTesting.Models
{
    public class Account
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Number { get; set; }

        public bool Type { get; set; }

        public int FacilityId { get; set; }
    }
}

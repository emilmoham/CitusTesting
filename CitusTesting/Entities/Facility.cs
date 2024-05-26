namespace CitusTesting.Entities
{
    public class Facility
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Facility(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

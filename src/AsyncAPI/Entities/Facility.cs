namespace AsyncAPI.Entities
{
    public class Facility
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Facility(string name)
        {
            Name = name;
        }
    }
}

namespace AsyncAPI.Models.Args
{
    public class CreateFacility
    {
        public string Name { get; set; }

        public CreateFacility(string name)
        {
            Name = name;
        }
    }
}
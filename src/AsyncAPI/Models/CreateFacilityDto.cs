namespace AsyncAPI.Models
{
    public class CreateFacilityDto
    {
        public string Name { get; set; }

        public CreateFacilityDto(string name)
        {
            Name = name;
        }
    }
}
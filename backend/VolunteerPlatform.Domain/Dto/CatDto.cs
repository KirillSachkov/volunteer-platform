namespace VolunteerPlatform.Domain.Dto;

public class CatDto
{
    public Guid Id { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public int Years { get; set; }
    public int Months { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AnimalAttitude { get; set; } = string.Empty;
    public string PeopleAttitude { get; set; } = string.Empty;
    public bool? Vaccine { get; set; }
    public string Color { get; set; } = string.Empty;
    public string Place { get; set; } = string.Empty;
    public string Health { get; set; } = string.Empty;
}
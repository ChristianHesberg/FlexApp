namespace Application.DTOs;

public class AddEmployeeDTO
{
    public string Name { get; set; }
}

public class EditEmployeeDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
}
using InternHub.Model;


namespace InternHub.WebApi.Models
{
    public class CompanyView
    {
        public CompanyView(Company company)
        {
            Name = company.Name;
            Website = company.Website;
            Address = company.Address;
            Id = company.Id;
        }
        public CompanyView() { }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public string Id { get; set; }
        
    }
}
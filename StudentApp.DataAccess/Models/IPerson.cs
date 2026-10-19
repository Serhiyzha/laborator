using System.Text.Json.Serialization;
namespace StudentApp.DataAccess.Models

{
    [JsonDerivedType(typeof(Student), typeDiscriminator: "student")]
    [JsonDerivedType(typeof(Joiner), typeDiscriminator: "joiner")]
    [JsonDerivedType(typeof(Photographer), typeDiscriminator: "photographer")]
    public interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Gender { get; set; }
        string City { get; set; }
        void BigNumbers();
    }
}
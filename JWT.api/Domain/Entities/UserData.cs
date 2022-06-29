namespace JWT.api.Domain.Entities
{
    public class UserData
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string EMail { get; set; }
        public string Role { get; set; }  //Deberíamos tener un enlace IdRole a tabla de roles porque puede tener más de uno

        //...... Otros datos
    }
}

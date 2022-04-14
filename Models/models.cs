namespace server.Models
{
     public class BudgetData{
       public int Id {get; set;}
       public int UserId { get; set;}
       public string Description {get; set;} = string.Empty;
       public int Amount {get; set;}
       public string? Type {get; set;}
    }

    public class Users
    {
        public int? Id {get; set;}
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

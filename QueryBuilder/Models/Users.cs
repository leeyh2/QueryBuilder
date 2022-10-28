using System;


namespace QueryBuilder
{
    public class Users:IClassModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserAddresss { get; set; }
        public string OtherUserDetails { get; set; }
        public string AmountOfFine { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
       
    }
}


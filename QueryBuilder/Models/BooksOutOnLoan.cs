    using System;
namespace QueryBuilder
{
    public class BooksOutOnLoan : IClassModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string DateIssued { get; set; }
        public string DueDate { get; set; }
        public string DateReturned { get; set; }


    }
}


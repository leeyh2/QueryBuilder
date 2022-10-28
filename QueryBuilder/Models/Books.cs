using System;
namespace QueryBuilder
{
    public class Books:IClassModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string DateOfPublication { get; set; }


    }
}


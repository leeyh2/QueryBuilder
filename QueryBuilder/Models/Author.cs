using System;
namespace QueryBuilder
{
    public class Author : IClassModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }

        public Author() { }

        public Author(int id, string firstName, string surname)
        {
            Id = id;
            FirstName = firstName;
            Surname = surname;
        }

        public int CompareTo(Author? other)
        {
            return string.Compare(this.Surname, other.Surname);
        }

        public override string ToString()
        {
            return $"{Surname},{FirstName}";
        }
    }
}


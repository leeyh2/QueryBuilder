using System;
using System.Reflection;
using System.Text;
using Microsoft.Data.Sqlite;
using QueryBuilder;

internal class Program
{
    private static void Main(string[] args)
    {
        var database = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString() + "/Data/LibraryDataBase.db";

        List<Author> authors;

        using (var qb = new QueryBuilder.QueryBuilder(database))
        {
           
            var sk = new Author(90989, "Gabbin", "Frisby");
            //var dk = new Author(24, "Sho", "Khan");
            qb.Create<Author>(sk);
            qb.Read<Author>(19);
            qb.Update<Author>(sk ,19);
            authors = qb.ReadAll<Author>();
            qb.Delete<Author>(sk);
        }

        authors.Sort();
        foreach(Author author in authors)
        {
            Console.WriteLine(author);
        }
        Console.WriteLine("\n\n");
    }
}
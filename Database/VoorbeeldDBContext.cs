using Linq.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Database
{
    public class VoorbeeldDBContext : DbContext
    {
        private readonly string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=VoorbeeldDBLinq;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        public DbSet<Voorbeeld> Voorbeelden { get; set; }
        public DbSet<Uitwerking> Uitwerkingen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var random = new Random(42); // Fixed seed for reproducible data
            var roles = Enum.GetValues<Role>();

            // Example templates
            var exampleTemplates = new[]
            {
                ("LINQ Select Query", "Transform a collection of numbers using Select operator"),
                ("LINQ Where Filter", "Filter a list of products based on price criteria"),
                ("LINQ OrderBy Sorting", "Sort employees by their salary in ascending order"),
                ("LINQ GroupBy Operation", "Group customers by their country of residence"),
                ("LINQ Join Tables", "Combine orders with customer information using Join"),
                ("LINQ Aggregate Sum", "Calculate the total revenue from all sales"),
                ("LINQ Any Condition", "Check if any student passed the exam"),
                ("LINQ All Validation", "Verify that all products are in stock"),
                ("LINQ First Element", "Get the first order placed by a customer"),
                ("LINQ Skip and Take", "Implement pagination for a product list"),
                ("Entity Framework Add", "Insert a new customer into the database"),
                ("Entity Framework Update", "Modify existing product information"),
                ("Entity Framework Delete", "Remove an obsolete record from inventory"),
                ("Entity Framework Include", "Load related entities using eager loading"),
                ("Async Query Pattern", "Execute asynchronous database queries efficiently"),
                ("Complex Where Clause", "Filter data using multiple AND/OR conditions"),
                ("LINQ SelectMany Flatten", "Flatten nested collections into a single sequence"),
                ("LINQ Distinct Values", "Remove duplicate entries from a result set"),
                ("LINQ Count Records", "Count the number of active users in the system"),
                ("LINQ Average Calculation", "Calculate average product rating from reviews"),
                ("LINQ Max and Min", "Find the highest and lowest temperatures recorded"),
                ("LINQ Contains Search", "Search for specific items in a collection"),
                ("LINQ Except Difference", "Find elements that exist in one set but not another"),
                ("LINQ Union Combine", "Merge two collections removing duplicates"),
                ("LINQ Intersect Common", "Find common elements between two datasets"),
                ("LINQ ThenBy Sorting", "Apply secondary sorting criteria to results"),
                ("LINQ Reverse Order", "Reverse the order of elements in a sequence"),
                ("LINQ Single Record", "Retrieve exactly one record matching criteria"),
                ("LINQ DefaultIfEmpty", "Handle empty result sets with default values"),
                ("LINQ OfType Filtering", "Filter elements by their specific type"),
                ("LINQ Cast Conversion", "Convert collection elements to a specific type"),
                ("LINQ Zip Combine", "Combine two sequences element by element"),
                ("LINQ Range Generation", "Generate a sequence of numbers within a range"),
                ("LINQ Repeat Elements", "Create a sequence with repeated values"),
                ("LINQ Empty Collection", "Create an empty enumerable of a type"),
                ("Navigation Properties", "Access related entities through navigation"),
                ("Lazy Loading Setup", "Configure lazy loading for related data"),
                ("Change Tracking", "Track entity state changes in DbContext"),
                ("Transaction Handling", "Manage database transactions with SaveChanges"),
                ("Bulk Insert Operation", "Insert multiple records efficiently"),
                ("Query Performance", "Optimize LINQ queries for better performance"),
                ("Index Usage", "Utilize database indexes in queries"),
                ("Compiled Queries", "Pre-compile frequently used queries"),
                ("Projection Mapping", "Project query results to DTOs or ViewModels"),
                ("String Operations", "Perform string filtering and manipulation"),
                ("Date Filtering", "Filter records based on date ranges"),
                ("Null Handling", "Handle nullable properties in LINQ queries"),
                ("Conditional Logic", "Implement conditional expressions in queries"),
                ("Subquery Pattern", "Use subqueries within main queries"),
                ("Left Join Implementation", "Perform left outer joins between tables"),
                ("Group Join Pattern", "Combine grouping with join operations"),
                ("Let Clause Usage", "Introduce range variables in query syntax"),
                ("Into Continuation", "Continue queries after grouping or joining"),
                ("Query Syntax vs Method", "Compare query and method syntax approaches"),
                ("Deferred Execution", "Understand lazy evaluation of LINQ queries"),
                ("Immediate Execution", "Force immediate query execution with ToList"),
                ("AsNoTracking Query", "Execute read-only queries without tracking"),
                ("Find by Primary Key", "Retrieve entity by its primary key value"),
                ("Local Cache Access", "Query the local DbContext cache"),
                ("Attach Entity", "Attach existing entity to context for updates"),
                ("Entry Metadata", "Access entity metadata and state information"),
                ("Complex Type Mapping", "Map complex types in entity models"),
                ("Value Conversion", "Convert property values during persistence"),
                ("Shadow Properties", "Use properties not in entity class definition"),
                ("Backing Fields", "Configure backing fields for properties"),
                ("Concurrency Tokens", "Handle concurrent updates with tokens"),
                ("Soft Delete Pattern", "Implement logical deletion of records"),
                ("Global Query Filters", "Apply automatic filters to all queries"),
                ("Table Splitting", "Map multiple entities to single table"),
                ("Entity Splitting", "Map single entity across multiple tables"),
                ("Inheritance TPH", "Table-per-hierarchy inheritance mapping"),
                ("Inheritance TPT", "Table-per-type inheritance strategy"),
                ("Inheritance TPC", "Table-per-concrete-class mapping"),
                ("Many-to-Many Relation", "Configure many-to-many relationships"),
                ("One-to-One Relation", "Set up one-to-one entity relationships"),
                ("One-to-Many Relation", "Configure one-to-many associations"),
                ("Self Referencing", "Create self-referencing entity relationships"),
                ("Cascade Delete", "Configure cascading delete behavior"),
                ("Required Navigation", "Make navigation properties required"),
                ("Optional Navigation", "Configure optional relationships"),
                ("Composite Primary Key", "Define composite keys for entities"),
                ("Alternate Keys", "Configure alternate unique keys"),
                ("Index Configuration", "Create database indexes for performance"),
                ("Unique Constraints", "Add unique constraints to properties"),
                ("Check Constraints", "Define check constraints on tables"),
                ("Default Values", "Set default values for properties"),
                ("Computed Columns", "Configure computed column expressions"),
                ("Stored Procedure Call", "Execute stored procedures from EF Core"),
                ("Raw SQL Query", "Execute raw SQL queries safely"),
                ("SQL Interpolation", "Use interpolated strings in SQL queries"),
                ("Database Functions", "Call database functions in LINQ"),
                ("User-Defined Functions", "Map and use user-defined functions"),
                ("View Mapping", "Map entities to database views"),
                ("Temporal Tables", "Work with SQL Server temporal tables"),
                ("Owned Entity Types", "Configure owned entity types"),
                ("Keyless Entity Types", "Define entities without primary keys"),
                ("Query Tags", "Add tags to queries for logging"),
                ("Split Queries", "Split complex queries for performance"),
                ("Batch Updates", "Perform batch update operations"),
                ("Batch Deletes", "Execute batch delete operations")
            };

            var voorbeelden = new List<Voorbeeld>();
            for (int i = 1; i <= 100; i++)
            {
                var template = exampleTemplates[i - 1];
                voorbeelden.Add(new Voorbeeld
                {
                    Id = i,
                    Name = template.Item1,
                    Description = template.Item2,
                    Count = random.Next(1, 100),
                    Role = roles.GetValue(random.Next(roles.Length)) as Role? ?? Role.User
                });
            }

            modelBuilder.Entity<Voorbeeld>().HasData(voorbeelden);

            // Owner names for Uitwerkingen
            var owners = new[]
            {
                "John Smith", "Emma Johnson", "Michael Brown", "Sophia Davis", "James Wilson",
                "Olivia Martinez", "William Anderson", "Ava Taylor", "Robert Thomas", "Isabella Moore",
                "David Jackson", "Mia White", "Richard Harris", "Charlotte Martin", "Joseph Thompson",
                "Amelia Garcia", "Thomas Robinson", "Harper Clark", "Charles Rodriguez", "Evelyn Lewis",
                "Daniel Lee", "Abigail Walker", "Matthew Hall", "Emily Allen", "Christopher Young",
                "Elizabeth King", "Andrew Wright", "Sofia Lopez", "Mark Hill", "Avery Scott",
                "Joshua Green", "Ella Adams", "Ryan Baker", "Scarlett Nelson", "Brian Carter",
                "Madison Mitchell", "Kevin Perez", "Luna Roberts", "Steven Turner", "Grace Phillips"
            };

            var uitwerkingen = new List<Uitwerking>();
            for (int i = 1; i <= 200; i++)
            {
                uitwerkingen.Add(new Uitwerking
                {
                    Id = i,
                    Owner = owners[random.Next(owners.Length)],
                    Tries = random.Next(1, 20),
                    VoorbeeldId = random.Next(1, 101) // Random VoorbeeldId between 1-100
                });
            }

            modelBuilder.Entity<Uitwerking>().HasData(uitwerkingen);
        }
    }
}

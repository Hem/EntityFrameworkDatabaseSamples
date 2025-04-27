using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleAPI.EFRepository;

namespace EFRepository.Tests
{
    [TestClass]
    public sealed class EntityTest
    {

        public static DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer("Server=localhost;Database=DbDemo;User Id=sa;Password=1201H3llo***;TrustServerCertificate=true")
            .Options;


        //[TestMethod]
        //public void TestCreateGroup()
        //{   
        //    // Create a new context instance
        //    using (var context = new AppDbContext(options))
        //    {
        //        // Add a new AppGroup entity
        //        var group = new AppGroup { Name = "Test Group", AdminId = 1 };
        //        context.AppGroups.Add(group);
        //        context.SaveChanges();
        //        // Retrieve the entity from the database
        //        var retrievedGroup = context.AppGroups.FirstOrDefault(g => g.Name == "Test Group");
        //        Assert.IsNotNull(retrievedGroup);
        //    }
        //}


        [TestMethod]
        public void TestSelectUser()
        {
            // Create a new context instance
            using (var context = new AppDbContext(options))
            {
                // Retrieve the entity from the database
                var user = context.AppUsers.Include(x => x.AdminOfGroups).FirstOrDefault(g => g.Id == 1);
                Assert.IsNotNull(user);
            }
        }


        [TestMethod]
        public void TestSelectGroup()
        {
            // Create a new context instance
            using (var context = new AppDbContext(options))
            {
                // Retrieve the entity from the database
                var group = context.AppGroups
                                    .Include(e => e.Admin) // Eager Loading
                                    .FirstOrDefault(g => g.Id == 1);

                Assert.IsNotNull(group);

                Assert.IsNotNull(group.Admin);
                Assert.IsNotNull(group.Admin.Id);

            }
        }


        [TestMethod]

        public void TestSelectUserInGroup()
        {
            // Create a new context instance
            using (var context = new AppDbContext(options))
            {
                // Retrieve the entity from the database
                var group = context.AppGroups
                                    .Include(e => e.Users) // Eager Loading
                                    .FirstOrDefault(g => g.Id == 1);

                Assert.IsNotNull(group);

                Assert.IsNotNull(group.Users);
                Assert.IsTrue(group.Users.Count > 0);
            }

        }
    }
}

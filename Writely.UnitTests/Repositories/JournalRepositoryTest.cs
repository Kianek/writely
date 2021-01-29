using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Writely.Models;
using Writely.Repositories;
using Xunit;

namespace Writely.UnitTests.Repositories
{
    public class JournalRepositoryTest : RepositoryTestBase
    {
        [Fact]
        public async Task GetById_JournalFound_ReturnsJournal()
        {
            // Arrange
            await PrepareDatabase();
            var journal = Helpers.GetJournal();
            Context.Journals?.Add(journal);
            await Context.SaveChangesAsync();
            var repo = GetJournalRepo();

            // Act
            var result = await repo.GetById(journal.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(journal.Id);
        }

        [Fact]
        public async Task GetById_JournalNotFound_ReturnsNull()
        {
            // Arrange
            await PrepareDatabase();
            var repo = GetJournalRepo();

            // Act
            var result = await repo.GetById(4L);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAll_JournalsFound_NoLimit_ReturnsAllJournals()
        {
            // Arrange
            await PrepareDatabase();
            var journals = Helpers.GetJournals(5);
            journals[3].UserId = "Blah McBlahston";
            journals[4].UserId = "Blah McBlahston";
            Context.Journals.AddRange(journals);
            await Context.SaveChangesAsync();
            var repo = GetJournalRepo();
            // var repo = GetJournalRepo(Context);

            // Act
            var result = await repo.GetAll();

            // Assert
            result.Count().Should().Be(3);
        }

        [Fact]
        public async Task GetAll_JournalsFound_Limit_ReturnsLimitedJournals()
        {
            // Arrange
            await PrepareDatabase();
            var journals = Helpers.GetJournals(5);
            Context.Journals.AddRange(journals);
            await Context.SaveChangesAsync();
            var repo = GetJournalRepo();
            // var repo = GetJournalRepo(Context);
            
            // Act
            var result = await repo.GetAll(limit: 2);

            // Assert
            result.Count().Should().Be(2);
        }

        [Fact]
        public async Task GetAll_JournalsFound_FilterByCatsInTitle_ReturnsFilteredJournals()
        {
            // Arrange
            await PrepareDatabase();
            var journals = Helpers.GetJournals(5);
            journals[1].Title = "A Cat Named the Next Journal";
            journals[2].Title = "aoijeafj aoiewjf awojfi302901 - cat";
            Context.Journals.AddRange(journals);
            await Context.SaveChangesAsync();
            var repo = GetJournalRepo();

            // Act
            var result = await repo.GetAll(filter: j => j.Title.Contains("Cat"));

            // Assert
            result.Count().Should().Be(1);
        }

        [Fact]
        public async Task GetAll_JournalsFound_OrderByTitleAscending_ReturnsOrderedJournals()
        {
            // Arrange
            await PrepareDatabase();
            var journals = Helpers.GetJournals(3);
            var fancyTitle = "My Fancy Title";
            var xyz = "XYZ";
            var alphabets = "Alphabets and Stuff";
            journals[0].Title = fancyTitle;
            journals[1].Title = xyz;
            journals[2].Title = alphabets;
            Context.Journals.AddRange(journals);
            await Context.SaveChangesAsync();
            var repo = GetJournalRepo();

            // Act
            var result = await repo.GetAll(order: "asc") as List<Journal>;

            // Assert
            result[0].Title.Should().Be(alphabets);
            result[1].Title.Should().Be(fancyTitle);
            result[2].Title.Should().Be(xyz);
        }

        [Fact]
        public async Task Find_WhereTitleContainsDog_ReturnsJournal()
        {
            // Arrange
            await PrepareDatabase();
            var journals = Helpers.GetJournals(3);
            journals[1].Title = "A Dog Wrote This";
            Context.Journals.AddRange(journals);
            await Context.SaveChangesAsync();
            var repo = GetJournalRepo();

            // Act
            var result = await repo.Find(j => j.Title.Contains("Dog"));

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be(journals[1].Title);
        }

        [Fact]
        public async Task Find_NoJournalFound_ReturnsNull()
        {
            // Arrange
            await PrepareDatabase();
            var repo = GetJournalRepo();

            // Act
            var result = await repo.Find(j => j.Title == "I don't exist!");

            // Assert
            result.Should().BeNull();
        }

        private JournalRepository GetJournalRepo() => new (Context, "UserId");
    }
}
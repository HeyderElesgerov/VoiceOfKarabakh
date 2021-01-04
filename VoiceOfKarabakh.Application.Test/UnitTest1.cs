using NUnit.Framework;
using VoiceOfKarabakh.Application.Interfaces.Category;
using VoiceOfKarabakh.Application.Services.Category;
using VoiceOfKarabakh.Infrastructure.Repository.Category;

namespace VoiceOfKarabakh.Application.Test
{
    public class Tests
    {
        private ICategoryService _categoryService;

        public Tests(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
using MiniStore.Dto;
using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStoreTest.Helper
{
    public static class CategoryHelper
    {


        public static Category CreateCategoryForTest()
        {
            return new Category()
            {
                CategoryId = 1,
                CategoryName = "Téléphone"
            };
        }



        public static CategoryDto CreateCategoryForAddMethodTest()
        {
            return new CategoryDto()
            {
                CategoryId = 1,
                CategoryName = "Téléphone"
            };
        }

        public static Category CreateCategoryForAddMethodServiceTest()
        {
            return new Category()
            {
                CategoryId = 1,
                CategoryName = "Téléphone"
            };
        }
        public static CategoryDto CreateCategoryDtoForTest()
        {
            return new CategoryDto()
            {
                CategoryId = 1,
                CategoryName = "Téléphone"
            };
        }
        public static CategoryDto UpdateCategoryForMethodTest()
        {
            return new CategoryDto()
            {
                CategoryId = 2,
                CategoryName = "Caméra"
            };
        }

        public static Category UpdateCategoryForUpdateServiceMethodTest()
        {
            return new Category()
            {
                CategoryId = 2,
                CategoryName = "Tél"
            };
        }

        public static CategoryDto CategoryTransfertForUpdateMethodTest()
        {
            return new CategoryDto()
            {
                CategoryId = 2,
                CategoryName = "Caméra"
            };
        }

        public static Category CategoryTransfertForUpdateServiceMethodTest()
        {
            return new Category()
            {
                CategoryId = 2,
                CategoryName = "Caméra"
            };
        }



        public static IEnumerable<Category> CreateCategoryListForTest()
        {

            return new List<Category>()
            {
                new Category()
                {

                    CategoryId = 1,
                    CategoryName = "Téléphone"

                }

            };
        }



        public static CategoryDto CreateCategoryDtoForUpdateTest()
        {
            return new CategoryDto()
            {
                CategoryId = 2,
                CategoryName = "Tél"
            };
        }
        public static IEnumerable<CategoryDto> CreateCategoryDtoListForTest()
        {

            return new List<CategoryDto>()
            {
                new CategoryDto()
                {

                    CategoryId = 1,
                    CategoryName = "Téléphone"

                }

            };
        }
    }
}

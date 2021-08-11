using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeAppAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : Controller
    {
        public RecipeDataContext DbContext;
        public RecipeController(RecipeDataContext context)
        {
            DbContext = context;
        }

        [Route("GetAllRecipe")]
        [HttpGet]
        public async Task<List<Recipe>> GetAllRecipe()
        {
            List<Recipe> response = new List<Recipe>();

            try
            {
                var list = DbContext.Set<Recipe>().ToListAsync().Result;
                foreach (var item in list)
                {
                    response.Add(item);
                }
            }
            catch (Exception ex)
            {
                response = new List<Recipe>();
            }

            return response;
        }

        [Route("CreateRecipe")]
        [HttpPost]
        public async Task<Recipe> CreateRecipe(Recipe model)
        {
            Recipe response = new Recipe();
            try
            {
                DbContext.Set<Recipe>().Add(model);
                int id = DbContext.SaveChangesAsync().Result;
                response = DbContext.Set<Recipe>().FindAsync(id).Result;
            }
            catch (Exception ex)
            {
                response = new Recipe();
            }
            return response;
        }

        [Route("GetRecipe")]
        [HttpGet]
        public async Task<Recipe> GetRecipe(int recipeId = 0)
        {
            Recipe response = new Recipe();
            try
            {
                response = DbContext.Set<Recipe>().FindAsync(recipeId).Result;
            }
            catch (Exception ex)
            {
                response = new Recipe();
            }
            return response;
        }

        [Route("UpdateRecipe")]
        [HttpPut]
        public async Task<bool> UpdateRecipe(Recipe model)
        {
            bool response = false;
            try
            {
                DbContext.Set<Recipe>().Update(model);
                await DbContext.SaveChangesAsync();
                response = true;
            }
            catch (Exception ex)
            {
                response = false;
            }
            return response;
        }

        [Route("DeleteRecipe")]
        [HttpDelete]
        public async Task<bool> DeleteRecipe(int recipeId = 0)
        {
            bool response = false;
            try
            {
                var entity = DbContext.Set<Recipe>().FindAsync(recipeId).Result;
                DbContext.Set<Recipe>().Remove(entity);
                await DbContext.SaveChangesAsync();
                response = true;
            }
            catch (Exception)
            {
                response = false;
            }
            return response;
        }
    }
}

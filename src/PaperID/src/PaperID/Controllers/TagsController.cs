using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PaperID.Models.TagManagerViewModels;
using PaperID.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaperID.DataModels;

namespace PaperID.Controllers
{
    [Produces("application/json")]
    [Route("api/Tags")]
    public class TagsController : Controller
    {
        private static readonly List<TagViewModel> tags = new List<TagViewModel>()
                {
                    new TagViewModel()
                    {
                        ID = "1",
                        Title = "Cool Shirt",
                        Price = (decimal)100.00,
                        SalePrice = (decimal)95.00,
                        ImageUrl = "http://nord.imgix.net/Zoom/19/_13423519.jpg?fit=fill&bg=FFF&fm=jpg&w=860&h=1318&dpr=1.5&q=52.5"
                    },
                    new TagViewModel()
                    {
                        ID = "2",
                        Title = "Cooler Shirt",
                        Price = (decimal)150.00,
                        ImageUrl = "http://nord.imgix.net/Zoom/12/_12807852.jpg?fit=fill&bg=FFF&fm=jpg&w=860&h=1318&dpr=1.5&q=52.5"
                    }
                };

        private readonly ShopSightContext shopSightContext;

        public TagsController(ShopSightContext shopSightContext)
        {
            this.shopSightContext = shopSightContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await this.shopSightContext.TaggedItems.ToListAsync();
            var tags = items.Select(item => ToTagViewModel(item));
            return new ObjectResult(tags);
        }

        [HttpGet("{id}", Name = "GetTag")]
        public async Task<IActionResult> GetById(string id)
        {
            var item = await this.shopSightContext.TaggedItems.FindAsync(id);
            if (item == null)
            {
                return this.NotFound();
            }

            var tag = ToTagViewModel(item);
            return new ObjectResult(tag);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] TagViewModel model)
        {
            if (!this.ModelState.IsValid || id != model.ID)
            {
                return this.BadRequest();
            }

            var item = await this.shopSightContext.TaggedItems.FindAsync(id);
            if (item == null)
            {
                return this.NotFound();
            }

            item.ImageUrl = model.ImageUrl;
            item.Manufacturer = model.Manufacturer;
            item.Price = model.Price;
            item.SalePrice = model.SalePrice;
            item.Title = model.Title;

            try
            {
                this.shopSightContext.TaggedItems.Update(item);
                await this.shopSightContext.SaveChangesAsync();
                return this.NoContent();
            }
            catch (DbUpdateException)
            {
                this.StatusCode(500);
            }

            return this.NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TagViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var item = new TaggedItem()
            {
                ID = model.ID,
                ImageUrl = model.ImageUrl,
                Manufacturer = model.Manufacturer,
                Price = model.Price,
                SalePrice = model.SalePrice,
                Title = model.Title
            };

            try
            {
                this.shopSightContext.TaggedItems.Add(item);
                await this.shopSightContext.SaveChangesAsync();
                return this.CreatedAtRoute("GetTag", new { id = model.ID }, model);
            } catch (DbUpdateException)
            {
                return this.StatusCode(500);
            }            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var item = await this.shopSightContext.TaggedItems.FindAsync(id);
            if (item == null)
            {
                return this.NotFound();
            }

            try
            {
                this.shopSightContext.TaggedItems.Remove(item);
                await this.shopSightContext.SaveChangesAsync();
                return this.NoContent();
            }
            catch (DbUpdateException /* ex */)
            {
                return this.StatusCode(500);
            }
        }

        private static TagViewModel ToTagViewModel(TaggedItem taggedItem)
        {
            return new TagViewModel()
            {
                ID = taggedItem.ID,
                ImageUrl = taggedItem.ImageUrl,
                Manufacturer = taggedItem.Manufacturer,
                Price = taggedItem.Price,
                SalePrice = taggedItem.SalePrice,
                Title = taggedItem.Title
            };
        }
    }
}
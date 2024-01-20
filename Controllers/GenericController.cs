using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDb.BLL;
using MongoDb.Common;
using MongoDB.Bson;

namespace CrudMongoDb.Controllers
{
    //kod tekrarını önlemek için generic controller kullanıldı.
    [Route("api/[controller]")]
    [ApiController]
    public abstract class GenericController<T> : ControllerBase where T : BaseEntity
    {
        private readonly GenericService<T> _service;
        public GenericController(GenericService<T> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Get() => Ok(await _service.GetAllAsync());

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _service.GetAsync(id);

            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult> Post(T entity)
        {
            await _service.InsertAsync(entity);

            return CreatedAtAction(nameof(Get), new { entity.Id }, entity);//201
        }

        [HttpPut]
        public async Task<ActionResult> Update(T entity)
        {
            var item = await _service.GetAsync(entity.Id);

            if (item is null)
            {
                return NotFound();
            }

            await _service.UpdateAsync(entity);

            return NoContent();
        }
        //İd 24 karakter olmalı yoksa:Value must be at least 24 characters

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {
            var item = await _service.GetAsync(id);

            if (item is null)
            {
                return NotFound();//404
            }
            try
            {
                await _service.DeleteAsync(id);
            }
            catch (Exception ex)
            {

            }
            return NoContent();//204
        }
    }
}

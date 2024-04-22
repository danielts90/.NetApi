using MarketPlace.Helpers;
using MarketPlaceDomain.Dto;
using MarketPlaceDomain.Entities;
using MarketPlaceDomain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public abstract class BaseController<TDto, TEntity> : Controller 
        where TDto : DtoBase
        where TEntity : EntityBase
    {
        private readonly IBaseService<TDto, TEntity> _service;

        protected BaseController(IBaseService<TDto, TEntity> service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<CustomResponse<IEnumerable<TDto>>> GetAll()
        {
            var itens = _service.GetAll();
            if (itens.Any())
                return Ok(new CustomResponse<IEnumerable<TDto>>(true, "", itens));
            else
                return NotFound(new CustomResponse<IEnumerable<TDto>>(false, "No registers founded.", itens));
        }

        [HttpGet("{id}")]
        public ActionResult<CustomResponse<TDto>> GetById(int id)
        {
            var item = _service.GetById(id);

            if (item == null)
            {
                return NotFound(new CustomResponse<TDto>(false,$"No registers founded for id {id}", item));
            }

            return Ok(new CustomResponse<TDto>(true, "", item));
        }

        [HttpPost]
        public ActionResult<TDto> Post(TDto dto)
        {
            try
            {
                var itemInserted = _service.Insert(dto);
                dto = _service.GetById(itemInserted);
                return Ok(new CustomResponse<TDto>(true, $"Item inserted with id {itemInserted}", dto));
            }
            catch (Exception ex)
            {
                return BadRequest(new CustomResponse<TDto>(false, $"An error ocurred while execute Post action,{ex.Message}", dto));
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, TDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest(new CustomResponse<TDto>(false, $"Invalid object Id: {id}", dto));
            }

            try
            {
                _service.Update(dto);
                return Ok(new CustomResponse<TDto>(true, "Object updated", dto));
            }
            catch (Exception ex)
            {
                return BadRequest(new CustomResponse<TDto>(false, $"An error ocurred while execute Put action, {ex.Message}", dto));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existentItem = _service.GetById(id);

            if (existentItem == null)
            {
                return NotFound(new CustomResponse<IEnumerable<TDto>>(false, $"No registers founded to id {id}", null));
            }

            try
            {
                _service.Delete(id);
                return Ok(new CustomResponse<TDto>(true, "Object deleted", existentItem));
            }
            catch (Exception ex)
            {
                return BadRequest(new CustomResponse<TDto>(false, $"An error ocurred while execute delete action, {ex.Message}", existentItem));
            }
            
        }
    }
}

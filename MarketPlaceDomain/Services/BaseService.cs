using FluentValidation;
using MarketPlaceBusiness.Dto;
using MarketPlaceBusiness.Entities;
using MarketPlaceBusiness.Interfaces;

namespace MarketPlaceBusiness.Services
{
    public class BaseService<TDto, TEntity> : IBaseService<TDto, TEntity> where TDto : DtoBase where TEntity : EntityBase
    {
        private readonly IBaseRepository<TEntity> _repository;

        public BaseService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public void Delete(int id)
        {
            var existentEntity = _repository.GetById(id);
            
            if(existentEntity != null)
                _repository.Delete(id);
            else
                throw new ArgumentException("Entidade não encontrada.");
        }

        public IEnumerable<TDto> GetAll()
        {
            return _repository.GetAll()
                              .Select(entity => (TDto)entity);
        }

        public TDto GetById(int id)
        {
            var entity = _repository.GetById(id);
            return (TDto)entity;
        }

        public int Insert(TDto dto)
        {
            var validations = dto.Validate();
            if (validations.IsValid)
                return _repository.Insert((TEntity)dto);
            else
            {
                string errorMessage = string.Join(Environment.NewLine, validations.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errorMessage);
            }
                
        }

        public void Update(TDto dto)
        {
            _repository.Update((TEntity)dto);
        }
    }
}

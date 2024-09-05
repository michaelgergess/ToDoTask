using AutoMapper;
using ToDoTask.Application.DTOs;
using ToDoTask.Application.Interfaces;
using ToDoTask.Domain.Entities;

namespace ToDoTask.Application.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ToDoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ToDoDto> GetToDoByIdAsync(int id)
        {
            var ToDo = await _unitOfWork.ToDosRepository.GetByIdAsync(id);
            return _mapper.Map<ToDoDto>(ToDo);
        }

        public async Task<IEnumerable<ToDoDto>> GetAllToDosAsync(int pageNumber, int pageSize, int userId)
        {
            var ToDos = await _unitOfWork.ToDosRepository.GetAllAsyncPagination<ToDo>( pageNumber ,  pageSize,userId );
            return _mapper.Map<IEnumerable<ToDoDto>>(ToDos);
        }

        public async Task AddToDoAsync(CreateToDoDto ToDoDto,int userId)
        {
           
            var ToDo = _mapper.Map<ToDo>(ToDoDto);
            ToDo.UserId = userId;
            await _unitOfWork.ToDosRepository.AddAsync(ToDo);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<UpdateToDoDto> UpdateToDoAsync( UpdateToDoDto updateToDoDto, int userId)
        {
            if (updateToDoDto == null)
            {
                return null;
            }

            // Retrieve existing ToDo from the repository
            var existingToDo = await _unitOfWork.ToDosRepository.GetByIdAsync(updateToDoDto.Id);

            if (existingToDo == null || existingToDo.UserId != userId)
            {
                return null; 
            }

            // Update the properties
            existingToDo.Title = updateToDoDto.Title;
            existingToDo.Completed = updateToDoDto.completed;
            // Save changes
            await _unitOfWork.ToDosRepository.UpdateAsync(existingToDo);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UpdateToDoDto>(existingToDo);
        }


        public async Task<ToDoDto> DeleteToDoAsync(int id)
        {
            var ToDo = await _unitOfWork.ToDosRepository.GetByIdAsync(id);

            if (ToDo != null)
            {
                await _unitOfWork.ToDosRepository.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();
                var toDoDto = _mapper.Map<ToDoDto>(ToDo);
                return toDoDto;
            }
            return null;
        }

      
    }
}

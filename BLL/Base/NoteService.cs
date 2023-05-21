using BLL.Interface;
using BLL.Response;
using DAL.Interface;
using Entity;

namespace BLL.Base
{
    public class NoteService : INoteService
    {

        private IUnitOfWork _unitOfWork;
        private INoteRepository _repository;

        public NoteService(INoteRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<GenericResponse<Note>> Delete(int id)
        {
            try
            {
                var entity = await _repository.GetById(id);
                if (entity != null)
                {
                    await _repository.Delete(id);
                    _unitOfWork.Commit();
                    return new GenericResponse<Note>(entity);
                }
                return new GenericResponse<Note>($"No se encontro el registro");
            }
            catch (Exception e)
            {
                _unitOfWork.Dispose();
                return new GenericResponse<Note>($"Error de consulta: error {e.Message}");

            }
        }

        public async Task<GenericResponse<Note>> GetAll(int id)
        {
            try
            {
                IEnumerable<Note> entities = _repository.GetAll().Result.Where(x=>x.IdUser == id);
                if (entities != null)
                {
                    return new GenericResponse<Note>(entities);
                }
                return new GenericResponse<Note>($"No se encotraron registros");
            }
            catch (Exception e) { _unitOfWork.Dispose(); return new GenericResponse<Note>($"Error de consulta: error {e.Message}"); }

        }

        public async Task<GenericResponse<Note>> GetById(int i)
        {
            try
            {
                var Entity = await _repository.GetById(i);
                if (Entity != null)
                {
                    return new GenericResponse<Note>(Entity);
                }
                return new GenericResponse<Note>($"No se encontro registro");
            }
            catch (Exception e) { _unitOfWork.Dispose(); return new GenericResponse<Note>($"Error de consulta: error {e.Message}"); }
        }

        public async Task<GenericResponse<Note>> Insert(Note entity)
        {
            try
            {
                var entityFind = await _repository.GetById(entity.ID);
                if (entityFind == null)
                {
                    var user = _unitOfWork._Context().Users.FirstOrDefault(x=>x.ID == entity.IdUser);
                    if(user != null)
                    {
                        entity.User= user;  
                        await _repository.Insert(entity);
                        _unitOfWork.Commit();
                        return new GenericResponse<Note>(entity);
                    }
                    return new GenericResponse<Note>("Error alasociar las notas");
                }
                return new GenericResponse<Note>($"Este registro ya existe");
            }
            catch (Exception e) { _unitOfWork.Dispose(); return new GenericResponse<Note>($"Error de registro: error {e.Message}"); }
        }

        public async Task<GenericResponse<Note>> Update(Note entity, int id)
        {
            try
            {
                var entityFind = await _repository.GetById(id);
                if (entityFind != null)
                {
                    await _repository.Update(entity);
                    _unitOfWork.Commit();
                    return new GenericResponse<Note>(entity);
                }
                return new GenericResponse<Note>($"Registro no encontrado");
            }
            catch (Exception e) { _unitOfWork.Dispose(); return new GenericResponse<Note>($"Error de registro: error {e.Message}"); }
        }
    }
}

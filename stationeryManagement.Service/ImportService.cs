using Microsoft.EntityFrameworkCore;
using stationeryManagement.Data;
using stationeryManagement.Data.Common.BaseRepository;
using stationeryManagement.Data.Dto;
using stationeryManagement.Data.Model;
using stationeryManagement.Service.Common;
using stationeryManagement.Service.Interface;

namespace stationeryManagement.Service
{
    public class ImportService : EntityService<Import>, IImportService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ImportService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.ImportRepository)
        {
            _unitOfWork = unitOfWork;
        }

       

        public async Task<Import> CreateImport(ImportDto import, Guid id)
        {
            var entity = new Import()
            {
                UserCreateId = id
            };
            var result = await _unitOfWork.ImportRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return result;
        }



        public  async Task<bool> DeleteImport(int ImportId)
        {
            var delete = await _unitOfWork.ImportRepository.GetByIdAsync(ImportId); 
            if(delete != null)
            {
                _unitOfWork.ImportRepository.Delete(delete);
            }
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<IEnumerable<Import>> GetAllImport()
        {
            return await _unitOfWork.ImportRepository.GetAll().ToListAsync();
        }

        public async Task<Import?> GetImportId(int ImportId)
        {
            return await _unitOfWork.ImportRepository.GetByIdAsync(ImportId);
        }

        public async Task<bool> UpdateImport(ImportDto import, Guid id,int Importid)
        {
            var update = new Import() { 
              ImportId=Importid,
              UserCreateId=id
            };
            _unitOfWork.ImportRepository.UpdateAsync(update);


            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}

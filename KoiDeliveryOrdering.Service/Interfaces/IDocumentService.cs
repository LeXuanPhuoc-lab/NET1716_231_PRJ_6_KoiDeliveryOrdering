using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Data.Dtos.Documents;

namespace KoiDeliveryOrdering.Service.Interfaces
{
    public interface IDocumentService
    {
        Task<ServiceResult> GetAll(SearchDocumentQueryDto searchDto);
        Task<ServiceResult> GetById(int id);
        Task<ServiceResult> CreateDocument(DocumentMutationDto dto);
        Task<ServiceResult> UpdateDocument(int id, DocumentMutationDto dto);
        Task<ServiceResult> DeleteDocument(int id);
    }
}
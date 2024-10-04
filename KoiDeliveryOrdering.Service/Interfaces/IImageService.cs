using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Business.Interfaces
{
    public interface IImageService
    {
        Task<string> UploadAsync(IFormFile file);
    }
}

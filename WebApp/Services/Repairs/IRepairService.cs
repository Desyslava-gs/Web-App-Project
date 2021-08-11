using System.Collections.Generic;
using WebApp.Data.Models;
using WebApp.Models.Repairs;

namespace WebApp.Services.Repairs
{
    public interface IRepairService
    {
     
      public IEnumerable<IndexRepairAllViewModel> GetAllRepairsCars(string id);
      public void CreateRepairs(CreateRepairFormModel repairs, string id);
      //public void GetEditRepairs(EditRepairFormModel repair, string id);

      public IEnumerable<RepairTypeViewModel> GetRepairTypes();
      public bool RepairExists(string id);
      public Repair GetRepairId(string id);
      public bool RepairTypesExists(string RepairTypeId);
    }
}

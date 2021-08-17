using System.Collections.Generic;
using System.Linq;
using WebApp.Data.Models;
using WebApp.Models.Repairs;

namespace WebApp.Services.Repairs
{
    public interface IRepairService
    {
     
      public IEnumerable<IndexRepairAllViewModel> AllRepairsForCar(string id);
      public void CreateRepairs(CreateRepairFormModel repairs, string id);
      public DetailsRepairViewModel DetailsRepair(string id);
      public void EditRepairs(string id, EditRepairFormModel repair);

      public DeleteRepairViewModel DeleteRepair(string id);

      public void DeleteConfirmed(string id);

      public IEnumerable<RepairTypeViewModel> GetRepairTypes();
      public bool RepairExists(string id);
      public Repair GetRepairId(string id);
      public bool RepairTypesExists(string rtId);

      public Repair Repair(string id);
      public bool AnyPart(string id);
    }
}

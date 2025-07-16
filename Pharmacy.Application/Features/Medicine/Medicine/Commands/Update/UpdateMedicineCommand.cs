using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Features.Medicine.Medicine.Commands.Create;

namespace Pharmacy.Application.Features.Medicine.Medicine.Commands.Update;

public class UpdateMedicineCommand : CreateMedicineCommand, IBaseUpdateCommand
{
    public Guid Id { get; set; }
}

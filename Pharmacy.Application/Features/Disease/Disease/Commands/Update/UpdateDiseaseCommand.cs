using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Features.Disease.Disease.Commands.Create;

namespace Pharmacy.Application.Features.Disease.Disease.Commands.Update;

public class UpdateDiseaseCommand : CreateDiseaseCommand, IBaseUpdateCommand
{
    public Guid Id { get; set; }
}

namespace ${{values.component_id}}.Application.Models;

public enum CommandResultTypeEnum
{
    Success,
    InvalidInput,
    UnprocessableEntity,
    Conflict,
    NotFound
}

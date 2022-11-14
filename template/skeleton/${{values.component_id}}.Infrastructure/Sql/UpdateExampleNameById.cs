namespace ${{values.component_id}}.Infrastructure.Sql;

public class UpdateExampleNameById
{
    public const string Value = @"UPDATE Example SET Name = @Name WHERE Id = @Id; SELECT @@ROWCOUNT;";
}

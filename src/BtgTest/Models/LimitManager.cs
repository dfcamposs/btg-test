using Amazon.DynamoDBv2.DataModel;

[DynamoDBTable("LimitManager")]
public class LimitManager
{
    [DynamoDBProperty("identifier")]
    public Guid Identifier { get; set; }

    [DynamoDBProperty("documentNumber")]
    public string? DocumentNumber { get; set; }

    [DynamoDBHashKey("accountNumber")]
    public int AccountNumber { get; set; }

    [DynamoDBRangeKey("branch")]
    public int Branch { get; set; }

    [DynamoDBProperty("maxPixLimit")]
    public decimal MaxPixLimit { get; set; }

    [DynamoDBProperty("currentPixLimit")]
    public decimal CurrentPixLimit { get; set; }

    [DynamoDBProperty("createdAt")]
    public DateTime CreatedAt { get; set; }

    [DynamoDBProperty("updatedAt")]
    public DateTime UpdatedAt { get; set; }

}
using System.Net;
using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace BtgTest;

public class LimitManagerRepository : ILimitManagerRepository
{
  private readonly IAmazonDynamoDB _dynamoDbClient;
  private readonly string tableName = "LimitManager";

  public LimitManagerRepository(IAmazonDynamoDB dynamoDbClient)
  {
    _dynamoDbClient = dynamoDbClient;
  }

  public async Task<LimitManager?> Get(int branch, int accountNumber)
  {
    var getItemRequest = new GetItemRequest
    {
      TableName = tableName,
      Key = new Dictionary<string, AttributeValue>
            {
                { "accountNumber", new AttributeValue { N = accountNumber.ToString() } },
                { "branch", new AttributeValue { N = branch.ToString() } }
            }
    };

    var response = await _dynamoDbClient.GetItemAsync(getItemRequest);

    if (response.Item.Count == 0) return null;

    var item = Document.FromAttributeMap(response.Item);

    return JsonSerializer.Deserialize<LimitManager>(item.ToJson());
  }

  public async Task<bool> Put(LimitManager accountLimitManager)
  {
    var accountLimitJson = JsonSerializer.Serialize(accountLimitManager);
    var accountLimitFormatted = Document.FromJson(accountLimitJson).ToAttributeMap();

    var putItemRequest = new PutItemRequest
    {
      TableName = tableName,
      Item = accountLimitFormatted
    };

    var response = await _dynamoDbClient.PutItemAsync(putItemRequest);

    return response.HttpStatusCode == HttpStatusCode.OK;
  }

  public async Task<bool> Delete(int branch, int accountNumber)
  {
    var deleteItemRequest = new DeleteItemRequest
    {
      TableName = tableName,
      Key = new Dictionary<string, AttributeValue>
            {
                { "accountNumber", new AttributeValue { N = accountNumber.ToString() } },
                { "branch", new AttributeValue { N = branch.ToString() } }
            }
    };

    var response = await _dynamoDbClient.DeleteItemAsync(deleteItemRequest);

    return response.HttpStatusCode == HttpStatusCode.OK;
  }

}

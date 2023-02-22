using Microsoft.Data.Sqlite;
using System.Text.Json;

namespace BlazorApp.Data;
public class BackendService
{
    private string filename;
    public BackendService(string filename)
    {
        this.filename = filename;

    }

    public async Task Init()
    {
        using (var connection = new SqliteConnection($"Data Source={this.filename}"))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"CREATE TABLE IF NOT EXISTS data (
                id TEXT,
                name TEXT,
                deviceTypeId TEXT,
                failsafe INTEGER,
                tempMin INTEGER,
                tempMax INTEGER,
                installationPosition TEXT,
                insertInto19InchCabinet INTEGER,
                motionEnable INTEGER,
                siplusCatalog INTEGER,
                simaticCatalog INTEGER,
                rotationAxisNumber INTEGER,
                positionAxisNumber INTEGER,
                terminalElement INTEGER,
                advancedEnvironmentalConditions INTEGER,
                pk INTEGER PRIMARY KEY AUTOINCREMENT
            )";

            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task InsertData(Device d)
    {
        using (var connection = new SqliteConnection($"Data Source={this.filename}"))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @$"INSERT INTO data
                (
                    id,
                    name,
                    deviceTypeId,
                    failsafe,
                    tempMin,
                    tempMax,
                    installationPosition,
                    insertInto19InchCabinet,
                    motionEnable,
                    siplusCatalog,
                    simaticCatalog,
                    rotationAxisNumber,
                    positionAxisNumber,
                    terminalElement,
                    advancedEnvironmentalConditions
                )

                VALUES 
                (
                    $Id,
                    $Name,
                    $DeviceTypeId,
                    $Failsafe,
                    $TempMin,
                    $TempMax,
                    $InstallationPosition,
                    $InsertInto19InchCabinet,
                    $MotionEnable,
                    $SiplusCatalog,
                    $SimaticCatalog,
                    $RotationAxisNumber,
                    $PositionAxisNumber,
                    $TerminalElement,
                    $AdvancedEnvironmentalConditions
            )";

            command.Parameters.AddWithValue("$Id", d.Id);
            command.Parameters.AddWithValue("$Name", d.Name);
            command.Parameters.AddWithValue("$DeviceTypeId", d.DeviceTypeId);
            command.Parameters.AddWithValue("$Failsafe", d.Failsafe);
            command.Parameters.AddWithValue("$TempMin", d.TempMin);
            command.Parameters.AddWithValue("$TempMax", d.TempMax);
            command.Parameters.AddWithValue("$InstallationPosition", d.InstallationPosition);
            command.Parameters.AddWithValue("$InsertInto19InchCabinet", d.InsertInto19InchCabinet);
            command.Parameters.AddWithValue("$MotionEnable", d.MotionEnable);
            command.Parameters.AddWithValue("$SiplusCatalog", d.SiplusCatalog);
            command.Parameters.AddWithValue("$SimaticCatalog", d.SimaticCatalog);
            command.Parameters.AddWithValue("$RotationAxisNumber", d.RotationAxisNumber);
            command.Parameters.AddWithValue("$PositionAxisNumber", d.PositionAxisNumber);
            command.Parameters.AddWithValue("$TerminalElement", d.TerminalElement);
            command.Parameters.AddWithValue("$AdvancedEnvironmentalConditions", d.AdvancedEnvironmentalConditions);

            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task<Device[]> GetDataAsync()
    {
        var result = new List<Device>();
        using (var connection = new SqliteConnection($"Data Source={this.filename}"))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"SELECT * FROM data";

            using (var reader = command.ExecuteReaderAsync())
            {
                if (reader != null)
                {
                    var content = await reader;

                    while (await content.ReadAsync())
                    {
                        var id = content.GetString(0);
                        var name = content.GetString(1);
                        var deviceTypeId = content.GetString(2);
                        var failsafe = content.GetBoolean(3);
                        var tempMin = content.GetInt32(4);
                        var tempMax = content.GetInt32(5);
                        var installationPosition = content.GetString(6);
                        var insertInto19InchCabinet = content.GetBoolean(7);
                        var motionEnable = content.GetBoolean(8);
                        var siplusCatalog = content.GetBoolean(9);
                        var simaticCatalog = content.GetBoolean(10);
                        var rotationAxisNumber = content.GetInt32(11);
                        var positionAxisNumber = content.GetInt32(12);
                        var terminalElement = content.GetBoolean(13);
                        var advancedEnvironmentalConditions = content.GetBoolean(14);
                        var pk = content.GetInt32(15);

                        var deviceData = new Device(
                            pk,
                            id,
                            name,
                            deviceTypeId,
                            failsafe,
                            tempMin,
                            tempMax,
                            installationPosition,
                            insertInto19InchCabinet,
                            motionEnable,
                            siplusCatalog,
                            simaticCatalog,
                            rotationAxisNumber,
                            positionAxisNumber,
                            terminalElement,advancedEnvironmentalConditions
                        );
                        result.Add(deviceData);
                    }
                }
            }
        }
        return result.ToArray();
    }

    public async Task ClearDataAsync()
    {
        using (var connection = new SqliteConnection($"Data Source={this.filename}"))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM data";
            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task AddJsonDataAsync(Stream jsonString)
    {
        try
        {
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var input = await JsonSerializer.DeserializeAsync<JsonInput>(jsonString, serializeOptions);
            if (input != null && input.devices != null)
            {
                foreach (var device in input.devices)
                {
                    await this.InsertData(device);
                }
            }
        }
        catch (System.Exception e)
        {
            // in a bigger project we would need proper error handling here...
            System.Console.WriteLine(e);
        }
    }

    public async Task RemoveDeviceAsync(Device d)
    {
        using (var connection = new SqliteConnection($"Data Source={this.filename}"))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM data WHERE pk = {d.PrimaryKey};";
            await command.ExecuteNonQueryAsync();
        }
    }

    // internal class for easy json parsing
    class JsonInput
    {
        public Device[]? devices { get; set; }
    }
}

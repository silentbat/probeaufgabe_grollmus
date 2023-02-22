namespace BlazorApp.Data;

public class Device
{
    public string Id {get; set;} = "";
    public string Name {get; set;} = "";
    public string DeviceTypeId {get; set;} = "";
    public bool Failsafe {get; set;}
    public int TempMin {get; set;}
    public int TempMax {get; set;}
    public string InstallationPosition {get; set;} = "";
    public bool InsertInto19InchCabinet {get; set;}
    public bool MotionEnable {get; set;}
    public bool SiplusCatalog {get; set;}
    public bool SimaticCatalog {get; set;}
    public int RotationAxisNumber {get; set;}
    public int PositionAxisNumber {get; set;}
    public bool TerminalElement {get; set;}
    public bool AdvancedEnvironmentalConditions {get; set;}
    
    public readonly int PrimaryKey;

public Device()
    {
        this.PrimaryKey = 0;
    }

    public Device(
        int pk,
        string Id,
        string Name,
        string DeviceTypeId,
        bool Failsafe,
        int TempMin,
        int TempMax,
        string InstallationPosition,
        bool InsertInto19InchCabinet,
        bool MotionEnable,
        bool SiplusCatalog,
        bool SimaticCatalog,
        int RotationAxisNumber,
        int PositionAxisNumber,
        bool TerminalElement,
        bool AdvancedEnvironmentalConditions
    )
    {
        this.PrimaryKey = pk;
        this.Id = Id;
        this.Name = Name;
        this.DeviceTypeId = DeviceTypeId;
        this.Failsafe = Failsafe;
        this.TempMin = TempMin;
        this.TempMax = TempMax;
        this.InstallationPosition = InstallationPosition;
        this.InsertInto19InchCabinet = InsertInto19InchCabinet;
        this.MotionEnable = MotionEnable;
        this.SiplusCatalog = SiplusCatalog;
        this.SimaticCatalog = SimaticCatalog;
        this.RotationAxisNumber = RotationAxisNumber;
        this.PositionAxisNumber = PositionAxisNumber;
        this.TerminalElement = TerminalElement;
        this.AdvancedEnvironmentalConditions = AdvancedEnvironmentalConditions;
    }

}